using AutoMapper;
using Cyper.Data.Entities;
using Cyper.Models.Collages;
using Cyper.Models.Problems;
using Cyper.Models.Solves;

namespace Cyper.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        { 
            CreateMap<Solve, CreateSolve>().ReverseMap();
            CreateMap<Solve, UpdateSolve>().ReverseMap();
            CreateMap<Solve, GetAllSolves>().ReverseMap();
            CreateMap<Solve, GetSolveDetails>().ReverseMap();

            CreateMap<Problem, CreateProblem>().ReverseMap();
            CreateMap<Problem, UpdateProblem>().ReverseMap();
            CreateMap<Problem, GetAllProblems>().ReverseMap();
            CreateMap<Problem, GetListProblemsForCollage>().ReverseMap();

            CreateMap<Data_Collage, CreateDataCollages>().ReverseMap();
            CreateMap<Data_Collage, UpdateDataCollages>().ReverseMap();
            CreateMap<Data_Collage, GetDataCollageDetails>().ReverseMap();
        }
    }
}
