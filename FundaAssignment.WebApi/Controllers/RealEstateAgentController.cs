using FundaAssignment.WebApi.Domain.Exceptions;
using FundaAssignment.WebApi.Domain.ViewModels;
using FundaAssignment.WebApi.Infrastructure.Attributes;
using FundaAssignment.WebApi.Infrastructure.Extensions;
using FundaAssignment.WebApi.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FundaAssignment.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RealEstateAgentController : ControllerBase
    {
        private readonly ILogger<RealEstateAgentController> _logger;
        private readonly IFundaService _fundaService;

        public RealEstateAgentController(ILogger<RealEstateAgentController> logger, IFundaService fundaService)
        {
            _logger = logger;
            _fundaService = fundaService;
        }
        //}
        /// <summary>
        /// Returns real estate agents who has the most number of homes for sale
        /// </summary>
        /// <param name="type">Type of home</param>
        /// <param name="depth">Optional - max number of elements in return list</param>
        /// <returns>Array of real estate agents</returns>
        [HttpGet]
        [AllowCrossOriginRequest]
        [Route("{type}")]
        [ProducesResponseType(typeof(IEnumerable<RealEstateAgent>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorObject), (int)HttpStatusCode.BadGateway)]
        [ProducesResponseType(typeof(ErrorObject), (int)HttpStatusCode.TooManyRequests)]
        [ProducesResponseType(typeof(ErrorObject), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] HouseType type, [FromQuery] int depth = 10)
        {
            try
            {
                var data = await _fundaService.GetTopAgents(type, depth);
                return new ObjectResult(data);
            }
            catch (ProxyServerException e)
            {
                return new ObjectResult(e.ToErrorObject())
                {
                    StatusCode =(int) HttpStatusCode.BadGateway
                };
            }
            catch (ToManyRequestException e)
            {
                return new ObjectResult(e.ToErrorObject())
                {
                    StatusCode = (int)HttpStatusCode.TooManyRequests
                };
            }
            catch (Exception e)
            {
                return new ObjectResult(e.ToErrorObject())
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }

        }
    }
}
