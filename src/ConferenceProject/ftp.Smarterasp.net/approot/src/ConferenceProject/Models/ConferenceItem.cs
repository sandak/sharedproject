using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceProject.Models
{
    public class ConferenceItem
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Speaker required!")]
        public int lecturerID { get; set; }

        [Required(ErrorMessage = "Title required!")]
        [StringLength(25,MinimumLength = 5,ErrorMessage ="Title must be a string with 5-16 characters")]
        public string _title { get; set; }

        [Required(ErrorMessage = "Start Time required!")]
        [DataType(DataType.DateTime,ErrorMessage ="Invalid Date")]
        public DateTime _startTime { get; set; }

        [Required(ErrorMessage = "End Time required!")]
        [DataType(DataType.DateTime, ErrorMessage ="Invalid Date")]
        public DateTime _endTime { get; set; }

        public string _location { get; set; }

        [Required(ErrorMessage = "Description required!")]
        [StringLength(255, MinimumLength = 15 ,ErrorMessage ="Description must be a string with 15-255 characters")]
        public string _description { get; set; }

        [Url(ErrorMessage ="Syllabus must be a valid URL")]
        public string _syllabus { get; set; }

        public Lecturer Lecturer { get; set; }
        public List<UsersConferenceItems> usersConferenceItems { get; set; }
    }
}
