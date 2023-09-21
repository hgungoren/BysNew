using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ToksozBysNew.Web.SignalR
{
    public class UiNotificationHub : Hub<INotificationClient>, ITransientDependency
    {
        public Task SendNotification(string message)
        {
            return Clients
                .Client(Context.ConnectionId)
                .ReceiveNotificationMessage(message);
        }
    }
}
