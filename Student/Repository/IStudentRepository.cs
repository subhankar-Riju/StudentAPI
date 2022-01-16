using Student.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Repository
{
    public interface IStudentRepository
    {
       Task<List<StudentModel>> GetAllStudentsAsync();
        Task<List<StudentModel>> GetStudentById(int StudentId);
        Task PostStudentAsync(int id, StudentModel student);
        Task PutStudentAsync(int id, StudentModel studentModel);

    }
}
