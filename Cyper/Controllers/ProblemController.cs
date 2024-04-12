using Cyper.Models.Identity;
using Cyper.Models.Problems;
using Cyper.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cyper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemController : ControllerBase
    {
        private readonly IProblemRepository _problemRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProblemController(IProblemRepository problemRepository, UserManager<ApplicationUser> userManager)
        {
            _problemRepository = problemRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = clsRoles.roleUser)]
        [HttpPost("CreateProblem")]
        public async Task<IActionResult> CreateProblem([FromBody] CreateProblem problem)
        {
            if (ModelState.IsValid)
            {
                var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = await _userManager.FindByNameAsync(username);

                var data = await _problemRepository.AddAsync(problem, user.UserName);

                return Ok(data);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = clsRoles.roleUser)]
        [HttpPut("UpdateProblem/{Id}")]
        public async Task<IActionResult> UpdateProblem(int Id, [FromBody] UpdateProblem problem)
        {
            problem.Id = Id; // Ensure UserName matches update target

            try
            {
                var updatedSubscription = await _problemRepository.UpdateAsync(problem);

                return Ok(updatedSubscription);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Handle offer not found gracefully
            }
        }

        [Authorize(Roles = clsRoles.roleUser)]
        [HttpDelete("DeleteProblem/{Id}")]
        public async Task<IActionResult> DeleteProblem(int Id)
        {
            var data = await _problemRepository.DeleteAsync(Id);

            return Ok(data);
        }

        [Authorize(Roles = clsRoles.roleEngineer)]
        [HttpGet("GetAllProblems")]
        public async Task<ActionResult<IEnumerable<GetAllProblems>>> GetAllProblems()
        {
            var data = await _problemRepository.GetListAsync();

            return Ok(data);
        }

        [Authorize(Roles = clsRoles.roleUser)]
        [HttpGet("GetAllProblemsToIser")]
        public async Task<ActionResult<IEnumerable<GetAllProblems>>> GetAllProblemsToUser()
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByNameAsync(username);

            var data = await _problemRepository.GetListAsync(user.UserName);

            return Ok(data);
        }
    }
}
