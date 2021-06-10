using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI.Services
{
    public interface IProfileService
    {
        List<Profile> Get();
        Profile Get(string id);

        Profile Create(Profile prof);
        void Update(string id, Profile profIn);
        List<Profile> Search(string str);

        void Remove(Profile profIn);
        void Remove(string id);
    }
}
