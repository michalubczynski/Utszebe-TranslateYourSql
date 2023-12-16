using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Having : BaseKeys
    {
        public string SqlHaving { get; set; }
        public Having()
        {

        }
        public override string ToString()
        {
            return string.Join(" ", KeyBefore, SqlHaving, KeyAfter);
        }
    }
}
