using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Select : BaseKeys
    {
        private const string SqlSelect = "SELECT";
        public Select()
        {

        }
        public override string ToString()
        {
            return string.Join(" ", SqlSelect, KeyAfter);
        }
    }
}
