using AutoMapper;
using Cyper.Data;
using Cyper.Data.Entities;
using Cyper.Models;
using Cyper.Models.Solves;
using Cyper.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cyper.Services.Repository
{
    public class SolveRepository : ISolveRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SolveRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseGeneral> AddAsync(CreateSolve entity, string UserName)
        {
            var RG = new ResponseGeneral();

            try
            {
                Solve data = new()
                {
                    Description = entity.Description,
                    ProblemId = entity.ProblemId,
                    UserName = UserName
                };

                await _context.solves.AddAsync(data);

                RG.Done = true;

                RG.Message = "Done";

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                RG.Done = false;

                RG.Message = $"An error occurred: {ex.Message}";
            }

            return RG;
        }

        public async Task<ResponseGeneral> DeleteAsync(int id)
        {
            var delData = await _context.solves.FindAsync(id);

            if (delData != null)
            {
                _context.solves.Remove(delData);

                await _context.SaveChangesAsync();

                return new ResponseGeneral { Done = true, Message = "Successful" };
            }

            return new ResponseGeneral { Message = "Not Found" };
        }

        public async Task<GetSolveDetails> GetByIdAsync(int id)
        {
            var data = await _context.solves.Where(d => d.ProblemId == id)
                .Select(d => new GetSolveDetails
                {
                    ProblemId = d.ProblemId,
                    Description = d.Description,
                    UserName = d.UserName
                }).FirstOrDefaultAsync();

            return data ?? throw new Exception("Not solve This time!");
        }

        public async Task<ResponseGeneral> UpdateAsync(UpdateSolve entity)
        {
            var RG = new ResponseGeneral();

            try
            {
                var existingOffer = await _context.solves.FindAsync(entity.Id); // Handle cases where Id is not set

                if (existingOffer == null)
                {
                    throw new ArgumentException("Solve not found with the provided Id.");
                }

                _mapper.Map(entity, existingOffer);

                _context.solves.Update(existingOffer);

                await _context.SaveChangesAsync();

                RG.Done = true;

                RG.Message = "Done";
            }
            catch (Exception ex)
            {
                RG.Done = false;

                RG.Message = $"An error occurred: {ex.Message}";
            }

            return RG;
        }
    }
}
