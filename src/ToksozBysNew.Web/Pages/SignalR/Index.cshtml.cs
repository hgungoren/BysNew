using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using System.Timers;
using ToksozBysNew.Web.SignalR;

namespace ToksozBysNew.Web.Pages.SignalR
{
    public class IndexModel : ToksozBysNewPageModel
    {
        private readonly UiNotificationClient _uiNotificationClient;

        public IndexModel(UiNotificationClient uiNotificationClient)
        {
            _uiNotificationClient = uiNotificationClient;
        }

        public async Task OnGet()
        {
            var timer = new Timer
            {
                Interval = 1000, //ticks every 1 second
                Enabled = true
            };

            timer.Elapsed += async (sender, args) =>
            {
                //sends server data to client
                await _uiNotificationClient.SendNotification("Server time is " + DateTime.Now.ToLongTimeString());
            };
        }
    }
}
