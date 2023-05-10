﻿namespace Contoso.Data.Entities
{
    public class CourseAssignment : BaseDataClass
    {
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public Instructor Instructor { get; set; }
        public Course Course { get; set; }
    }
}
