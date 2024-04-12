using Cyper.Models;
using Cyper.Models.Problems;

namespace Cyper.Services.Interface
{
    public interface IProblemRepository
    {
        Task<IEnumerable<GetAllProblems>> GetListAsync();

        Task<IEnumerable<GetListProblemsForCollage>> GetListAsync(string UserName);

        Task<ResponseGeneral> AddAsync(CreateProblem entity, string UserName);

        Task<ResponseGeneral> UpdateAsync(UpdateProblem entity);

        Task<ResponseGeneral> DeleteAsync(int id);
    }
}
