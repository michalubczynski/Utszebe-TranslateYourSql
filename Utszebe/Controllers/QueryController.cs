using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Utszebe.Core.Entities;
using Utszebe.Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private readonly IDatabaseRepository _database;
        private readonly IMessageTranslator _messageTranslator;
        

        public QueryController(IDatabaseRepository database, IMessageTranslator messageTranslator)
        {
            _database = database;
            _messageTranslator = messageTranslator;
        }

        public List<Message> MessagesEchanged { get; set; } = new List<Message>();

        [HttpGet("columns")]
        public async Task<ActionResult<string>> GetColumns()
        {
            return Ok(await _database.GetAllColumnsAsync());
        }
        [HttpGet("tables")]
        public async Task<ActionResult<string>> GetTables()
        {
            return Ok(await _database.GetAllTablesAsync());
        }
        [HttpGet("tablesWithColumns")]
        public async Task<ActionResult<string>> GetTablesAndColumnsAsync()
        {
            return Ok(await _database.GetTablesAndColumnsAsync());
        }
        [HttpGet("createDatabase")]
        public async Task<ActionResult<bool>> CreateDatabase()
        {
            return Ok(await _database.CreateDatabaseAsync());
        }
    }
}
