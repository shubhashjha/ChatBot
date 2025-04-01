
using chat_bot;
//Login Into : https://platform.openai.com/signup
//Navigate to the API section and generate a new API key. Ensure you store this key securely, as it will be required to authenticate your application.


Console.WriteLine("Enter your OpenAI API Key:");
string apiKey = Console.ReadLine();
var chatClient = new ChatGPTClient(apiKey);

Console.WriteLine("Chatbot is ready. Type 'exit' to end the conversation.");
while (true)
{
    Console.Write("You: ");
    string userInput = Console.ReadLine();
    if (userInput.ToLower() == "exit")
        break;

    string response = await chatClient.GetResponseAsync(userInput);
    Console.WriteLine($"Bot: {response}");
}