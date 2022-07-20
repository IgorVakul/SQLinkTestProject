using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Enums
{
    public enum ErrorCodes : int
    {

        #region Unauthorized

        [Display(Name = "unauthorized")]
        Unauthorized = 401,
        [Display(Name = "forbidden")]
        Forbidden = 403,

        #endregion

        #region Not Found

        [Display(Name = "not_found")]
        NotFound = 404,

        [Display(Name = "course_not_found")]
        CourseNotFound = 404101,

        [Display(Name = "cources_do_not_exist")]
        CoursesDoNotExists = 404102,

        #endregion

        #region Bad Request

        [Display(Name = "bad_request")]
        BadRequest = 400,

        #region Courses

        [Display(Name = "add_course_failed", Description = "Add new Course failed")]
        AddCourseFailed = 400101,

        [Display(Name = "update_course_failed", Description = "Update Course failed")]
        UpdateCourseFailed = 400102,

        #endregion Courses

        #endregion Bad Request

        #region "Internal Server Error"

        [Display(Name = "internal_server_error")]
        InternalServerError = 500,


        [Display(Name = "general_error")]
        GeneralError = 500999,

        #endregion "Internal Server Error"
    }
}
