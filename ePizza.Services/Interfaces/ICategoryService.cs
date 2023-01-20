using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ePizza.Entities.Dtos;
using ePizza.Shared.Utilities.Results.Abstract;

namespace ePizza.Services.Interfaces
{
    public interface ICategoryService
    {


        Task<IDataResult<CategoryListDto>> GetAllAsync();

    }
}
