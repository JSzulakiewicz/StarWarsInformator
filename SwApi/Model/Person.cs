using System;
using System.Collections.Generic;
using System.Text;

namespace SwApiClient.Model
{
    public class Person
    {
        public string Name { get; set; }
        public IEnumerable<string> Films {get;set;}
        public IEnumerable<string> Ships { get; set; }
        public IEnumerable<string> Vehicles{ get; set; }

        /*
         * 
        Other properties returned from api could be added here, if our client should be universal.
        Only properties required by specification was added here.
        *
        */
    }
}
