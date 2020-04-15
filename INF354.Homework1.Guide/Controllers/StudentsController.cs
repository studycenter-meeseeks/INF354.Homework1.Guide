using System.Collections.Generic;
using System.Linq;
using INF354.Homework1.Guide.Controllers.Resources;
using INF354.Homework1.Guide.Data;
using INF354.Homework1.Guide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace INF354.Homework1.Guide.Controllers
{
    //api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        //api/students/AllStudents
        [HttpGet("AllStudents")]
        public IEnumerable<Student> AllStudents()
        {
            var students = _context
                .Students
                .ToList();

            return students;

            //throw new NotImplementedException();
        }


        [HttpGet("StudentsAndModules")]
        public IEnumerable<StudentResource> StudentsModules()
        {

            var students = _context
                .Students
                .Include(item => item.Modules);

            var studentResources = new List<StudentResource>();  //an empty list of student resources


            foreach (var student in students)
            {
                var resource = new StudentResource(); //foreach student in the db, we create a student resource object


                resource.Id = student.Id;
                resource.FirstName = student.FirstName;
                resource.LastName = student.LastName;

                /*
                 * Remember that A student can have 0...N modules.
                 * We need to iterate through all the modules that are associated with this each student
                 * Goal of action: Get Student Enrollments
                 *
                 *Each student already has a list of modules,
                 * so we do not need the studentId in modules entity
                 * this is why we make use of a resource
                 */
                foreach (var module in student.Modules)
                {
                    var moduleResource = new ModuleResource
                    {
                        Id = module.Id,
                        Name = module.Name,
                        Code = module.Code
                    };
                    resource.ModuleResources.Add(moduleResource);
                }

                studentResources.Add(resource);

            }

            return studentResources;

        }

        [HttpGet("GetStudentsFullName")]
        public IEnumerable<dynamic> GetStudentsFullName()
        {
            var students = _context.Students.Select(item => new
            {
                firstName = item.FirstName,
                lastName = item.LastName,
            }).ToList();

            return students;
        }

        //api/students/Student/3
        [HttpGet("Student/{id}")]
        public ActionResult<Student> Student(int id)
        {
            var student = _context
                .Students
                .FirstOrDefault(item => item.Id == id);

            //student was not found with the provided ID
            if (student == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(student);
            }


        }


        [HttpPost("CreateStudent")]
        public ActionResult<Student> AddStudent([FromBody]CreateStudentResource model)
        {

            if (ModelState.IsValid)
            {
                var newStudent = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                _context.Students.Add(newStudent);
                _context.SaveChanges();


                return CreatedAtAction(nameof(Student), new { id = newStudent.Id }, newStudent);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("UpdateStudent/{id}")]
        public ActionResult<Student> UpdateStudent([FromBody]CreateStudentResource model, int id)
        {

            if (ModelState.IsValid)
            {

                var studentInDb = _context.Students.Find(id); //First(item=>item.Id ==id) // FirstOrDefault(item=>item.Id ==id)

                if (studentInDb == null)
                {
                    return NotFound(); //404
                }
                else
                {
                    studentInDb.FirstName = model.FirstName;
                    studentInDb.LastName = model.LastName;
                    _context.SaveChanges();

                    return Ok(studentInDb);
                }

            }
            else
            {
                return BadRequest();
            }

        }


        //api/students/Student/3
        [HttpDelete("DeleteStudent/{id}")]
        public ActionResult<Student> DeleteStudent(int id)
        {
            var student = _context
                .Students
                .FirstOrDefault(item => item.Id == id);

            //student was not found with the provided ID
            if (student == null)
            {
                return NotFound(); //404
            }
            else
            {

                _context.Students.Remove(student);
                _context.SaveChanges();

                return Ok();
            }


        }



    }
}