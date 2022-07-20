using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class BaseService
    {
        #region  Data members and Constants

        protected readonly ILogger<BaseService> _logger;

        #endregion Data members and Constants

        #region Constructors

        protected BaseService(ILogger<BaseService> logger)
        {
            this._logger = logger ?? throw new ArgumentNullException(paramName: nameof(logger));
        }

        #endregion Constructors
    }
}
