using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Services
{
    public interface IProfileService
    {
        Task<List<Profile>> GetAsync();
        Task<Profile> GetAsync(string id);

        Task<Profile> CreateAsync(Profile prof);
        Task UpdateAsync(string id, Profile profIn);
        Task<List<Profile>> SearchAsync(string str);

        Task RemoveAsync(Profile profIn);
        Task RemoveAsync(string id);
    }
}
