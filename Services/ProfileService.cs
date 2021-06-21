using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using ProfileAPI.Models;

namespace ProfileAPI.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMongoCollection<Profile> _profile;

        public ProfileService(IDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _profile = database.GetCollection<Profile>(settings.ProfileCollectionName);
        }

        public async Task<List<Profile>> GetAsync()
        {
            var coll = await _profile.FindAsync(prof => true);
            return await coll.ToListAsync();
        }

        public async Task<Profile> GetAsync(string id)
        {
            var coll = await _profile.FindAsync(prof => prof.AppUserId == id);
            return await coll.FirstOrDefaultAsync();
        }

        public async Task<Profile> CreateAsync(Profile prof)
        {
            await _profile.InsertOneAsync(prof);
            return prof;
        }

        public async Task UpdateAsync(string id, Profile profIn) =>
            await _profile.ReplaceOneAsync(prof => prof.AppUserId == id, profIn);

        public async Task RemoveAsync(Profile profIn) =>
            await _profile.DeleteOneAsync(prof => prof.AppUserId == profIn.Id);

        public async Task RemoveAsync(string id) =>
            await _profile.DeleteOneAsync(prof => prof.AppUserId == id);

        public async Task<List<Profile>> SearchAsync(string str)
        {
            var coll = await _profile.FindAsync(prof => prof.Name.ToLower().Contains(str.ToLower()));
            return await coll.ToListAsync();
        }
    }
}
