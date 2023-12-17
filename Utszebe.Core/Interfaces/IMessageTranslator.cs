using Utszebe.Core.Entities;

namespace Utszebe.Core.Interfaces
{
    public interface IMessageTranslator
    {
        public Task<String> TranslateMessageToSQLQuery(string message, Func<string, Task> func);
    }
}
