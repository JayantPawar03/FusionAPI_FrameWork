
using FusionAPI_Framework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FusionAPI_Framework.Interfaces
{
    public interface ILabourService
    {      
        Task<List<Labour>> GetAllLabour();      
        Task<Labour> GetLabourId(int id);        
        Task AddLabour(Labour labour);    
        Task UpdateLabour(Labour labour);
        Task DeleteLabour(int id);
    }
}
