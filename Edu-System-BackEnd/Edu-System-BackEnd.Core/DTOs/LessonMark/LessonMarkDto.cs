﻿namespace Edu_System_BackEnd.Edu_System_BackEnd.Core.DTOs.Homework
{
    public class LessonMarkDto
    {
        public Guid Id { get; set; }
        public Guid LessonId { get; set; }
        public Guid StudentId { get; set; }
        public int Mark { get; set; }
    }

}
