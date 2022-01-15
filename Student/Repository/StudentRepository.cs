﻿using Microsoft.EntityFrameworkCore;
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
    }

   
}
