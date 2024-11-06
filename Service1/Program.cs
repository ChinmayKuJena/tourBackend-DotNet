using Amazon.Extensions.NETCore.Setup;
using Amazon.Rekognition;
using Amazon.S3;
// using GroqPlaceInfoApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service1.config;
using Service1.Services;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
var config = builder.Configuration;


// Register GroqSettings configuration
builder.Services.Configure<GroqSettings>(config.GetSection("GroqSettings"));

// Register the GroqService and SqsService with dependency injection


// Configure the database connection
var connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
builder.Services.AddSingleton(new DatabaseConnection(connectionString)); 

// Get AWS configuration settings
var AWS_ACCESS_KEY = config["AWS:AccessKey"];
var AWS_SECRET_KEY = config["AWS:SecretKey"];
var AWS_REGION = config["AWS:Region"];

// Configure AWS options
var awsOptions = new AWSOptions
{
    Credentials = new Amazon.Runtime.BasicAWSCredentials(AWS_ACCESS_KEY, AWS_SECRET_KEY),
    Region = Amazon.RegionEndpoint.GetBySystemName(AWS_REGION)
};

// Add AWS services
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonRekognition>();
builder.Services.AddAWSService<IAmazonS3>();

// builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<ImageRecognitionService>();
// sqs polling
// builder.Services.AddHostedService<SqsPollingService>();

builder.Services.AddScoped<GroqService>();
builder.Services.AddScoped<DatabaseService>(); // Register DatabaseService





builder.Services.AddSingleton<KafkaProducer>(provider =>
    new KafkaProducer("localhost:9092"));

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

