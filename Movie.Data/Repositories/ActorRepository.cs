using Microsoft.EntityFrameworkCore;
using Movie.Core.DomainContracts;
using Movie.Core.Entities;

namespace Movie.Data.Repositories
{
    /// <summary>
    /// Concrete repository for actors.
    /// </summary>
    public class ActorRepository : IActorRepository
    {
        private readonly MovieContext _context;

        public ActorRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Actor>> GetAllAsync(int page, int pageSize)
        {
            return await _context.Actors
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Actor?> GetAsync(int id)
        {
            return await _context.Actors.FindAsync(id);
        }

        public void Add(Actor actor)
        {
            _context.Actors.Add(actor);
        }

        public void Update(Actor actor)
        {
            _context.Actors.Update(actor);
        }
    }
}