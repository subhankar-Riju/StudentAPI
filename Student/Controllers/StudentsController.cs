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
        //1--assending
        //0--decending
        [HttpGet("all/{Count}/{Cursor}")]
        public async Task<IActionResult> GetAllStudentsAsync([FromRoute]CursorParams @params)
        {
            
            var records = await _studentRepository.GetAllStudentsDefaultAsync(Response, @params);
            return Ok(records);

        }

        //1--assending
        //-1--decending
        [HttpGet("srch/{Count}/{Cursor}")]
        public async Task<IActionResult> GetStudentsAsync([FromRoute]int Count, [FromRoute] int Cursor,[FromQuery]int order, [FromQuery]SearchModel search)
        {
            var @params = new CursorParams()
            {
                Count = Count,
                Cursor = Cursor
            };
           
            
           
            if(order==1)
            {
                var records = await _studentRepository.GetStudentsAsendAsync(Response, @params,search);
                return Ok(records);
            }
            else
            {
                var records = await _studentRepository.GetStudentsDesendAsync(Response, @params, search);
                return Ok(records);
            }


        }

        [HttpGet("{roll}")]

        public async Task<IActionResult> GetStudentByIdAsync([FromRoute] int roll)
        {
            var record = await _studentRepository.GetStudentById(roll);

            return Ok(record);
        }

        [HttpGet("{Count}/{Cursor}")]
        public async Task<IActionResult> StudentsOrderby([FromRoute]CursorParams @params,[FromQuery]StudentModel student)
        {
            var result = await _studentRepository.StudentAsending(Response, @params, student);

            return Ok(result);
        }

        [HttpPost("{roll}")]
        public async Task<IActionResult> PostStudent([FromRoute] int roll, [FromBody] StudentModel stu)
        {
            await _studentRepository.PostStudentAsync(roll, stu);

            return Ok();
        }


        [HttpPut("{roll}")]
        public async Task<IActionResult> PutStudent([FromRoute] int roll, [FromBody] StudentModel student)
        {
            await _studentRepository.PutStudentAsync(roll, student);

            return Ok();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchStudent([FromRoute] int id, [FromBody] JsonPatchDocument student)
        {
            await _studentRepository.PatchStudentAsync(id, student);
            return Ok();

        }
        [HttpDelete("{roll}")]
        public async Task<IActionResult> DeleteStudentAsync(int roll)
        {
            await _studentRepository.DeleteStudent(roll);
            return Ok();
        }

        
    }
}
