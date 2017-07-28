using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using firstcore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class HomeController : Controller
{
    private IGreeter greeter;
    private IConfiguration configuration;

    public HomeController(IGreeter greeter, IConfiguration configuration)
    {
        this.greeter = greeter;
        this.configuration = configuration;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult stuff()
    {
        var result = new[] {
            new Person("Bek", 35),
            new Person("Damien", 43),
            new Person($"{this.greeter.GetGreeting()}",50)
            };
        return new ObjectResult(result);
    }

    ///Can still route to this. MVC will automatically wrap in ObjectResult and return json.
    public IEnumerable<Person> stuff2()
    {
        var result = new[] { new Person("Rebekah", 35), new Person("Damien", 43) };
        var r2 = result.SelectMany(r => Enumerable.Repeat(r, 10));
        return r2;
    }

    [Route("/happy/{id}")]
    public string[] random(string id)
    {
        var rnd = new Random(DateTime.Now.Millisecond);
        return Enumerable.Range(0, 100).Select(x => $"why, hello {id} {rnd.Next(20000)}").ToArray();
    }

    [Route("/contacts")]
    public async Task<IActionResult> GetContacts()
    {
        var connstring = configuration["DBInfo:ConnectionString"];
        var connection = new NpgsqlConnection(connstring);
        using (IDbConnection dbConnection = connection)
        {
            dbConnection.Open();
            var r = await dbConnection.QueryAsync<Contact>("select * from Contact;");
            return new ObjectResult(r);
        }
    }
}

public class Contact
{
    public Guid id { get; set; }
    public DateTime date { get; set; }
}
