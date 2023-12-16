using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ResultHub : Hub
{
    public async Task SendResult(string[] responseFromAi)
    {
        await Clients.All.SendAsync("ReceiveResult", responseFromAi);
    }
}
