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

    }
}
