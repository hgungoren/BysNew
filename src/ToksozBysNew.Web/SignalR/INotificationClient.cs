using System.Threading.Tasks;

namespace ToksozBysNew.Web.SignalR
{
    public interface INotificationClient
    {
        Task ReceiveNotificationMessage(string message);    
    }
}
