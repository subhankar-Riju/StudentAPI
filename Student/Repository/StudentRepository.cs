using Microsoft.AspNetCore.Http;
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

        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsync(HttpResponse res,CursorParams @params)
        {
            /* var records =await  _context.students.Select(x => new StudentModel() { 
             Id=x.Id,
             Name=x.Name,
             Class=x.Class,
             Roll=x.Roll,
             FathersName=x.FathersName,
             Mothersname=x.Mothersname,
             Address=x.Address,
             City=x.City
             }).ToListAsync();*/

            var stu = await _context.students
                 .OrderBy(x => x.Roll)
                 .Where(x => x.Roll > @params.Cursor)
                 .Take(@params.Count)
                 .ToListAsync();
            var nextCursor = stu.Any() ? stu.LastOrDefault()?.Roll : 0;

            res.Headers.Add("X-Paggination", $"(Next Cursor)={nextCursor}");

            var records = stu.Select(x => new StudentModel()
            {
                Roll=x.Roll,
                Name=x.Name,
                FathersName=x.FathersName,
                Mothersname=x.Mothersname,
                Class=x.Class,
                City=x.City,
                Address=x.Address
                
            });

            return records;
        }


        public async Task<List<StudentModel>> GetStudentById(int roll)
        {
            var record = await _context.students
                        .Where(x => x.Roll == roll)
                        .Select(x => new StudentModel()
                        {
                            Id = x.Id,
                            Roll = x.Roll,
                            Name = x.Name,
                            Class = x.Class,
                            FathersName=x.FathersName,
                            Mothersname=x.Mothersname,
                            City=x.City,
                            Address=x.Address
                        }).ToListAsync();
            return record;

        }


        public async Task PostStudentAsync(int roll,StudentModel student)
        {
            var students= new Students()
            {
                Mothersname=student.Mothersname,
                FathersName=student.FathersName,
                Class = student.Class,
                Name = student.Name,
                Roll = student.Roll,
                City=student.City,
                Address=student.Address
               
            };

            await _context.students.AddAsync(students);
            await _context.SaveChangesAsync();

            
        }

        public async Task PutStudentAsync(int roll,StudentModel studentModel)
        {
            var student = await _context.students.FindAsync(roll);
            if(student != null)
            {
                student.Name = studentModel.Name;
                student.Roll = studentModel.Roll;
                student.Class = studentModel.Class;
                student.FathersName = studentModel.FathersName;
                student.Mothersname = studentModel.Mothersname;
                student.City = studentModel.City;
                student.Address = studentModel.Address;

                await _context.SaveChangesAsync();
            }
        }

        public async Task PatchStudentAsync(int id,JsonPatchDocument student)
        {
            var Student = await _context.students.FindAsync(id);

            student.ApplyTo(Student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int roll)
        {
            var book = await _context.students.Where(x => x.Roll == roll).FirstOrDefaultAsync();
            if (book != null)
            {
                _context.students.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

    }

   
}
