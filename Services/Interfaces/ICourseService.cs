using DAL.Models;
using Models.DTO;
using Models.Requests;
using Models.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICourseService
    {
        /// <summary>
        /// GetAllCoursesAsync
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns>Task<List<CourseDTO>></returns>
        public Task<List<CourseDTO>> GetAllCoursesAsync(CancellationToken cancelToken = default);

        /// <summary>
        /// GetCoursesWithPagingAsync
        /// </summary>
        /// <param name="paging"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<ListPageResult<CourseDTO>></returns>
        public Task<ListPageResult<CourseDTO>> GetCoursesWithPagingAsync(PaginationModel paging, CancellationToken cancelToken = default);

        /// <summary>
        /// GetCourseByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<CourseDTO></returns>
        public Task<CourseDTO> GetCourseByIdAsync(int id, CancellationToken cancelToken = default);

        /// <summary>
        /// UpdateCourseAsync
        /// </summary>
        /// <param name="courseToUpdate"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<CourseDTO></returns>
        public Task<CourseDTO> UpdateCourseAsync(AddUpdateCourseRequest  courseToUpdate, CancellationToken cancelToken = default);

        /// <summary>
        /// AddCourseAsync
        /// </summary>
        /// <param name="courseToAdd"></param>
        /// <param name="cancelToken"></param>
        /// <returns>Task<CourseDTO></returns>
        public Task<CourseDTO> AddCourseAsync(AddUpdateCourseRequest courseToAdd, CancellationToken cancelToken = default);       
    }
}
