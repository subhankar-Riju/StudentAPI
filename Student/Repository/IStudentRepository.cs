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
        Task<IEnumerable<StudentModel>> GetAllStudentsDefaultAsync(HttpResponse res, CursorParams @params);
       Task<IEnumerable<StudentModel>> GetAllStudentsAsendAsync(HttpResponse res,CursorParams @params,StudentModel student);
        Task<IEnumerable<StudentModel>> GetAllStudentsDesendAsync(HttpResponse res, CursorParams @params, StudentModel student);
        Task<IEnumerable<StudentModel>> GetStudentById(int StudentId);
        Task PostStudentAsync(int id, StudentModel student);
        Task PutStudentAsync(int id, StudentModel studentModel);
        Task PatchStudentAsync(int id, JsonPatchDocument student);
        Task<IEnumerable<StudentModel>> GetStudentsAsendAsync(HttpResponse res, CursorParams @params, SearchModel search);
        Task<IEnumerable<StudentModel>> GetStudentsDesendAsync(HttpResponse res, CursorParams @params, SearchModel search);
        Task DeleteStudent(int roll);

        Task<IEnumerable<StudentModel>> StudentAsending(HttpResponse res, CursorParams @params, StudentModel student);


    }
}
