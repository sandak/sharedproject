using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceProject.Models
{
    public class ConferenceItem
    {
        public int ID { get; set; }
        public int lecturerID { get; set; }
        public string _title { get; set; }
        public DateTime _startTime { get; set; }
        public DateTime _endTime { get; set; }
        public string _location { get; set; }
        public string _description { get; set; }

        public Lecturer Lecturer { get; set; }
        public List<UsersConferenceItems> usersConferenceItems { get; set; }
    }
}
