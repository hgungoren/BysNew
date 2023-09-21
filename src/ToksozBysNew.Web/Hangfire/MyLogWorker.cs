using Hangfire;
using Quartz;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Uow;
using Microsoft.Extensions.Logging;

namespace ToksozBysNew.Web.Hangfire
{
    public class MyLogWorker : HangfireBackgroundWorkerBase, IMyLogWorker
    {
        public MyLogWorker()
        {
            RecurringJobId = nameof(MyLogWorker);
            CronExpression = Cron.Daily();
        }

        public override Task DoWorkAsync(CancellationToken cancellationToken = default)
        { 
            using (var uow = LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>().Begin())
            {
                Logger.LogInformation("Executed MyLogWorker..!");
                return Task.CompletedTask;
            }

        }
    }

}
