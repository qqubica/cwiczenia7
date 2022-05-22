using cwiczenia7.Models;
using cwiczenia7.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cwiczenia7.Services
{
    public interface ITripService   
    {
        public Task<IEnumerable<GetTips>> GetTips();
        public Task<bool> ClientHasTrips(int id);
        public Task<Client> DeleteClient(int id);
        public Task<bool> ClientExists(string pesel);
        public Task<Client> PostClient(PostClient post);
        public Task<Client> GetClient(string pesel);
        public Task<ClientTrip> PostClientTrip(PostClientTrip postTrip);
        public Task<bool> TripExists(int id);
        public Task<bool> ClientTripEntry(int tripId, int clientId);
    }
}
