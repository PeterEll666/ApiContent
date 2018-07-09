using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApiContent.DataAccess;
using ApiContent.Models;
using ApiContent.Services;

namespace ApiContent.Controllers
{
    [RoutePrefix("api/ContentTemplates")]
    public class ContentTemplateController : ApiController
    {
        private readonly IContentTemplateData _templateRepo = null;

        public ContentTemplateController()
        {
            _templateRepo = new ContentTemplateData();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Add(ContentTemplate template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _templateRepo.AddOrUpdateContentTemplate(template);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Update(ContentTemplate template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _templateRepo.AddOrUpdateContentTemplate(template);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var page = await _templateRepo.GetContentTemplate(id);
            if (page == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(page);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<List<ContentTemplate>> GetPageTemplates(string filter)
        {
            var templates = await _templateRepo.GetContentTemplates(filter);
            return templates;
        }
  
    }
}
