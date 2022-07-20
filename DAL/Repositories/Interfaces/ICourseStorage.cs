using DAL.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface ICourseStorage
    {
        /// <summary>
        /// GetAllCoursesAsync
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns>Task<List<Course>></returns>
        public Task<List<Course>> GetAllCoursesAsync(CancellationToken cancelToken = default);

        /// <summary>
        /// GetCoursesWithPagingAsync
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<List<Course>></returns>
        public Task<List<Course>> GetCoursesWithPagingAsync(PagingModel paging, CancellationToken cancelToken = default);

        /// <summary>
        /// GetCourseByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<Course></returns>
        public Task<Course> GetCourseByIdAsync(int id, CancellationToken cancelToken = default);

        /// <summary>
        /// UpdateCourseAsync
        /// </summary>
        /// <param name="courseToUpdate"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<Course></returns>
        public Task<Course> UpdateCourseAsync(Course courseToUpdate, CancellationToken cancelToken = default);

        /// <summary>
        /// AddCourseAsync
        /// </summary>
        /// <param name="courseToAdd"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<Course></returns>
        public Task<Course> AddCourseAsync(Course courseToAdd, CancellationToken cancelToken = default);

        /// <summary>
        /// GetCoursesCountAsync
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns>Task<int></returns>
        public Task<int> GetCoursesCountAsync(CancellationToken cancelToken = default);
    }
}
