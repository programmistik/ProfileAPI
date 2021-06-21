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
    public class SearchController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public SearchController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("{searchStr}", Name = "Search")]
        public async Task<ActionResult<List<Profile>>> GetAsync(string searchStr) =>
             await _profileService.SearchAsync(searchStr);
    }
}
