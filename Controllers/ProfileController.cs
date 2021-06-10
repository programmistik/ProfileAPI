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
            public ActionResult<List<Profile>> Get() =>
                _profileService.Get();

            [HttpGet("{AppUserId}", Name = "GetProfile")]
            public ActionResult<Profile> Get(string AppUserId)
            {
                var profile = _profileService.Get(AppUserId);
               

            if (profile == null)
                {
                    return NotFound();
                }

                return profile;
            }

            [HttpPost]
            public ActionResult<Profile> Create(Profile profile)
            {
                _profileService.Create(profile);

                return CreatedAtRoute("GetProfile", new { id = profile.Id.ToString() }, profile);
            }

            [HttpPut("{id}")]
            public IActionResult Update(string id, Profile profile)
            {
             //   var Profile = _profileService.Get(id);

                if (profile == null)
                {
                    return NotFound();
                }

                _profileService.Update(id, profile);

                return NoContent();
            }

            [HttpDelete("{id}")]
            public IActionResult Delete(string id)
            {
                var profile = _profileService.Get(id);

                if (profile == null)
                {
                    return NotFound();
                }

                _profileService.Remove(profile.Id);

                return NoContent();
            }
        }
    }