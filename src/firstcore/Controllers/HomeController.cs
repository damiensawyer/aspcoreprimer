using System;
using System.Collections.Generic;
using System.Linq;
using firstcore;
using Microsoft.AspNetCore.Mvc;
public class HomeController : Controller
{
    private IGreeter greeter;

    public HomeController(IGreeter greeter)
    {
        this.greeter = greeter;
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
}
