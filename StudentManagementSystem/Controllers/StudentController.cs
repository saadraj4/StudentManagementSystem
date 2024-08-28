using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly MongoDBService _mongoDBService;

    public StudentsController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public ActionResult<List<Student>> Get() => _mongoDBService.GetStudents();

    [HttpGet("{id}")]
    public ActionResult<Student> Get(string id)
    {
        var student = _mongoDBService.GetStudent(id);
        if (student == null)
        {
            return NotFound();
        }
        return student;
    }

    [HttpPost]
    public ActionResult<Student> Create(Student student)
    {
        _mongoDBService.CreateStudent(student);
        return CreatedAtAction(nameof(Get), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, Student student)
    {
        var existingStudent = _mongoDBService.GetStudent(id);

        if (existingStudent == null)
        {
            return NotFound();
        }

        _mongoDBService.UpdateStudent(id, student);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        var student = _mongoDBService.GetStudent(id);

        if (student == null)
        {
            return NotFound();
        }

        _mongoDBService.DeleteStudent(id);

        return NoContent();
    }
}
