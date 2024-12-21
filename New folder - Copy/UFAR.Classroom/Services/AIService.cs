using Azure.AI.OpenAI;
using OpenAI.Chat;
using UFAR.Classroom;
using UFAR.Classroom.Entities;
using System.ClientModel;
using UFAR.Classroom.Entities;
using UFAR.Classroom.Services;

namespace UFAR.Classroom.Services
{
    public class AIService : IAIService
    {
        private readonly AzureOpenAIClient _azureClient;
        private readonly ChatClient _chatClient;
        private readonly ApplicationDbContext _context; // Add DbContext

        public AIService(ApplicationDbContext context) // Inject DbContext through constructor
        {
            string endpoint = "https://i22m-m3in499z-westeurope.openai.azure.com/";
            string key = "BvxxwtmW5qmCcJpyKNNT0NkJjoCofoaEXpFSLwevwJ6d5XKPq1tJJQQJ99AKAC5RqLJXJ3w3AAAAACOGTiEi";
            ApiKeyCredential credential = new ApiKeyCredential(key);
            _azureClient = new AzureOpenAIClient(new Uri(endpoint), credential);
            _chatClient = _azureClient.GetChatClient("gpt-35-turbo");

            _context = context; // Initialize DbContext
        }

        public async Task<string> GetAIResponseAsync(string userMessage)
        {
            try
            {
                var completion = await _chatClient.CompleteChatAsync(
                    new ChatMessage[]
                    {
                new SystemChatMessage("You are an intelligent assistant designed to help users organize and manage their daily tasks, deadlines, and exams..."),
                new UserChatMessage(userMessage)
                    }
                );

                var aiResponse = completion.Value.Content[0].Text; // Get AI's response

                // Save AI response to the database
                var aiResponseEntity = new AIResponse
                {
                    UserMessage = userMessage,
                    Response = aiResponse,
                    CreatedAt = DateTime.UtcNow
                };

                _context.AIResponses.Add(aiResponseEntity); // Adding  to DbSet
                await _context.SaveChangesAsync(); // Saveing changes to database

                return aiResponse; // Return AI's response
            }
            catch (Exception ex)
            {
                // Log error for debugging
                Console.WriteLine($"Error while getting AI response: {ex.Message}");
                throw;
            }
        }
    }
}
