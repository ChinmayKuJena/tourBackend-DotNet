using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Service1.config;
using Newtonsoft.Json;
using Service1.Models;

// """ Data Base Service For upadating and inserting tables """


    public class DatabaseService
    {
        private readonly DatabaseConnection _databaseConnection;

        public DatabaseService(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }
        // """ To get images of input place from 2 tables using INNER JOIN """
        public async Task<List<ImageRecognitionResult>> GetImageRecognitionResultsByPlaceNameAsync(string placeName)
        {
            var results = new List<ImageRecognitionResult>();

            using (var connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();

                // Use parameterized query to prevent SQL injection
                var command = new SqlCommand(@"
            SELECT 
                irr.Id AS ResultId,
                irr.PlaceName,
                irr.IsPlaceImage,
                irr.S3Url,
                dl.Id AS LabelId,
                dl.Name AS LabelName,
                dl.Percentage AS LabelPercentage
            FROM 
                ImageRecognitionResults AS irr
            INNER JOIN 
                DetectedLabels AS dl ON irr.Id = dl.ImageRecognitionResultId
            WHERE 
                irr.PlaceName = @PlaceName", connection);

                command.Parameters.AddWithValue("@PlaceName", (object)placeName ?? DBNull.Value);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var resultId = reader.GetInt32(reader.GetOrdinal("ResultId"));
                        var existingResult = results.Find(r => r.ResultId == resultId);

                        if (existingResult == null)
                        {
                            existingResult = new ImageRecognitionResult
                            {
                                ResultId = resultId,
                                PlaceName = reader.GetString(reader.GetOrdinal("PlaceName")),
                                IsPlaceImage = reader.GetBoolean(reader.GetOrdinal("IsPlaceImage")),
                                S3Url = reader.GetString(reader.GetOrdinal("S3Url")),
                                DetectedLabels = new List<DetectedLabel>()
                            };
                            results.Add(existingResult);
                        }

                        var label = new DetectedLabel
                        (
                            reader.GetString(reader.GetOrdinal("LabelName")),
                            (float)reader.GetDouble(reader.GetOrdinal("LabelPercentage"))
                        );
                        existingResult.DetectedLabels.Add(label);
                    }
                }
            }

            return results;
        }
        // """ Save Recognized Image by AWS Rekognition And also the LabelName with it's % to use them as TAGS in Images when Blog Will be implemented  """

        public async Task SaveImageRecognitionResultAsync(ImageRecognitionResponse recognitionResult)
        {
            using (var connection = _databaseConnection.GetConnection())
            {
                await connection.OpenAsync();

                // Insert into ImageRecognitionResults table
                var command = new SqlCommand(
                    "INSERT INTO ImageRecognitionResults (PlaceName, IsPlaceImage, S3Url) " +
                    "OUTPUT INSERTED.Id VALUES (@PlaceName, @IsPlaceImage, @S3Url)", connection);

                command.Parameters.AddWithValue("@PlaceName", recognitionResult.PlaceName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsPlaceImage", recognitionResult.IsPlaceImage);
                command.Parameters.AddWithValue("@S3Url", recognitionResult.S3Url);

                // Retrieve the newly inserted ImageRecognitionResult Id
                var resultId = (int)await command.ExecuteScalarAsync();

                // Insert each detected label into the DetectedLabels table
                foreach (var label in recognitionResult.DetectedLabels)
                {
                    var labelCommand = new SqlCommand(
                        "INSERT INTO DetectedLabels (ImageRecognitionResultId, Name, Percentage) VALUES (@ResultId, @Name, @Percentage)", connection);

                    labelCommand.Parameters.AddWithValue("@ResultId", resultId);
                    labelCommand.Parameters.AddWithValue("@Name", label.Name);
                    labelCommand.Parameters.AddWithValue("@Percentage", label.Percentage);

                    await labelCommand.ExecuteNonQueryAsync();
                }
            }
        }
    }
