using Animalsy.BE.Services.VisitAPI.Data;
using Animalsy.BE.Services.VisitAPI.Models;
using Animalsy.BE.Services.VisitAPI.Models.Dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Animalsy.BE.Services.VisitAPI.Repository
{
    public class VisitRepository(AppDbContext dbContext, IMapper mapper) : IVisitRepository
    {
        public async Task<VisitDto> GetByIdAsync(Guid visitId)
        {
            var result = await dbContext.Visits.FirstOrDefaultAsync(c => c.Id == visitId);
            return mapper.Map<VisitDto>(result);
        }

        public async Task<IEnumerable<VisitDto>> GetByVendorIdAsync(Guid vendorId)
        {
            var results = await dbContext.Visits
                .Where(v => v.VendorId == vendorId)
                .ToListAsync();

            return mapper.Map<IEnumerable<VisitDto>>(results);
        }

        public async Task<IEnumerable<VisitDto>> GetByCustomerIdAsync(Guid customerId)
        {
            var results = await dbContext.Visits
                .Where(v => v.CustomerId == customerId)
                .ToListAsync();

            return mapper.Map<IEnumerable<VisitDto>>(results);
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
    }
}
