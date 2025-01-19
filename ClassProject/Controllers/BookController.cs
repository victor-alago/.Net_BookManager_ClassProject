using Microsoft.AspNetCore.Mvc;

namespace ClassProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    /// GET: api/<BookController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<BookController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<BookController>
    [HttpPost]
    public Book_Initial Post([FromBody] Book_Initial bookInitial)
    {
        Console.WriteLine(bookInitial);
        return bookInitial;
    }

    // PUT api/<BookController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        Book_Initial bookInitial = new Book_Initial();
    }

    // DELETE api/<BookController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}