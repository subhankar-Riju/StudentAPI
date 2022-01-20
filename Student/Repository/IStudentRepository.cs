using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Student.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Repository
{
    public interface IStudentRepository
    {
       Task<IEnumerable<StudentModel>> GetAllStudentsAsync(HttpResponse res,CursorParams @params);
        Task<List<StudentModel>> GetStudentById(int StudentId);
        Task PostStudentAsync(int id, StudentModel student);
        Task PutStudentAsync(int id, StudentModel studentModel);
        Task PatchStudentAsync(int id, JsonPatchDocument student);

        Task DeleteStudent(int roll);


    }
}
