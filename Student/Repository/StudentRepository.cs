using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Repository
{
    public class StudentRepository: IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<List<StudentModel>> GetAllStudentsAsync()
        {
            var records =await  _context.students.Select(x => new StudentModel() { 
            Id=x.Id,
            Name=x.Name,
            Class=x.Class,
            Roll=x.Roll
            }).ToListAsync();

            return records;
        }


        public async Task<List<StudentModel>> GetStudentById(int StudentId)
        {
            var record = await _context.students
                        .Where(x => x.Id == StudentId)
                        .Select(x => new StudentModel()
                        {
                            Id = x.Id,
                            Roll = x.Roll,
                            Name = x.Name,
                            Class = x.Class
                        }).ToListAsync();
            return record;

        }


        public async Task PostStudentAsync(int id,StudentModel student)
        {
            var students= new Students()
            {
                Class = student.Class,
                Name = student.Name,
                Roll = student.Roll
            };

            await _context.students.AddAsync(students);
            await _context.SaveChangesAsync();

            
        }

        public async Task PutStudentAsync(int id,StudentModel studentModel)
        {
            var student = await _context.students.FindAsync(id);
            if(student != null)
            {
                student.Name = studentModel.Name;
                student.Roll = studentModel.Roll;
                student.Class = studentModel.Class;

                await _context.SaveChangesAsync();
            }
        }

        public async Task PatchStudentAsync(int id,JsonPatchDocument student)
        {
            var Student = await _context.students.FindAsync(id);

            student.ApplyTo(Student);
            await _context.SaveChangesAsync();
        }

    }

   
}
