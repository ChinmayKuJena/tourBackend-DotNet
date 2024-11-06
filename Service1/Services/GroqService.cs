using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using GroqPlaceInfoApi.Models;
using GroqPlaceInfoApi.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
// using Service1.Config; 
using Service1.Models; 


    public class GroqService
    {

        private readonly string ApiKey;
        private readonly string ApiUrl;        

        public GroqService(IOptions<GroqSettings> groqSettings)
        {
            ApiKey = groqSettings.Value.ApiKey;
            ApiUrl = groqSettings.Value.ApiUrl;
        }


        // """ This method is used for checking if user input place name is really a placename or something else. """
        public async Task<bool> IsPlaceName(string place)
        {
            // Define the prompt to check if the input is a place name
            var prompt = $"Is '{place}' a known geographic location? Respond with 'yes' or 'no' only.";

            // Call the Groq API to validate the input
            string response = await GetGroqResponse(prompt);

            // Check if the response is 'Yes' (indicating it's a valid place name)
            return response.Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);
        }

        // """" This Method is used for Fetching trivia and fun facts about a place """
        public async Task<Dictionary<string, object>> GetTriviaAndFunFacts(string place)
        {
            var resultData = new Dictionary<string, object>();
            resultData.Add("placeName", place);
            resultData.Add("requestType", "Trivia and Fun Facts");

            // Trivia Prompt
            string triviaPrompt = PromptLibrary.GetTriviaPrompt(place);
            string triviaResponse = await GetGroqResponse(triviaPrompt);
            resultData.Add("trivia", triviaResponse);

            // Fun Facts Prompt
            string funFactsPrompt = PromptLibrary.GetFunFactsPrompt(place);
            string funFactsResponse = await GetGroqResponse(funFactsPrompt);
            resultData.Add("fun_facts", funFactsResponse);

            return resultData;
        }

        // """ This is basicalyy compare some details about 2 places TODO: Plan to make comparison between more than 2 """
        public async Task<Dictionary<string, Dictionary<string, string>>> GetPlaceComparison(List<string> places, List<string> criteria)
        {
            var comparisonData = new Dictionary<string, Dictionary<string, string>>();

            foreach (var place in places)
            {
                var placeDetails = new Dictionary<string, string>();

                foreach (var criterion in criteria)
                {
                    var prompt = PromptLibrary.GetComparisonPrompt(criterion, place);
                    string response = await GetGroqResponse(prompt);

                    placeDetails[criterion] = response ?? "No information available";
                }

                comparisonData[place] = placeDetails;
            }

            return comparisonData;
        }

        // """ This is a wrapper method for rendering between different requestTypes """
        public async Task<Dictionary<string, object>> GetSpecificPlaceDetail(string place, string requestType)
        {
            var newData = new Dictionary<string, object>();
            var prompt = PromptLibrary.GetPrompt(requestType, place);
            string detail = await GetGroqResponse(prompt);

            // var placeInfo = new Dictionary<string, string>();
            newData.Add("placeName", place);
            newData.Add("requestType", requestType);
            newData.Add(requestType, detail);
            return newData;
        }
        
        // """ This is the most used or major Method in our whole service Where it gets all the details of user request """
        public async Task<Dictionary<string, object>> GetPlaceDetails(string place)
        {
            var newData = new Dictionary<string, object>();
            var placeInfo = new Dictionary<string, string>();
            newData.Add("requestType", "Place Info");
            newData.Add("placeName", place);

            foreach (var key in PromptLibrary.PlacePrompts.Keys)
            {
                var prompt = PromptLibrary.GetPrompt(key, place);
                string response = await GetGroqResponse(prompt);
                placeInfo.Add(key, response);
            }
            newData.Add("details", placeInfo);

            return newData;
        }

        // """ The Gate or Key to our whole Datas or ser """
        private async Task<string> GetGroqResponse(string question)
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest();
            request.AddHeader("Authorization", $"Bearer {ApiKey}");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                messages = new[] {
                new { role = "system", content = "You are a helpful assistant with expertise in geographical and place-based information, similar to Google Maps." },

                new { role = "user", content = question } },
                model = "llama3-8b-8192"
            });

            var response = await client.PostAsync(request);

            if (response.IsSuccessful)
            {
                var groqResponse = JsonConvert.DeserializeObject<GroqResponse>(response.Content);
                return groqResponse.Choices?[0].Message.Content ?? "No response received.";
            }

            return "Error fetching data from Groq.";
        }
    }

