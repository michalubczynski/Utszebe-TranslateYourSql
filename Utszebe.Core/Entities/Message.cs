using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utszebe.Core.Entities
{
    public class Message
    {
        [Required]
        public int Id { get; set;  }
        [Required]
        public string UserInput { get; set; } = string.Empty;
    }
}
