using Utszebe.Core.Entities;

namespace Utszebe.Core.Interfaces
{
    public interface IMessageTranslator
    {
        public Task<String> TranslateMessageToSQLQuery(Message message);
    }
}
