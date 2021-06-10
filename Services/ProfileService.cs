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

        public List<Profile> Get() =>
            _profile.Find(prof => true).ToList();

        public Profile Get(string id) =>
            _profile.Find<Profile>(prof => prof.AppUserId == id).FirstOrDefault();

        public Profile Create(Profile prof)
        {
            _profile.InsertOne(prof);
            return prof;
        }

        public void Update(string id, Profile profIn)
        {
            _profile.ReplaceOne(prof => prof.AppUserId == id, profIn);
        }

        public void Remove(Profile profIn) =>
            _profile.DeleteOne(prof => prof.AppUserId == profIn.Id);

        public void Remove(string id) =>
            _profile.DeleteOne(prof => prof.AppUserId == id);

        public List<Profile> Search(string str) =>
            _profile.Find(prof => prof.Name.Contains(str)).ToList();
    }
}
