
using linq;
using System;
using System.Net.Security;
using System.Xml.Linq;

namespace linq;

class Program
{
    static void Main(string[] args)
    {
       
        // Linq queries
        linq_queries();

        // Return a query from a method
        query_from_method();

        // Store results of a query in memory
        query_memory();

        //Grouping query results
        group();

        //Nested group
        nested_group();

        //Subquery 
        subquery();

        //Joins
        joins();

        static void linq_queries()
        {
            Console.WriteLine("[1] **** Writing LINQ queries in c **** ");

            var numbers = new List<int> { 15, 14, 11, 13, 19, 18, 16, 17, 12, 10 };

            Console.WriteLine();

            Console.WriteLine("[1.a] *** Using Query Syntax *** ");

            var filterQuery = (from num in numbers where num < 15 select num);

            foreach (var filter in filterQuery)
            {
                Console.WriteLine(filter);
            }

            Console.WriteLine();


            Console.WriteLine("[1.b] *** Using Method Syntax ***");

            var filterMethod = numbers.Where(num => num < 15);

            foreach (var filter in filterMethod)
            {
                Console.WriteLine(filter);
            }

            Console.WriteLine();

            Console.WriteLine("[1.c] *** A combination of both Query and Method syntax");

            var numbersAvg = numbers.Where(num => num > 15);

            var newList = (from num in numbersAvg let newNum = num + 3 select newNum);

            foreach (var num in newList)
            {
                Console.WriteLine($"New number is {num} when 3 is added to it");
            }
        }

        static void query_from_method()
        {
            Console.WriteLine();
            Console.WriteLine("[2] **** Returning a query from a method ****");
            Console.WriteLine();

            static dynamic query()
            {
                var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                return (from num in numbers where num > 5 select num);
            }

            foreach (var num in query())
            {
                Console.WriteLine(num);
            }
        }

        static void query_memory()
        {
            Console.WriteLine();
            Console.WriteLine("[3] **** Store the results of the query in memory ****");
            Console.WriteLine();

            var array = new List<int> { 1, 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };

            var query = array.Where(num => num % 4 == 0).OrderBy(num => num).ToList();

            Console.WriteLine($"The first occurance in the list is {query[0]}");
        }

        static void group()
        {

            Console.WriteLine("[4] **** Grouping Query results ****");

            var locations = Location.locations;

            var groupByState =
                    from location in locations
                    group location by location.state into newState
                    orderby newState.Key
                    select newState;

            foreach (var stateGroup in groupByState)
            {
                Console.WriteLine($"Key: {stateGroup.Key}");
                foreach (var place in stateGroup)
                {
                    Console.WriteLine($"\t{place.state}, {place.city}");
                }
            }
        }
    
        static void nested_group()
        {
            Console.WriteLine("[5] **** Nesting groups **** ");
            var locations = Location.locations;

            var nestedLocations = from location in locations group location by location.state into newState from newState2 in (from location in newState group location by location.city) group newState2 by newState.Key;

            foreach (var outerGroup in nestedLocations)
            {
                Console.WriteLine($"DataClass.State Level = {outerGroup.Key}");

                foreach (var innerGroup in outerGroup)
                {
                    Console.WriteLine($"\tNames that begin with: {innerGroup.Key}");
                    foreach (var innerGroupElement in innerGroup)
                    {
                        Console.WriteLine($"\t\t{innerGroupElement.state} {innerGroupElement.city}");
                    }
                }
            }
        }
    
        static void subquery()
        {
            Console.WriteLine();
            Console.Write("[6] **** Sub query **** ");
            Console.WriteLine();

            var locations = Location.locations;

            var queryGroup = from location in locations
                             group location by location.state into newState
                             select new
                             {
                                 Level = newState.Key,
                                 LengthOfWord = (from word in newState select word.ToString())
                             };

            foreach (var item in queryGroup)
            {
                Console.WriteLine();
                Console.WriteLine($"  {item.Level} Number of cities is ={item.LengthOfWord.Count()}");
            }
        }
    
        static void joins()
        {
            Console.WriteLine();
            Console.WriteLine("[7] **** Joins **** ");
            Console.WriteLine();

            
            Location morris = new(city: "lombo", state: "Gulu");
            Location garreth = new("upa", "Mbale");
            Location ben = new("iko", "Kotido");

            List<Location> locations = new() { morris, garreth };

            List<Leaders> leaders = new()
            {
                new(name: "Morris", place: morris.state),
                new("Garreth", garreth.state),
                new("Amigo", ben.state)
            };


            var query = from leader in leaders
                        join location in locations on leader.place equals location.state
                        select new
                        {
                            leader_name = leader.name,
                            state_name = location.state
                        };

            foreach (var leaderAndState in query)
            {
                Console.WriteLine($"\"The leader of {leaderAndState.state_name}\" is {leaderAndState.leader_name}");
            }
        }
    }
}

internal class Leaders
{
   
    public string name;
    public string place;

    public Leaders(string name, string place)
    {
        this.name = name;
        this.place = place;
    }

    

}
    