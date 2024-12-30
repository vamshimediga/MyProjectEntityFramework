using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface IStudent
    {
        Task<List<Student>> allStudents();

        Task<Student> students(int id);

        Task<int> insert(Student student);  

        Task<int> update(Student student);  

        Task<int> delete(int id);   
    }
}
