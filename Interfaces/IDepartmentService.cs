using FusionAPI_Framework.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FusionAPI_Framework.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartment();
        Task<Department> GetDepartmentById(int id);
        Task AddDepartment(Department department);
        Task UpdateDepartmentById(Department department);
        Task DeleteDepartment(int id);
    }
}
