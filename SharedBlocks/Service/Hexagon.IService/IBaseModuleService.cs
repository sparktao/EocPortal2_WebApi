using Hexagon.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hexagon.IService
{
    public interface IBaseModuleService
    {
        Task<List<Base_Module>> GetModuleList();
    }
}
