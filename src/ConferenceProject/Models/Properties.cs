using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceProject.Models
{
    public class Properties
    {
        public int ID { get; set; }
        public string _title { get; set; }
        public string _address { get; set; }
        public DateTime _startTime { get; set; }
        public string _movie { get; set; }

        public Properties()
        {
           // ID = 1;
            _title = "deafult name";
            _address = "rabin 1 tel aviv";
            _startTime = System.DateTime.Parse("01/01/2001");
        }

        
      

    }
}
