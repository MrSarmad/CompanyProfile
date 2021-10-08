using ASI.Contracts.CompanyProfile;
using ASI.Contracts.CompanyProfile.Search;
using ASI.Services.Search.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.MyEntities;
using CompanyProfile.Core.Search.Models;
using CompanyProfile.Core.Search.Providers;
using System;
using System.Threading.Tasks;

namespace CompanyProfile.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/myentities")]
    [ApiController]
    //[Authorize]
    public sealed class MyEntityController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMyEntityService _myEntityService;
        private readonly IMyEntitySearchProvider _myEntitySearchProvider;
        private readonly ILogger<MyEntityController> _logger;

        public MyEntityController(IMapper mapper, IMyEntityService myEntityService,
            IMyEntitySearchProvider myEntitySearchProvider, ILogger<MyEntityController> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _myEntityService = myEntityService ?? throw new ArgumentNullException(nameof(myEntityService));
            _myEntitySearchProvider = myEntitySearchProvider ?? throw new ArgumentNullException(nameof(myEntitySearchProvider));
            _logger = logger;
        }

        /// <summary>
        /// Search Endpoint
        /// </summary>
        /// <param name="searchCriteriaView"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("search")]
        public async Task<SearchResultView<MyEntityView>> SearchAsync(SearchCriteriaView searchCriteriaView)
        {
            var searchRequest = _mapper.Map<SearchRequest>(searchCriteriaView);
            var searchRes = await _myEntitySearchProvider.SearchAsync(searchRequest);
            var res = _mapper.Map<SearchResultView<MyEntityView>>(searchRes);
            return res;
        }

        //Example of a method that has no async operation
        [HttpGet]
        [Route("{id}")]
        public MyEntityView Get(long id)
        {
            //example of any time you need to log something, rather than
            // creating a log4net logger to instead inject an ILogger<T>
            _logger.LogInformation($"GET {id}");

            var model = _myEntityService.Get(id);
            var res = _mapper.Map<MyEntityView>(model);
            return res;
        }

        [HttpPost]
        public async Task<ActionResult<MyEntityView>> PostAsync(MyEntityView view)
        {
            var model = _mapper.Map<MyEntity>(view);
            if (string.IsNullOrWhiteSpace(model.Name))
                return BadRequest();

            var created = await _myEntityService.AddAsync(model);
            var res = _mapper.Map<MyEntityView>(created);
            return res;
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<MyEntityView>> PutAsync(long id, MyEntityView view)
        {
            if (id <= 0)
                return BadRequest();

            var model = _mapper.Map<MyEntity>(view);
            var updated = await _myEntityService.UpdateAsync(id, x =>
            {
                _mapper.Map(view, x);
            });
            var res = _mapper.Map<MyEntityView>(updated);
            return res;
        }
    }
}
