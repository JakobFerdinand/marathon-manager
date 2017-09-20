using System.Collections.Generic;
using System.Threading.Tasks;
using UI.Universal.Models;

namespace UI.Universal.Interfaces
{
    public interface IRunnerService
    {
        Task<IEnumerable<Runner>> GetAll();
    }
}
