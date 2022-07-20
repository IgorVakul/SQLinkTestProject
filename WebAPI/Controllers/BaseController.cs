using Microsoft.AspNetCore.Mvc;
using Models.Responses;
using Models.Responses.Interfaces;


namespace WebAPI.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        #region Data members/constants

        protected readonly ILogger<BaseController> _logger;

        #endregion

        #region Constructors

        public BaseController(ILogger<BaseController> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// CreateResponse()
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="statusCode"></param>
        /// <param name="ex"></param>
        /// <returns>IActionResult</returns>
        protected IActionResult CreateResponse<T>(IApiResponse<T> response)
        {
            response ??= new ApiResponse<T>();
            response.HttpCode = 200;
            return Ok(response);
        }


        #endregion
    }
}
