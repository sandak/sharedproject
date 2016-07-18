using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceProject.Models;

namespace ConferenceProject.ViewModels.Statistics
{
    public class AttendGraph
    {
        public string date { get; set; }
        public int count { get; set; }
        public ConferenceItem item { get; set; }
    }

    public class PopularityGraph
    {
        public string title { get; set; }
        public int lecturer { get; set; }
        public int count { get; set; }
    }
}
