using cwiczenia7.Models;
using cwiczenia7.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cwiczenia7.Services
{
    public class TripService : ITripService
    {
        private readonly cw7Context _context;
        public TripService(cw7Context context)
        {
            _context = context;
        }
        public Task<bool> ClientExists(string pesel)
        {
            return _context.Clients.AnyAsync(c => c.Pesel.Equals(pesel));
        }

        public async Task<bool> ClientHasTrips(int id)
        {
            return await _context.Trips
                .Include(e => e.ClientTrips)
                .ThenInclude(e => e.IdClient == id)
                .AnyAsync();
        }

        public Task<bool> ClientTripEntry(int tripId, int clientId)
        {
            return _context.ClientTrips.AnyAsync(e => e.IdClient == clientId && e.IdTrip == tripId);
        }

        public async Task<Client> DeleteClient(int id)
        {
            var client = _context.Clients.Where(e => e.IdClient==id).FirstOrDefault();
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public Task<Client> GetClient(string pesel)
        {
            return _context.Clients.Where(e=>e.Pesel==pesel).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<GetTips>> GetTips()
        {
            return await _context.Trips
                .Include(e => e.ClientTrips)
                .ThenInclude(e => e.IdClientNavigation)
                .Include(e => e.CountryTrips)
                .ThenInclude(e => e.IdCountryNavigation)
                .Select(e => new GetTips
                {
                    Name = e.Name,
                    Description = e.Description,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    MaxPeople = e.MaxPeople,
                    Countries = e.CountryTrips.Select(e => new GetTips.Country
                    {
                        Name = e.IdCountryNavigation.Name
                    }),
                    Clients = e.ClientTrips.Select(e => new GetTips.Client
                    {
                        FirstName = e.IdClientNavigation.FirstName,
                        LastName = e.IdClientNavigation.LastName,
                    })
                }).ToListAsync();
        }

        public async Task<Client> PostClient(PostClient post)
        {
            var client = _context.Clients.AddAsync(new Client
            {
                IdClient = (await _context.Clients.MaxAsync(e => e.IdClient)) + 1,
                FirstName = post.FirstName,
                LastName = post.LastName,
                Email = post.Email,
                Telephone = post.Telephone,
                Pesel = post.Pesel
            }).Result.Entity;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<ClientTrip> PostClientTrip(PostClientTrip postTrip)
        {
            var clientTrip = _context.ClientTrips.AddAsync(new ClientTrip
            {
                IdClient = postTrip.IdClient,
                IdTrip = postTrip.IdTrip,
                RegisteredAt = DateTime.Now
            });
            await _context.SaveChangesAsync();
            return clientTrip.Result.Entity;
        }

        public Task<bool> TripExists(int id)
        {
            return _context.Trips.AnyAsync(e => e.IdTrip == id);
        }
    }
}
