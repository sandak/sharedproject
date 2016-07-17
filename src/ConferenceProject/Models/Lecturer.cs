﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceProject.Models
{
    public class Lecturer
    {
        public int lecturerID { get; set; }

        [Required(ErrorMessage ="First name required!")]
        [RegularExpression("123456789",ErrorMessage ="Numbers arent allowed!")]
        public string _fname { get; set; }

        [Required(ErrorMessage = "Last name required!")]
        [RegularExpression("[a-zA-Z]", ErrorMessage = "Numbers arent allowed!")]
        public string _lname { get; set; }

        [Required(ErrorMessage = "Email required!")]
        [EmailAddress(ErrorMessage ="Not a valid email address")]
        public string _email { get; set; }

        [DataType(DataType.Text)]
        public string _company { get; set; }

        [Url(ErrorMessage ="Not a valid image url")]
        public string _imgUrl { get; set; }

        [Required(ErrorMessage = "Description required!")]
        [StringLength(50,MinimumLength = 15,ErrorMessage ="description must be a string with 15-50 chars")] 
        public string _description { get; set; }
    }
}
