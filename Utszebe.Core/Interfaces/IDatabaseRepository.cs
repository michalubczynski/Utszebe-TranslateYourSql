using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utszebe.Core.Entities;
using Utszebe.Core.Entities.Database;

namespace Core.Interfaces
{
    public interface IDatabaseRepository
    {
        Task<bool> CreateDatabaseAsync();
        public Task<IEnumerable<TableWithColumns>> GetTablesAndColumnsAsync();
        public Task<IEnumerable<Column>> GetColumnsInTableAsync(Table table);
        public Task<IEnumerable<Column>> GetAllColumnsAsync();
        public Task<IEnumerable<Table>> GetAllTablesAsync();
    }
}
