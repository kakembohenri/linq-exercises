using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linq
{
    public class Location
    {

        public string city;
        public string state;
            
            public Location(string city, string state)
            {
                this.city = city;
                this.state = state;
                
            }

            

            public static List<Location> locations = new()
    {
        new(
            city: "Ntinda", state: "Kampala"
        ),
         new(
            city: "Bukoto", state: "Kampala"
        ),
          new(
            city: "Naguru", state: "Kampala"
        ),
           new(
            city: "Nshema", state: "Mbarara"
        ),
            new(
            city: "murembe", state: "Mbarara"
        ),
    };
        }

        
    
}
