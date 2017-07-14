using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UI.Universal.Interfaces
{
    public interface IRunnerService
    {
        Task<IEnumerable<Runner>> GetAll();
    }
}
