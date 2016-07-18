using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceProject.Models
{
    public class UsersConferenceItems
    {
        public int ID { get; set; }
        public DateTime registerDate { get; set; }
        public ApplicationUser user { get; set; }
        public ConferenceItem item { get; set; }

       

    }
}
