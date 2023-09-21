using System.Threading;
using System.Threading.Tasks;

namespace ToksozBysNew.Web.Hangfire
{
    public interface IMyLogWorker
    {
        Task DoWorkAsync(CancellationToken cancellationToken = default);
    }
}
