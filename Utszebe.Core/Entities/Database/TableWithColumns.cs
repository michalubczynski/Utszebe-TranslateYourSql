using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utszebe.Core.Entities.Database
{
    public class TableWithColumns
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Column> Columns { get; set; } = new List<Column>();
    }
}
