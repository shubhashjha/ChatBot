using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace chat_bot
{
    internal class ChatGPTClient
    {
        private readonly string _apiKey;
        private readonly string CHATGPT_URL;
        public ChatGPTClient(string apiKey)
        {
            _apiKey = apiKey;
            CHATGPT_URL = "https://api.openai.com/v1/chat/completions";
        }
        public async Task<string> GetResponseAsync(string prompt)
        {
            using HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                model = "gpt-4o-mini",
                store = true,
                messages = new[]
                {
                new { role = "user", content = prompt }
            }
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);

            var response = await client.PostAsync(CHATGPT_URL, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                using JsonDocument doc = JsonDocument.Parse(responseContent);
                JsonElement root = doc.RootElement;

                // Extract content from the first choice
                return root.GetProperty("choices")[0]
                                      .GetProperty("message")
                                      .GetProperty("content")
                                      .GetString();

            }
            else
            {
                return "Please Try again";
            }
        }

    }
}
