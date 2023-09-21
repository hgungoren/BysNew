using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ToksozBysNew.Web.SignalR
{
    public class UiNotificationClient : ITransientDependency
    {
        private readonly IHubContext<UiNotificationHub, INotificationClient> _notificationHub;

        public UiNotificationClient(IHubContext<UiNotificationHub, INotificationClient> notificationHub)
        {
            _notificationHub = notificationHub;
        }

        public async Task SendNotification(string message)
        {
            await _notificationHub
                .Clients
                .All
                .ReceiveNotificationMessage(message);
        }
    }
}
