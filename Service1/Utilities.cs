using System.Collections.Generic;

namespace GroqPlaceInfoApi.Utilities
{
    public static class PromptLibrary
    {
        public static readonly Dictionary<string, string> PlacePrompts = new Dictionary<string, string>
        {
            { "historical_facts", "Tell me about the historical significance of {0}.Just Want '5' points" },
            { "attractions", "What are the main attractions in {0}?.Just Want '5' points" },
            { "famous_places", "Which famous places should I not miss in {0}?.Just Want '5' points" },
            { "unique_information", "Share some unique information about {0}.Just Want '5' points" }
        };

        public static string GetPrompt(string key, string place)
        {
            if (PlacePrompts.TryGetValue(key, out var prompt))
            {
                return string.Format(prompt, place);
            }
            return "null";
        }

        public static readonly Dictionary<string, string> PlaceComparisonPrompts = new Dictionary<string, string>
        {
            { "historical_facts", "Provide 5 key historical facts about {0}." },
            { "attractions", "List 5 main attractions in {0}." },
            { "famous_places", "What are 5 famous places in {0}?" },
            { "unique_information", "Share 5 unique facts about {0}." }
        };

        public static string GetComparisonPrompt(string key, string place)
        {
            if (PlaceComparisonPrompts.TryGetValue(key, out var prompt))
            {
                return string.Format(prompt, place);
            }
            return "Invalid prompt";
        }
        public static Dictionary<string, string> funfactsPrompt = new Dictionary<string, string>
        {
            { "historical_facts", "Share historical facts about {0}." },
            { "trivia", "What are some fun and interesting trivia facts about {0}?" },
            { "fun_facts", "Provide some unique and quirky facts about {0} that people may not know." }
        };        
        public static string GetTriviaPrompt(string place)
        {
            return string.Format(funfactsPrompt["trivia"], place);
        }

        public static string GetFunFactsPrompt(string place)
        {
            return string.Format(funfactsPrompt["fun_facts"], place);
        }
    }
    
}
