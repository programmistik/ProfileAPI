using Microsoft.AspNetCore.Mvc;
using ProfileAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ProfileController : ControllerBase
        {
            private readonly IProfileService _profileService;

            public ProfileController(IProfileService profileService)
            {
                _profileService = profileService;
            }

            [HttpGet]
            public async Task<ActionResult<List<Profile>>> GetAsync() =>
                await _profileService.GetAsync();

            [HttpGet("{AppUserId}", Name = "GetProfile")]
            public async Task<ActionResult<Profile>> GetAsync(string AppUserId)
            {
                var profile = await _profileService.GetAsync(AppUserId);

                if (profile == null)
                        return NotFound();

                return profile;
            }
             
        

            


        [HttpPost]
            public async Task<ActionResult<Profile>> PostAsync(Profile profile)
            {
                await _profileService.CreateAsync(profile);

                return CreatedAtRoute("GetProfile", new { id = profile.Id.ToString() }, profile);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateAsync(string id, Profile profile)
            {

                if (profile == null)
                    return NotFound();

                await _profileService.UpdateAsync(id, profile);

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteAsync(string id)
            {
                var profile = await _profileService.GetAsync(id);

                if (profile == null)
                    return NotFound();

                await _profileService.RemoveAsync(profile.Id);

                return NoContent();
            }
        }
    }