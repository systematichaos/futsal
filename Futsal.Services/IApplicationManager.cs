using Futsal.Data;
using System.Threading.Tasks;
using System.Web;

namespace Futsal.Services
{
    public interface IApplicationManager
    {
        Task<int> ReturnApplicationIDAsync();
        Task<string> ReturnApplicationNameAsync();
        string ReturnIPAddress();

    }


    public class ApplicationManager: IApplicationManager
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationManager(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public async Task<int> ReturnApplicationIDAsync() => await _applicationRepository.GetApplicationIdAsync();
        public string ReturnIPAddress() => HttpContext.Current.Request.UserHostAddress;
        public async Task<string> ReturnApplicationNameAsync() =>  await _applicationRepository.GetApplicationNameAsync();
    }
}
