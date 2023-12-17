using Utszebe.Core.Entities;

namespace Utszebe.Core.Interfaces
{
    public interface IMessageTranslator
    {
        bool IsProcessing { get; set; }
        public Task<String> TranslateMessageToSQLQuery(Message message, Func<string, Task> func);
    }
}
