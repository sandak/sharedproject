using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceProject.Models
{
    public class Lecturer
    {
        public int lecturerID { get; set; }
        public string _fname { get; set; }
        public string _lname { get; set; }
        public string _email { get; set; }
        public string _company { get; set; }
        public string _imgUrl { get; set; }
        public string _description { get; set; }
    }
}
