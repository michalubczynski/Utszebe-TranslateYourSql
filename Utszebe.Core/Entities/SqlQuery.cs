using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class SqlQuery
    {
        public Select Select { get; set; }
        public Where Where { get; set; }
        public Having Having { get; set; }
        public OrderBy OrderBy { get; set; }
        public GroupBy GroupBy { get; set; }
    }
}
