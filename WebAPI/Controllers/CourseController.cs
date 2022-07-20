using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Requests;
using Models.Responses.Interfaces;
using Models.Responses;
using Services.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CourseController : BaseController
    {
        #region Members and constants

        private readonly ICourseService _thisService;

        #endregion Members and constants

        #region Constructors

        public CourseController(ILogger<BaseController> logger, ICourseService courseService) : base(logger)
        {
            this._thisService = courseService ?? throw new ArgumentNullException(paramName: nameof(courseService));
        }

        #endregion Constructors

        #region Web Methods

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllCourses")]
        //[ServiceFilter(typeof(ClientIpCheckActionFilter))]
        [ProducesResponseType(typeof(IApiResponse<List<CourseDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCourses(CancellationToken cancelToken = default)
        {
            IApiResponse<List<CourseDTO>> response = new ApiResponse<List<CourseDTO>>
            {
                Data = await _thisService.GetAllCoursesAsync(cancelToken).ConfigureAwait(false)
            };

            return CreateResponse(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetCoursesWithPaging")]
        //[ServiceFilter(typeof(ClientIpCheckActionFilter))]
        [ProducesResponseType(typeof(IApiResponse<ListPageResult<CourseDTO>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCoursesWithPaging(PaginationModel paging, CancellationToken cancelToken = default)
        {
            IApiResponse<ListPageResult<CourseDTO>> response = new ApiResponse<ListPageResult<CourseDTO>>
            {
                Data = await _thisService.GetCoursesWithPagingAsync(paging, cancelToken).ConfigureAwait(false)
            };

            return CreateResponse(response);
        }

        [HttpGet]
        [Route("GetCourseById")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IApiResponse<CourseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseByIdAsync(int id, CancellationToken cancelToken = default)
        {
            IApiResponse<CourseDTO> response = new ApiResponse<CourseDTO>
            {
                Data = await _thisService.GetCourseByIdAsync(id, cancelToken).ConfigureAwait(false)
            };

            return CreateResponse(response);
        }

        [HttpPut]
        [Route("UpdateCourse")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IApiResponse<CourseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCourse(AddUpdateCourseRequest CourseToUpdate, CancellationToken cancelToken = default)
        {
            IApiResponse<CourseDTO> response = new ApiResponse<CourseDTO>
            {
                Data = await _thisService.UpdateCourseAsync(CourseToUpdate, cancelToken).ConfigureAwait(false)
            };

            return CreateResponse(response);
        }

        [HttpPost]
        [Route("AddCourse")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IApiResponse<CourseDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCourse(AddUpdateCourseRequest CourseToAdd, CancellationToken cancelToken = default)
        {
            IApiResponse<CourseDTO> response = new ApiResponse<CourseDTO>
            {
                Data = await _thisService.AddCourseAsync(CourseToAdd, cancelToken).ConfigureAwait(false)
            };
            return CreateResponse(response);
        }

        #endregion Web Methods

    }
}
