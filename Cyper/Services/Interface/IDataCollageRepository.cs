﻿using Cyper.Models;
using Cyper.Models.Collages;

namespace Cyper.Services.Interface
{
    public interface IDataCollageRepository
    {
        Task<GetDataCollageDetails> GetByIdAsync(string UserName);

        Task<ResponseGeneral> AddAsync(CreateDataCollages entity, string UserName);

        Task<ResponseGeneral> UpdateAsync(UpdateDataCollages entity);

        Task<ResponseGeneral> DeleteAsync(string UserName);
    }
}
