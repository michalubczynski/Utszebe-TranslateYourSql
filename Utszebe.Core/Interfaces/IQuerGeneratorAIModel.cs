using FluentResults;
using Utszebe.Core.Entities;

namespace Utszebe.Core.Interfaces
{
    public interface IQuerGeneratorAIModel
    {
        public Task<Result<string>> GenerateResponse(string message, Func<string, Task> func);
    }
}
