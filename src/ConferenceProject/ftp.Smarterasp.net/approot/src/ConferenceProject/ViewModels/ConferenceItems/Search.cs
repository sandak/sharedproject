using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceProject.Models;

namespace ConferenceProject.ViewModels.ConferenceItems
{
    public class Search
    {
        public Lecturer lecturer { get; set; }
        public ConferenceItem talks { get; set; }
    }
}

