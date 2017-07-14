using Core;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("/api/runners")]
    public class RunnersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RunnersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
