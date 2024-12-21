namespace UFAR.Classroom.Services
{
    public interface IAIService
    {


        /// <summary>
        /// Gets the AI response based on the user's input message.
        /// </summary>
        /// <param name="userMessage">The message input from the user.</param>
        /// <returns>The AI's response as a string.</returns>
        Task<string> GetAIResponseAsync(string userMessage);


    }
}
