using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using static PeanutButter.RandomGenerators.RandomValueGen;

namespace MiniMvc
{
    public class MooController : Controller
    {
        public async Task<int> Add(int a, int b)
        {
            return await Task.Run<int>(() =>
            {
                var foo = Request.Query["foo"];
                var A = Request.Query["A"];
                var session = HttpContext.Session;
                var pathBase = Request.PathBase;
                var rel = "~/foo/bar";
                var resolved = pathBase + rel[1..];
                return a + b;
            });
        }

        public string Submit()
        {
            var foo = Request.Form["FoO"];
            return foo;
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static class People
    {
        private static int _sequence;
        private static Dictionary<int, Person> _store = new();

        public static Person FindById(int id)
        {
            return _store.TryGetValue(id, out var result)
                ? result
                : throw new FileNotFoundException();
        }

        public static void Add(string name)
        {
            lock (_store)
            {
                var person = new Person()
                {
                    Id = ++_sequence,
                    Name = name
                };
                _store.Add(person.Id, person);
            }
        }

        public static Person Latest()
        {
            return FindById(_sequence);
        }
    }

    public class PersonController
    {
        [ActionName("")]
        public Person Index(int id)
        {
            return People.FindById(id);
        }

        public Person Add(string name)
        {
            People.Add(name ?? GetRandomName());
            return People.Latest();
        }
    }
}