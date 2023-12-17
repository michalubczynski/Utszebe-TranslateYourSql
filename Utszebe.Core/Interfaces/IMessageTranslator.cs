using FluentResults;
using Utszebe.Core.Entities;

namespace Utszebe.Core.Interfaces
{
    public interface IMessageTranslator
    {
        public Task<Result<string>> TranslateMessageToSQLQuery(string message, Func<string, Task> func);
    }
}
