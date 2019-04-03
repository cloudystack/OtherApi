using Microsoft.AspNetCore.Mvc;
using Demo.OtherApi.BusinessLogic.Core.Services.Interfaces;
using Demo.OtherApi.BusinessLogic.Models;
using Demo.OtherApi.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace Demo.OtherApi.Presentation.Controllers
{
    [Route("appsettings")]
    public class AppSettingController : BaseController
    {
        private readonly IAppSettingQueryService _appSettingQueryService;
        private readonly IAppSettingService _appSettingService;
        private readonly IBusinessManagerService _businnesManagerService;

        public AppSettingController(IAppSettingQueryService appSettingQueryService, 
                                    IAppSettingService appSettingService,
                                    IBusinessManagerService businessManagerService) : base(businessManagerService)
        {
            this._appSettingQueryService = appSettingQueryService;
            this._appSettingService = appSettingService;
            this._businnesManagerService = businessManagerService;
        }

        /// <summary>
        /// Get appsettings
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(AppSettingListRp), 200)]
        public async Task<IActionResult> Get()
        {
            var model = await this._appSettingQueryService.GetAppSettings();
            return this.Ok(model);
        }

        /// <summary>
        /// Get appsetting by id 
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppSettingGetRp), 200)]
        public async Task<IActionResult> GetById(string id)
        {
            var model = await this._appSettingQueryService.GetAppSettingById(id);
            return this.Ok(model);
        }

        /// <summary>
        /// Create a new appsetting
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /appsettings
        ///     {
        ///        "id": "key1",
        ///        "value": "Value1"
        ///     }
        ///
        /// </remarks>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]AppSettingPostRp resource)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            await this._appSettingService.CreateAppSetting(resource);

            if (this._businnesManagerService.HasConflicts())
            {
                return this.Conflict(this._businnesManagerService.GetConflicts());
            }

            return this.Ok();
        }

        /// <summary>
        /// Update an appsetting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]AppSettingPutRp resource) {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            await this._appSettingService.UpdateAppSetting(id, resource);

            if (this._businnesManagerService.HasNotFounds())
            {
                return this.NotFound(this._businnesManagerService.GetNotFounds());
            }

            if (this._businnesManagerService.HasConflicts())
            {
                return this.Conflict(this._businnesManagerService.GetConflicts());
            }

            return this.Ok();
        }

        /// <summary>
        /// Delete an appsetting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(string id)
        {
            if (!this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            await this._appSettingService.DeleteAppSetting(id);

            if (this._businnesManagerService.HasNotFounds())
            {
                return this.NotFound(this._businnesManagerService.GetNotFounds());
            }

            if (this._businnesManagerService.HasConflicts())
            {
                return this.Conflict(this._businnesManagerService.GetConflicts());
            }

            return this.NoContent();
        }


    }
}
