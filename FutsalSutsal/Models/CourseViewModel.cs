using System;
using System.ComponentModel.DataAnnotations;

namespace FutsalSutsal.Models
{
    public class SearchCourseViewModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseInstructor { get; set; }
        public int CourseDuration { get; set; }
        public string CourseStartDate { get; set; }
        public string CourseEndDate { get; set; }
        public string CourseStatus { get; set; }


    }



    public class AddEditCourseViewModel
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Course Name ")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Course Instructor ")]
        public string CourseInstructor { get; set; }

        [Required]
        [Display(Name = "Course Duration ")]
        public int CourseDuration { get; set; } //  gets calculated based on start time and end time


        [Required(ErrorMessage ="Start date is required")]
        [Display(Name = "Course Start Date")]
        public DateTime? CourseStartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [Display(Name = "Course End Date")]
        public DateTime?CourseEndDate { get; set; }

        [Display(Name = "Course Status ")]
        public string CourseStatus { get; set; }
    }
}