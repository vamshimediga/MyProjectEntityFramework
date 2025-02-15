using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IDepartment
    {
        Task<List<Department>> GetDepartments();

        Task<Department> GetDepartmentById(int id); 

        Task<int> Insert(Department department);

        Task<bool> Update(Department department);


        Task<bool> Delete(int id);
 
    }
}
