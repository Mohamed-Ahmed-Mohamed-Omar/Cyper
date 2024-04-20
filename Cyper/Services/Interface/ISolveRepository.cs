using Cyper.Models;
using Cyper.Models.Solves;

namespace Cyper.Services.Interface
{
    public interface ISolveRepository
    {
        Task<GetSolveDetails> GetByIdAsync(string Username);

        Task<ResponseGeneral> AddAsync(CreateSolve entity, string UserName);

        Task<ResponseGeneral> UpdateAsync(UpdateSolve entity);

        Task<ResponseGeneral> DeleteAsync(int id);

        Task<IEnumerable<GetAllSolves>> GetAllSolves();
    }
}
