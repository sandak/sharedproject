using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ConferenceProject.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class

    public class ApplicationUser : IdentityUser
    {

        public string _fname { get; set; }
        public string _lname { get; set; }
        public string _address { get; set; }
        public DateTime _registerDate { get; set; }

        public List<UsersConferenceItems> usersConferenceItems { get; set; }
    }
}
