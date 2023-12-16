using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Where : BaseKeys
    {
        public string SqlWhere { get; set; }
        public Where()
        {

        }
        public override string ToString()
        {
            return string.Join(" ", KeyBefore, SqlWhere, KeyAfter).ToUpper();
        }
    }
}
