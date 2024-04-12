using AutoMapper;
using Cyper.Data;
using Cyper.Data.Entities;
using Cyper.Models;
using Cyper.Models.Collages;
using Cyper.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cyper.Services.Repository
{
    public class DataCollageRepository : IDataCollageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DataCollageRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseGeneral> AddAsync(CreateDataCollages entity, string UserName)
        {
            var RG = new ResponseGeneral();

            try
            {
                Data_Collage data = new()
                {
                    Departement = entity.Departement,
                    Nom_of_points = entity.Nom_of_points,
                    Points_not_working = entity.Points_not_working,
                    Points_work = entity.Points_work,
                    Room_name = entity.Room_name,
                    Room_nom = entity.Room_nom,
                    Round = entity.Round,
                    Switch = entity.Switch,
                    UserName = UserName
                };

                await _context.data_Collages.AddAsync(data);

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

        public async Task<ResponseGeneral> DeleteAsync(string username)
        {
            var delData = await _context.solves.FindAsync(username);

            if (delData != null)
            {
                _context.solves.Remove(delData);

                await _context.SaveChangesAsync();

                return new ResponseGeneral { Done = true, Message = "Successful" };
            }

            return new ResponseGeneral { Message = "Not Found" };
        }

        public async Task<GetDataCollageDetails> GetByIdAsync(string UserName)
        {
            var data = await _context.data_Collages.Where(d => d.UserName == UserName)
                .Select(m=> new GetDataCollageDetails
                {
                    Departement = m.Departement,
                    Nom_of_points = m.Nom_of_points,
                    Points_not_working = m.Points_not_working,
                    Points_work = m.Points_work,
                    Room_name = m.Room_name,
                    Room_nom = m.Room_nom,
                    Round = m.Round,
                    Switch = m.Switch
                }).FirstOrDefaultAsync();

            return data ?? throw new Exception("Not DataCollage This time!");
        }

        public async Task<ResponseGeneral> UpdateAsync(UpdateDataCollages entity)
        {
            var RG = new ResponseGeneral();

            try
            {
                var existingOffer = await _context.data_Collages.FindAsync(entity.UserName); // Handle cases where UserName is not set

                if (existingOffer == null)
                {
                    throw new ArgumentException("DataCollage not found with the provided UserName.");
                }

                _mapper.Map(entity, existingOffer);

                _context.data_Collages.Update(existingOffer);

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
