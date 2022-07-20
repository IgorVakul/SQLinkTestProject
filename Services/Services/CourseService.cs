using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Models.Requests;
using Models.Responses;
using Services.Constants;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CourseService : BaseService, ICourseService
    {
        #region Members and constants

        private readonly IMapper _mapper;
        private readonly ICourseStorage _courseStorage;
        private readonly IMemoryCache _memoryCache;

        #endregion Members and constants

        #region Constructors

        public CourseService(ILogger<CourseService> logger, ICourseStorage courseStorage, IMapper mapper, IMemoryCache memoryCache) : base(logger)
        {
            this._courseStorage = courseStorage ?? throw new ArgumentNullException(paramName: nameof(courseStorage));
            this._mapper = mapper ?? throw new ArgumentNullException(paramName: nameof(mapper));
            this._memoryCache = memoryCache ?? throw new ArgumentNullException(paramName: nameof(memoryCache));
        }

        #endregion Constructors       

        #region Methods

        public async Task<CourseDTO> AddCourseAsync(AddUpdateCourseRequest courseToAdd, CancellationToken cancelToken = default)
        {
            //map object for db
            var model = _mapper.Map<Course>(courseToAdd);

            //save data in db
            var data = await _courseStorage.AddCourseAsync(model, cancelToken).ConfigureAwait(false);

            if (data.Id == 0)
            {
                throw new BadRequestException(ErrorCodes.AddCourseFailed);
            }

            return _mapper.Map<CourseDTO>(data);
        }

        public async Task<List<CourseDTO>> GetAllCoursesAsync(CancellationToken cancelToken = default)
        {
            List<CourseDTO> Result = null;

            //get courses list from cache
            Result = _memoryCache.Get<List<CourseDTO>>(CacheConstants.COURSES_LIST);

            if (Result == null)
            {
                var data = await _courseStorage.GetAllCoursesAsync(cancelToken).ConfigureAwait(false);

                //map data to result
                Result = _mapper.Map<List<CourseDTO>>(data);

                if (Result?.Count > 0)
                {
                    _memoryCache.Set(CacheConstants.COURSES_LIST, Result, DateTimeOffset.UtcNow.AddMinutes(5));
                }
                else
                {
                    throw new NotFoundException(ErrorCodes.CoursesDoNotExists);
                }
            }

            return Result;
        }

        public async Task<CourseDTO> GetCourseByIdAsync(int id, CancellationToken cancelToken = default)
        {
            var data = await _courseStorage.GetCourseByIdAsync(id, cancelToken).ConfigureAwait(false);

            if (data == null)
            {
                throw new NotFoundException(ErrorCodes.CourseNotFound);
            }

            return _mapper.Map<CourseDTO>(data);
        }

        public async Task<ListPageResult<CourseDTO>> GetCoursesWithPagingAsync(PaginationModel paging, CancellationToken cancelToken = default)
        {
            if (paging.PageSize <= 0)
                throw new ArgumentException($"{nameof(paging.PageSize)} must be greater than zero");

            if (paging.PageNumber <= 0)
                throw new ArgumentException($"{nameof(paging.PageNumber)} must be greater than zero");

            //get total journey list count 
            int TotalListCount = await _courseStorage.GetCoursesCountAsync(cancelToken);

            var pageModel = new PagingModel(paging.PageNumber, paging.PageSize);

            //check if pageSize  not over the total rows
            if (paging.PageSize > TotalListCount)
            {
                pageModel.Take = TotalListCount;
                pageModel.Skip = 1;
                paging.PageNumber = 1;
                paging.PageSize = TotalListCount;
            }

            //get data from db
            var courseListData = await _courseStorage.GetCoursesWithPagingAsync(pageModel, cancelToken).ConfigureAwait(false);

            //map data to result
            var courses = _mapper.Map<List<CourseDTO>>(courseListData);

            if (courses.Any())
            {
                //create response
                ListPageResult<CourseDTO> result = new(paging.PageNumber, paging.PageSize)
                {
                    //set total count of the list
                    TotalCount = TotalListCount,
                    List = courses
                };

                return result;
            }
            else
            {
                throw new NotFoundException(ErrorCodes.CoursesDoNotExists);
            }
        }

        public async Task<CourseDTO> UpdateCourseAsync(AddUpdateCourseRequest courseToUpdate, CancellationToken cancelToken = default)
        {
            //map object for db
            Course model2DB = _mapper.Map<Course>(courseToUpdate);

            Course data = await _courseStorage.UpdateCourseAsync(model2DB, cancelToken).ConfigureAwait(false);

            if (data == null)
            {
                throw new BadRequestException(ErrorCodes.UpdateCourseFailed);
            }

            return _mapper.Map<CourseDTO>(data);
        }

        #endregion
    }
}
