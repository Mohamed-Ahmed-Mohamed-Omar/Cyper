using Cyper.Models.Collages;
using Cyper.Models.Identity;
using Cyper.Models.Problems;
using Cyper.Services.Interface;
using Cyper.Services.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cyper.Controllers
{
    [Authorize(Roles = clsRoles.roleUser)]
    [Route("api/[controller]")]
    [ApiController]
    public class DataCollageController : ControllerBase
    {
        private readonly IDataCollageRepository _dataCollageRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public DataCollageController(IDataCollageRepository dataCollageRepository, UserManager<ApplicationUser> userManager)
        {
            _dataCollageRepository = dataCollageRepository;
            _userManager = userManager;
        }


        [HttpPost("CreateDataCollage")]
        public async Task<IActionResult> CreateDataCollage([FromBody] CreateDataCollages collage)
        {
            if (ModelState.IsValid)
            {
                var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var user = await _userManager.FindByNameAsync(username);

                var data = await _dataCollageRepository.AddAsync(collage, user.UserName);

                return Ok(data);
            }

            return BadRequest(ModelState);
        }


        [HttpPut("UpdateDataCollage")]
        public async Task<IActionResult> UpdateDataCollage([FromBody] UpdateDataCollages collage)
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByNameAsync(username);

            var UserName = user.UserName;

            collage.UserName = UserName; // Ensure UserName matches update target

            try
            {
                var updatedSubscription = await _dataCollageRepository.UpdateAsync(collage);

                return Ok(updatedSubscription);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Handle offer not found gracefully
            }
        }

        
        [HttpDelete("DeleteDataCollage")]
        public async Task<IActionResult> DeleteDataCollage()
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByNameAsync(username);

            var data = await _dataCollageRepository.DeleteAsync(user.UserName);

            return Ok(data);
        }


        [HttpGet("GetDataCollageDetails")]
        public async Task<IActionResult> GetDataCollageDetails()
        {
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByNameAsync(username);

            var data = await _dataCollageRepository.GetByIdAsync(user.UserName);

            return Ok(data);
        }
    }
}
