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


        /// <summary>
        /// ---default call for all students
        /// </summary>
        /// <param name="res"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StudentModel>> GetAllStudentsDefaultAsync(HttpResponse res, CursorParams @params)
        {
            var stu = await _context.students.ToListAsync();
            var leng = stu.ToArray().Length;

            


            stu = stu.OrderByDescending(x=>x.Id)
                .Where(x => x.Id < @params.Cursor)
                 .Take(@params.Count)
                 .ToList();
            var nextCursor = stu.Any() ? stu.LastOrDefault().Id : 0;
            //var nextCursor = stu.LastOrDefault().Roll;
            if (nextCursor == leng)
            {
                nextCursor = 0;
            }

            res.Headers.Add("X-Paggination", $"(Next Cursor)={nextCursor}");
            // res.Headers.Append("count", $"{stu.}");

            var records = stu.Select(x => new StudentModel()
            {
                Id=x.Id,
                Roll = x.Roll,
                Name = x.Name,
                FathersName = x.FathersName,
                Mothersname = x.Mothersname,
                Class = x.Class,
                City = x.City,
                Address = x.Address
            });

            return records;
        }
        ///---- implementing sorting
        /////1--- asending
        ///0---desending

        public async Task<IEnumerable<StudentModel>> GetAllStudentsDesendAsync(HttpResponse res,CursorParams @params,StudentModel student)
        {
            var stu = await _context.students.ToListAsync();
            var leng = stu.ToArray().Length;
            
            if (student.Roll == 1)
            {
                stu = stu.OrderBy(x => x.Roll).ToList();
            }
            else
            {
                stu = stu.OrderByDescending(x => x.Roll).ToList();
            }
            

            stu=stu                
                .Where(x => x.Id > @params.Cursor)
                 .Take(@params.Count)
                 .ToList();
             var nextCursor = stu.Any() ? stu.LastOrDefault().Id : 0;
            //var nextCursor = stu.LastOrDefault().Roll;
            if (nextCursor == leng)
            {
                nextCursor = 0;
            }

            res.Headers.Add("X-Paggination", $"(Next Cursor)={nextCursor}");
           // res.Headers.Append("count", $"{stu.}");

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

        public async Task<IEnumerable<StudentModel>> GetAllStudentsAsendAsync(HttpResponse res, CursorParams @params,StudentModel student)
        {
            var stu = await _context.students.ToListAsync();
            var leng = stu.ToArray().Length;

            stu = stu
                 .OrderByDescending(x => x.Id)
                 .Where(x => x.Roll < @params.Cursor)
                 .Take(@params.Count)
                 .ToList();
            var nextCursor = stu.Any() ? stu.LastOrDefault().Id : 0;
            //var nextCursor = stu.LastOrDefault().Roll;
            if (nextCursor == leng)
            {
                nextCursor = 0;
            }

            res.Headers.Add("X-Paggination", $"(Next Cursor)={nextCursor}");
            // res.Headers.Append("count", $"{stu.}");

            var records = stu.Select(x => new StudentModel()
            {
                Roll = x.Roll,
                Name = x.Name,
                FathersName = x.FathersName,
                Mothersname = x.Mothersname,
                Class = x.Class,
                City = x.City,
                Address = x.Address
            });

            return records;
        }

        public async Task<IEnumerable<StudentModel>> GetStudentsAsendAsync(HttpResponse res, CursorParams @params,SearchModel search)
        {           
            var stu = await _context.students.

                ToListAsync();
            
            if (search.roll != 0)
            {
                stu = stu.Where(x => x.Roll == search.roll).ToList();
            }
            if (search.Class != 0)
            {
                stu = stu.Where(x => x.Class == search.Class).ToList();
            }
            if (search.name != null)
            {
                stu = stu.Where(x => x.Name.Contains(search.name, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (search.fname != null)
            {
                stu = stu.Where(x => x.FathersName.Contains(search.fname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (search.mname != null)
            {
                stu = stu.Where(x => x.Mothersname.Contains(search.mname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (search.city != null)
            {
                stu = stu.Where(x => x.City.Contains(search.city, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (search.add != null)
            {        
                    stu = stu.Where(x => x.Address.Contains(search.add, StringComparison.OrdinalIgnoreCase)).ToList();       
            }

            stu = stu.OrderBy(x => x.Id)
                 .Where(x => x.Id > @params.Cursor)
                 .Take(@params.Count)
                 .ToList();
           

            var nextCursor = stu.Any() ? stu.LastOrDefault().Roll : 0;
            

             res.Headers.Add("X-Paggination", $"{nextCursor}");
           // res.Headers.Append("nextCur", $"{nextCursor}");

            var records = stu.Select(x => new StudentModel()
            {
                Roll = x.Roll,
                Name = x.Name,
                FathersName = x.FathersName,
                Mothersname = x.Mothersname,
                Class = x.Class,
                City = x.City,
                Address = x.Address
            });
            

            return records;
        }

        public async Task<IEnumerable<StudentModel>> GetStudentsDesendAsync(HttpResponse res, CursorParams @params, SearchModel search)
        {
            var stu = await _context.students.

                ToListAsync();

            if (search.roll != 0)
            {
                stu = stu.Where(x => x.Roll == search.roll).ToList();
            }
            if (search.Class != 0)
            {
                stu = stu.Where(x => x.Class == search.Class).ToList();
            }
            if (search.name != null)
            {
                stu = stu.Where(x => x.Name.Contains(search.name, System.StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            if (search.fname != null)
            {
                stu = stu.Where(x => x.FathersName.Contains(search.fname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (search.mname != null)
            {
                stu = stu.Where(x => x.Mothersname.Contains(search.mname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (search.city != null)
            {
                stu = stu.Where(x => x.City.Contains(search.city, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (search.add != null)
            {
                stu = stu.Where(x => x.Address.Contains(search.add, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            stu = stu.OrderByDescending(x => x.Id)
                 .Where(x => x.Id < @params.Cursor)
                 .Take(@params.Count)
                 .ToList();


            var nextCursor = stu.Any() ? stu.LastOrDefault().Roll : 0;


            res.Headers.Add("X-Paggination", $"{nextCursor}");
            // res.Headers.Append("nextCur", $"{nextCursor}");

            var records = stu.Select(x => new StudentModel()
            {
                Roll = x.Roll,
                Name = x.Name,
                FathersName = x.FathersName,
                Mothersname = x.Mothersname,
                Class = x.Class,
                City = x.City,
                Address = x.Address
            });


            return records;
        }
        public async Task<IEnumerable<StudentModel>> GetStudentById(int roll)
        {
            var stu = await _context.students.ToListAsync();
            //test----
            stu = stu.OrderByDescending(x=>x.Roll).ToList();
            var  c= stu.Count;
            //-----
            var record = stu
                        .Where(x => x.Roll == roll)
                        .Select(x => new StudentModel()
                        {
                            Id = x.Id,
                            Roll = stu[c-1].Roll,
                            Name = stu[c-1].Name,
                            Class = x.Class,
                            FathersName=x.FathersName,
                            Mothersname=x.Mothersname,
                            City=x.City,
                            Address=x.Address
                        });
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
