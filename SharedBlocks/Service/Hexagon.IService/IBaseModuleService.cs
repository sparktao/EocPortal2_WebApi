using Hexagon.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.IService
{
    public interface IBaseModuleService
    {
        Task<IEnumerable<Base_Module>> GetModuleList();
        Task<Base_Module> GetModuleById(string id);
    }
}
