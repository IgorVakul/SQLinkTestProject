using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories.Classes
{
    public class CourseStorage : ICourseStorage
    {
        #region  Data members/Constants 

        private readonly SqLinkTestDBContext _dbContext;

        #endregion

        #region Constructor

        public CourseStorage(SqLinkTestDBContext dbContext)
        {
            this._dbContext = dbContext ?? throw new ArgumentNullException(paramName: nameof(dbContext));
        }

        #endregion

        #region Methods

        public async Task<Course> AddCourseAsync(Course courseToAdd, CancellationToken cancelToken = default)
        {
            if (courseToAdd != null)
            {
                _dbContext.Courses.Add(courseToAdd);
                await _dbContext.SaveChangesAsync(cancelToken);
            }

            return courseToAdd;
        }

        public async Task<List<Course>> GetAllCoursesAsync(CancellationToken cancelToken = default)
        {
            return await _dbContext.Courses.ToListAsync(cancelToken);
        }

        public async Task<Course> GetCourseByIdAsync(int id, CancellationToken cancelToken = default)
        {
            return await _dbContext.Courses
              .Where(m => m.Id == id).FirstOrDefaultAsync(cancelToken);
        }

        public async Task<int> GetCoursesCountAsync(CancellationToken cancelToken = default)
        {
            return await _dbContext.Courses.CountAsync(cancelToken);
        }

        public async Task<List<Course>> GetCoursesWithPagingAsync(PagingModel paging, CancellationToken cancelToken = default)
        {
            return await _dbContext.Courses
               .Skip((paging.Skip - 1) * paging.Take).Take(paging.Take)
               .ToListAsync(cancelToken);
        }

        public async Task<Course> UpdateCourseAsync(Course courseToUpdate, CancellationToken cancelToken = default)
        {
            Course course = await _dbContext.Courses.FindAsync(courseToUpdate.Id);

            if (course != null)
            {
                course.StartHour = courseToUpdate.StartHour;
                course.CourseName = courseToUpdate.CourseName;
                course.CourseDescription = courseToUpdate.CourseDescription;
                course.StartDate = courseToUpdate.StartDate;
                course.EndDate = courseToUpdate.EndDate;
                course.DurationInAcademicHours = courseToUpdate.DurationInAcademicHours;
                course.Lecturer = courseToUpdate.Lecturer;
                course.EndHour = courseToUpdate.EndHour;                
                course.LessonDurationInMinutes = courseToUpdate.LessonDurationInMinutes;
                course.Place = courseToUpdate.Place;
                course.CoursePrice = courseToUpdate.CoursePrice;

                await _dbContext.SaveChangesAsync(cancelToken);
            }

            return course;
        }

        #endregion
    }
}
