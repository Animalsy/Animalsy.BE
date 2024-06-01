using Animalsy.BE.Services.VisitAPI.Data;
using Animalsy.BE.Services.VisitAPI.Models;
using Animalsy.BE.Services.VisitAPI.Models.Dto;
using Animalsy.BE.Services.VisitAPI.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Animalsy.BE.Services.VisitAPI.Repository
{
    public class VisitRepository(AppDbContext dbContext, IApiService apiService, IMapper mapper) : IVisitRepository
    {
        public async Task<VisitResponseDto> GetByIdAsync(Guid visitId)
        {
            var visit = await dbContext.Visits.FirstOrDefaultAsync(c => c.Id == visitId);

            return visit != null
                ? await new VisitResponseBuilder(apiService, mapper.Map<VisitDto>(visit))
                    .WithContractor()
                    .WithCustomer()
                    .WithPet()
                    .WithProduct()
                    .WithVendor()
                    .BuildAsync()
                : new VisitResponseDto();
        }

        public async Task<IEnumerable<VisitResponseDto>> GetByVendorIdAsync(Guid vendorId)
        {
            var visits = await dbContext.Visits
                .Where(v => v.VendorId == vendorId)
                .ToListAsync();

            if (!visits.IsNullOrEmpty()) return [];

            return await BuildResultsAsync(visits).ConfigureAwait(false);
        }

        public async Task<IEnumerable<VisitResponseDto>> GetByCustomerIdAsync(Guid customerId)
        {
            var visits = await dbContext.Visits
                .Where(v => v.CustomerId == customerId)
                .ToListAsync();

            if (!visits.IsNullOrEmpty()) return [];

            return await BuildResultsAsync(visits).ConfigureAwait(false);
        }

        public async Task<Guid> CreateAsync(CreateVisitDto visitDto)
        {
            var visit = mapper.Map<Visit>(visitDto);
            await dbContext.AddAsync(visit);
            await dbContext.SaveChangesAsync();
            return visit.Id;
        }

        public async Task<bool> TryUpdateAsync(UpdateVisitDto visitDto)
        {
            var existingVisit = await dbContext.Visits.FirstOrDefaultAsync(v => v.Id == visitDto.Id);
            if (existingVisit == null) return false;

            existingVisit.Comment = visitDto.Comment;
            existingVisit.State = visitDto.State;
            existingVisit.Date = visitDto.Date;

            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TryDeleteAsync(Guid visitId)
        {
            var existingVisit = await dbContext.Visits.FirstOrDefaultAsync(v => v.Id == visitId);
            if (existingVisit == null) return false;

            dbContext.Remove(existingVisit);
            await dbContext.SaveChangesAsync();
            return true;
        }

        private async Task<IEnumerable<VisitResponseDto>> BuildResultsAsync(IEnumerable<Visit> visits)
        {
            var tasks = visits.Select(visit =>
                new VisitResponseBuilder(apiService, mapper.Map<VisitDto>(visit))
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
