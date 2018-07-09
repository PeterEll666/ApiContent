using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApiContent.DataAccess;
using ApiContent.Models;
using ApiContent.Models.DTOs;

namespace ApiContent.Controllers
{
    [RoutePrefix("api/Contents")]
    public class ContentController : ApiController
    {
        private readonly IContentData _cdtRepo = null;

        public ContentController()
        {
            _cdtRepo = new ContentData();;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Add(ContentDTO area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _cdtRepo.AddContent(area);
            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Update(ContentDTO area)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _cdtRepo.UpdateContent(area);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<List<Content>> GetContents(string filter)
        {
            var areas = await _cdtRepo.GetContents(filter);
            return areas;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<Content> GetContent(int id)
        {
            var areas = await _cdtRepo.GetContent(id);
            return areas;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}/all")]
        public async Task<ContentDTO> GetContentAll(int id, bool includeMarks = false, string language = null)
        {
            var areas = await _cdtRepo.GetContentAll(id, includeMarks, language);
            return areas;
        }
    }
}
