using AutoMapper;
using Core;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Controllers.Resources;

namespace Web.Controllers
{
    [Route("/api/runners")]
    public class RunnersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RunnersController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var runners = await _unitOfWork.Runners.GetAllWithRelated(asNoTracking: true);

            var runnerResources = _mapper.Map<IEnumerable<Runner>, IEnumerable<RunnerResource>>(runners);

            return Ok(runnerResources);
        }

        [HttpGet("{id}")]
        public IActionResult GetRunner(int id)
        {
            var runner = _unitOfWork.Runners.Get(id);

            if (runner is null)
                return NotFound();

            return Ok(runner);
        }
    }
}
