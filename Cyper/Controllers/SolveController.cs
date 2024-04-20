using Cyper.Models.Identity;
using Cyper.Models.Problems;
using Cyper.Models.Solves;
using Cyper.Services.Interface;
using Cyper.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cyper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolveController : ControllerBase
    {
        private readonly ISolveRepository _solveRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SolveController(ISolveRepository solveRepository, UserManager<ApplicationUser> userManager)
        {
            _solveRepository = solveRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = clsRoles.roleUser)]
        [HttpGet("GetSolveDetails")]
        public async Task<IActionResult> GetSolveDetails()
        {
            try
            {
                var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = await _userManager.FindByNameAsync(username);

                var data = await _solveRepository.GetByIdAsync(user.UserName);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = clsRoles.roleEngineer)]
        [HttpPost("CreateSolve")]
        public async Task<IActionResult> CreateSolve([FromBody] CreateSolve solve)  
        {
            if (ModelState.IsValid)
            {
                var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = await _userManager.FindByNameAsync(username);

                var data = await _solveRepository.AddAsync(solve, user.UserName);

                return Ok(data);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = clsRoles.roleEngineer)]
        [HttpPut("UpdateSolve/{Id}")]
        public async Task<IActionResult> UpdateSolve(int Id, [FromBody] UpdateSolve solve)
        {
            solve.Id = Id; // Ensure Id matches update target

            try
            {
                var updatedOffer = await _solveRepository.UpdateAsync(solve);

                return Ok(updatedOffer);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); 
            }
        }

        [Authorize(Roles = clsRoles.roleEngineer)]
        [HttpDelete("DeleteSolve/{Id}")]
        public async Task<IActionResult> DeleteSolve(int Id)
        {
            if (ModelState.IsValid)
            {
                var data = await _solveRepository.DeleteAsync(Id);

                return Ok(data);
            }

            return BadRequest(ModelState);
        }

        [Authorize(Roles = clsRoles.roleEngineer)]
        [HttpGet("GetAllSolves")]
        public async Task<ActionResult<IEnumerable<GetAllSolves>>> GetAllProblems()
        {
            var data = await _solveRepository.GetAllSolves();

            return Ok(data);
        }
    }
}
