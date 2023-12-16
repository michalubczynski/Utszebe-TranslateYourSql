using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utszebe.Core.Entities;

namespace Utszebe.Core.Interfaces
{
    public interface IMessageTranslator
    {
        public Task<String> TranslateMessageToSQLQuery(Message message);
    }
}
