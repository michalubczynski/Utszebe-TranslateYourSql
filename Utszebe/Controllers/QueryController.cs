using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Primitives;
using System.Text;
using System.Text.Json;
using Utszebe.Core.Entities;
using Utszebe.Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueryController : ControllerBase
    {
        private const string _schema_details_json = "[{\"name\":\"JPK_NAGLOWEK\",\"columns\":[{\"name\":\"NAGLOWEK_ID\"},{\"name\":\"CZAS_WYSLANIA\"},{\"name\":\"CZAS_UTWORZENIA\"},{\"name\":\"DATA_OD\"},{\"name\":\"DATA_DO\"},{\"name\":\"ROKMC\"}]},{\"name\":\"JPK_PODMIOT\",\"columns\":[{\"name\":\"PODMIOT_ID\"},{\"name\":\"NAGLOWEK_ID\"},{\"name\":\"NIP\"},{\"name\":\"IMIE\"},{\"name\":\"NAZWISKO\"},{\"name\":\"DATA_URODZENIA\"},{\"name\":\"TELEFON\"}]},{\"name\":\"VAT_SPRZEDAZ\",\"columns\":[{\"name\":\"SPRZEDAZ_ID\"},{\"name\":\"NAGLOWEK_ID\"},{\"name\":\"NR_KONTRAHENTA\"},{\"name\":\"DOWOD_SPRZEDAZY\"},{\"name\":\"DATA_WYSTAWIENIA\"},{\"name\":\"DATA_SPRZEDAZY\"},{\"name\":\"P_6\"},{\"name\":\"P_8\"},{\"name\":\"P_9\"},{\"name\":\"P_11\"},{\"name\":\"P_13\"},{\"name\":\"P_15\"},{\"name\":\"P_16\"},{\"name\":\"P_19\"},{\"name\":\"P_96\"}]},{\"name\":\"VAT_ZAKUP\",\"columns\":[{\"name\":\"ZAKUP_ID\"},{\"name\":\"NAGLOWEK_ID\"},{\"name\":\"NR_DOSTAWCY\"},{\"name\":\"DOWOD_ZAKUPU\"},{\"name\":\"DATA_ZAKUPU\"},{\"name\":\"DATA_WPLYWU\"},{\"name\":\"P_61\"},{\"name\":\"P_77\"},{\"name\":\"P_78\"}]}]\r\n";

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
        public async Task<ActionResult<string[]>> GetResult([FromBody] Message request)
        {
            MessagesEchanged.Add(request);

            //StringBuilder prompt = new StringBuilder();
            //prompt.Append("You are an SQL assistant.");
            //prompt.Append("\n\n Based on the provided database schema, recent user interactions and the current request, construct SQL query that extracts the information.");
            //prompt.Append($"\n\n User request is: {request.UserInput}.");
            //prompt.Append("\n\nOnly craft the SQL code without any comments or explanations.");
            //prompt.Append("ALWAYS reference columns using their respective table names to avoid ambiguity.");
            //prompt.Append($"\n\nAdhere to the SQL dialect: SQL Lite");
            //prompt.Append($"\n\nOnly utilize columns and tables from the provided schema below: {_schema_details_json}");
            //var promptString = prompt.ToString();
            var promptString = request.UserInput;

            var fullResultFromAi = await _messageTranslator.TranslateMessageToSQLQuery(promptString, UpdateRespopnse);
            if (fullResultFromAi.IsFailed)
            {
                return StatusCode(500);
            }

            return Ok(JsonSerializer.Serialize(fullResultFromAi.Value));
        }

        private async Task UpdateRespopnse(string response)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveResult", response);
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
