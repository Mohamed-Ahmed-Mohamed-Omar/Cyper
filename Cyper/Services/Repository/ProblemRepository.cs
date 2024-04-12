using AutoMapper;
using Cyper.Data;
using Cyper.Data.Entities;
using Cyper.Models;
using Cyper.Models.Problems;
using Cyper.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cyper.Services.Repository
{
    public class ProblemRepository : IProblemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProblemRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseGeneral> AddAsync(CreateProblem entity, string UserName)
        {
            var RG = new ResponseGeneral();

            try
            {
                Problem data = new()
                {
                    Description = entity.Description,
                    UserName = UserName
                };

                await _context.problems.AddAsync(data);

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

        public async Task<ResponseGeneral> DeleteAsync(string UserName)
        {
            var delData = await _context.problems.FindAsync(UserName);

            if (delData != null)
            {
                _context.problems.Remove(delData);

                await _context.SaveChangesAsync();

                return new ResponseGeneral { Done = true, Message = "Successful" };
            }

            return new ResponseGeneral { Message = "Not Found" };
        }

        public async Task<IEnumerable<GetAllProblems>> GetListAsync()
        {
            var problems = await _context.problems.ToListAsync();

            var mappedProblemss = _mapper.Map<IEnumerable<GetAllProblems>>(problems);

            return mappedProblemss;
        }

        public async Task<IEnumerable<GetListProblemsForCollage>> GetListAsync(string UserName)
        {
            var data = await _context.problems.Where(d => d.UserName == UserName)
                .Select(m => new GetListProblemsForCollage
                {
                    Description_Problems = m.Description
                }).ToListAsync();

            return data;
        }

        public async Task<ResponseGeneral> UpdateAsync(UpdateProblem entity)
        {
            var RG = new ResponseGeneral();

            try
            {
                var existingOffer = await _context.problems.FindAsync(entity.UserName); // Handle cases where Id is not set

                if (existingOffer == null)
                {
                    throw new ArgumentException("Problem not found with the provided Id.");
                }

                _mapper.Map(entity, existingOffer);

                _context.problems.Update(existingOffer);

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
