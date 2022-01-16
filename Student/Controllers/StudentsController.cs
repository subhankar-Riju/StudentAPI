using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Student.Model;
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
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var records = await _studentRepository.GetAllStudentsAsync();

            return Ok(records);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] int id)
        {
            var record = await _studentRepository.GetStudentById(id);

            return Ok(record);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> PostStudent([FromRoute] int id, [FromBody] StudentModel stu)
        {
            await _studentRepository.PostStudentAsync(id, stu);

            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent([FromRoute] int id,[FromBody]StudentModel student)
        {
            await _studentRepository.PutStudentAsync(id, student);

            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchStudent([FromRoute] int id, [FromBody]JsonPatchDocument student)
        {
            await _studentRepository.PatchStudentAsync(id, student);
            return Ok();

        }
    }
}
