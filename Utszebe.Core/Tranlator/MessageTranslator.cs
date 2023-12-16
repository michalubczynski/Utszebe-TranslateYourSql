using LLama;
using Utszebe.Core.Entities;
using Utszebe.Core.Interfaces;

namespace Utszebe.Core.Tranlator
{
    public class MessageTranslator : IMessageTranslator
    {
        private readonly string _modelPath = "C:\\Utszebe\\Model\\wizardLM-7B.ggmlv3.q4_1.bin";
        public async Task<String> TranslateMessageToSQLQuery(Message message)
        {
            var result = string.Empty;
            var model = new LLamaModel(new LLamaParams(
                model: _modelPath,
                n_ctx: 512,
                interactive: true,
                repeat_penalty: 1.0f,
                verbose_prompt: false));

            var session = new ChatSession<LLamaModel>(model)
                .WithPrompt($"Create a new SQL query out of text: {message.UserInput}")
                .WithAntiprompt(new[] { "User: " });
            Console.WriteLine();
            Console.Write("User: ");
            while (true)
            {
                var prompt = "\n";
                foreach (var output in session.Chat(prompt, encoding: "UTF-8"))
                {
                    result += output;
                }
                return result;
            }
        }
    }
}
