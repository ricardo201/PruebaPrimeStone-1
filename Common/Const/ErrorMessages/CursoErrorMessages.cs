using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Const.ErrorMessages
{
    public class CursoErrorMessages
    {
        public const string CourseDoesNotExist = "Course does not exist";
        public const string CourseUpdateNotAllowed = "Course update not allowed";
        public const string CourseDeleteNotAllowed = "Course delete not allowed";
        public const string NameCourseCannotBeNull = "Name course cannot be null";
        public const string NameCourseCannotBeEmpty = "Name course cannot be empty";
        public const string InitialDateCannotBeNull = "Initial date cannot be null";
        public const string InitialDateCannotBeEmpty = "Initial date cannot be empty";
        public const string FinalDateCannotBeNull = "Final date cannot be null";
        public const string FinalDateCannotBeEmpty = "Final date cannot be empty";
        public const string InitialDateCannotLessThanToday = "Initial date cannot less than today";
        public const string FinalDateCannotLessThanToday = "Final date cannot less than today";
        public const string FinalDateCannotLessThanInitialDate = "Final date cannotl less than today";
    }
}
