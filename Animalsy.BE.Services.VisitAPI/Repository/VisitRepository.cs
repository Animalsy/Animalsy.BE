using Animalsy.BE.Services.VisitAPI.Data;
using Animalsy.BE.Services.VisitAPI.Models;
using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Repository.Builder;
using Animalsy.BE.Services.VisitAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Animalsy.BE.Services.VisitAPI.Repository
{
    public class VisitRepository : IVisitRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IApiService _apiService;
        private readonly IMapper _mapper;

        public VisitRepository(AppDbContext dbContext, IApiService apiService, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<VisitResponseDto> GetByIdAsync(Guid visitId)
        {
            var visit = await _dbContext.Visits.FirstOrDefaultAsync(c => c.Id == visitId);

            return visit != null
                ? await new VisitResponseBuilder(_apiService, _mapper.Map<VisitDto>(visit))
                    .WithContractor()
                    .WithCustomer()
                    .WithPet()
                    .WithProduct()
                    .WithVendor()
                    .BuildAsync()
                : null;
        }

        public async Task<IEnumerable<VisitResponseDto>> GetByVendorIdAsync(Guid vendorId)
        {
            var visits = await _dbContext.Visits
                .Where(v => v.VendorId == vendorId)
                .ToListAsync();

            if (!visits.IsNullOrEmpty()) return [];

            return await BuildResultsAsync(visits).ConfigureAwait(false);
        }

        public async Task<IEnumerable<VisitResponseDto>> GetByCustomerIdAsync(Guid customerId)
        {
            var visits = await _dbContext.Visits
                .Where(v => v.CustomerId == customerId)
                .ToListAsync();

            if (!visits.IsNullOrEmpty()) return [];

            return await BuildResultsAsync(visits).ConfigureAwait(false);
        }

        public async Task<Guid> CreateAsync(CreateVisitDto visitDto)
        {
            var visit = _mapper.Map<Visit>(visitDto);
            await _dbContext.AddAsync(visit);
            await _dbContext.SaveChangesAsync();
            return visit.Id;
        }

        public async Task<bool> TryUpdateAsync(UpdateVisitDto visitDto)
        {
            var existingVisit = await _dbContext.Visits.FirstOrDefaultAsync(v => v.Id == visitDto.Id);
            if (existingVisit == null) return false;

            existingVisit.Comment = visitDto.Comment;
            existingVisit.State = visitDto.State;
            existingVisit.Date = visitDto.Date;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid visitId)
        {
            var existingVisit = await _dbContext.Visits.FirstOrDefaultAsync(v => v.Id == visitId);
            if (existingVisit == null) return false;

            _dbContext.Remove(existingVisit);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<IEnumerable<VisitResponseDto>> BuildResultsAsync(IEnumerable<Visit> visits)
        {
            var tasks = visits.Select(visit =>
                new VisitResponseBuilder(_apiService, _mapper.Map<VisitDto>(visit))
                    .WithContractor()
                    .WithCustomer()
                    .WithPet()
                    .WithProduct()
                    .WithVendor()
                    .BuildAsync());

            return await Task.WhenAll(tasks);
        }
    }
}
