using System;
using Domain.Entites;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController:ControllerBase
    {
        private StudentService _studentService;
        public StudentController()
        {
            _studentService = new StudentService();
        }

        [HttpGet("Students")]
        public List<Student> Students()
        {
            return _studentService.GetStudents();
        }

        [HttpPost("Add")]
        public bool Add(Student student)
        {
            return _studentService.AddStudent(student);
        }


        [HttpPut("Update")]
        public bool Update(Student student)
        {
            return _studentService.UpdateStudent(student);
        }

        [HttpDelete("Delete")]
        public bool Delete(int id)
        {
            return _studentService.DeleteStudent(id);
        }

    }
}

