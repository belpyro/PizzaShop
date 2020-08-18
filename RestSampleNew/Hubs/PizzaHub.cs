using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RestSample.Logic.Models;
using System.Threading.Tasks;

namespace PizzaShop.Web.Hubs
{
    public interface IPizzaClient
    {
        Task PizzaAdded(PizzaDto dto);

        Task UpdateMessage(string message); // lowerCamelCase()
    }

    [HubName("sample")]
    public class PizzaHub : Hub<IPizzaClient>
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.UpdateMessage(message);
        }
    }
}
