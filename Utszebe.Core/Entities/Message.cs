using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utszebe.Core.Entities
{
    public class Message
    {
        public int Id { get; set;  }
        public string UserInput { get; set; } = string.Empty;
    }
}
