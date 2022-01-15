using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository =studentRepository;
        }

        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var records = await _studentRepository.GetAllStudentsAsync();

            return Ok(records);
        }
    }
}
