using FluentResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utszebe.Core.Entities;
using Utszebe.Core.Interfaces;

namespace Utszebe.Infrastracture.Services
{
    public class MessageTranslatorService : IMessageTranslator
    {
        private readonly IConfiguration _configuration;

        public MessageTranslatorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Result<string>> TranslateMessageToSQLQuery(string message, Func<string, Task> func)
        {
            string result = "";
            Request request = new Request(message);
            var buffer = request.AsByteArray();
            

            using (var clientWebSocket = new ClientWebSocket())
            {
                try
                {
                    var hostIp = _configuration.GetSection("HostIpAddress").Value;
                    await clientWebSocket.ConnectAsync(new Uri($"ws://{hostIp}:5005/api/v1/stream"), CancellationToken.None);
                    await clientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);



                    // Define a variable to concatenate all text
                    result = await ReadData(clientWebSocket, func);
                }
                catch (Exception ex)
                {
                    //TODO
                    //log this 
                    //Console.WriteLine($"Exception: {ex.Message}");
                    return Result.Fail(ex.Message);
                }
            }
            return result;
        }

        private async Task<string> ReadData(ClientWebSocket clientWebSocket, Func<string, Task> func)
        {
            var receiveBuffer = new byte[1024];
            var concatenatedTextSb = new StringBuilder();
            while (clientWebSocket.State == WebSocketState.Open)
            {
                var receiveResult = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(receiveBuffer), CancellationToken.None);

                if (receiveResult.MessageType == WebSocketMessageType.Close)
                {
                    await CloseConnection(clientWebSocket, receiveResult.CloseStatus.ToString()!);
                    return string.Empty;
                }
                
                // Process received data here
                var receivedData = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);

                // Parse JSON and handle accordingly
                using JsonDocument jsonDoc = JsonDocument.Parse(receivedData);
                if (jsonDoc.RootElement.TryGetProperty("event", out JsonElement eventElement))
                {
                    string eventValue = eventElement.GetString()!;

                    if (eventValue == "stream_end")
                    {
                        await CloseConnection(clientWebSocket, receiveResult.CloseStatus.ToString()!);
                        break;
                    }
                    // Extract the 'text' value from the received message and concatenate it
                    else if (jsonDoc.RootElement.TryGetProperty("text", out JsonElement textElement))
                    {
                        concatenatedTextSb.Append(textElement.GetString());
                        string res = concatenatedTextSb.ToString();
                        await func(res);
                    }
                }
            }
            return concatenatedTextSb.ToString();
        }

        private async Task CloseConnection(ClientWebSocket clientWebSocket, string receiveResult)
        {
            await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
            //Console.WriteLine("\n\nConnection closed.");
            //Console.WriteLine($"Close status: {receiveResult}");
        }
    }
}
