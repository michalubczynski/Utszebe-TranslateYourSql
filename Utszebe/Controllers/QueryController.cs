using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
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
        private readonly IHubContext<ResultHub> _hubContext;


        public QueryController(IDatabaseRepository database, IMessageTranslator messageTranslator, IHubContext<ResultHub> hubContext)
        {
            _database = database;
            _messageTranslator = messageTranslator;
            _hubContext = hubContext;

        }

        [HttpPost("result")]
        public async Task<ActionResult<string[]>> GetResult()
        {
            var resultFromAi = "WYNIK CHATA";

            // Prześlij wynik do klientów za pomocą SignalR
            await _hubContext.Clients.All.SendAsync("ReceiveResult", resultFromAi);

            return Ok(resultFromAi);
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

        [HttpPost]
        public async Task<ActionResult> AskAI([FromBody] Message request)
        {
            if (request is null)
            {
                return BadRequest("Invalid request format");
            }
            MessagesEchanged.Add(request);

            var response = await _messageTranslator.TranslateMessageToSQLQuery(request);

            return Ok(JsonSerializer.Serialize(response));
        }
    }
}
