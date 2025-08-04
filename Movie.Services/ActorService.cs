using AutoMapper;
using Movie.Core;
using Movie.Core.DomainContracts;
using Movie.Core.DTOs;
using Movie.Core.Entities;
using Movie.Service.Contracts;

namespace Movie.Services
{
    /// <summary>
    /// Implementation of <see cref="IActorService"/> for managing actor data.
    /// </summary>
    public class ActorService : IActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ActorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<ActorDto>> GetActorsAsync(int page, int pageSize)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageSize > 100) pageSize = 100;
            if (page <= 0) page = 1;
            var actors = await _unitOfWork.Actors.GetAllAsync(page, pageSize);
            var total = actors.Count();
            var dtoList = actors.Select(a => _mapper.Map<ActorDto>(a)).ToList();
            return new PagedResult<ActorDto>
            {
                Items = dtoList,
                TotalItems = total,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize)
            };
        }

        public async Task<ActorDto?> GetActorAsync(int id)
        {
            var actor = await _unitOfWork.Actors.GetAsync(id);
            return actor == null ? null : _mapper.Map<ActorDto>(actor);
        }

        public async Task<ActorDto> CreateActorAsync(ActorDto dto)
        {
            var actor = _mapper.Map<Actor>(dto);
            _unitOfWork.Actors.Add(actor);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ActorDto>(actor);
        }

        public async Task<bool> UpdateActorAsync(int id, ActorDto dto)
        {
            var existing = await _unitOfWork.Actors.GetAsync(id);
            if (existing == null) return false;
            existing.Name = dto.Name;
            // BirthYear cannot be updated via DTO intentionally for simplicity
            _unitOfWork.Actors.Update(existing);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}