using System;
using System.CodeDom.Compiler;
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
    [RoutePrefix("api/Texts")]
    public class TextController : ApiController
    {
        private readonly ITextData _cdtRepo = null;

        public TextController()
        {
            _cdtRepo = new TextData();;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Add(TextDTO text)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _cdtRepo.AddOrUpdateText(text);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Update(TextDTO text)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = await _cdtRepo.AddOrUpdateText(text);
            if (id == -1)
            {
                return NotFound();
            }
            return Ok();

        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("Lang")]
        public async Task<IHttpActionResult> Add(TextLang lang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _cdtRepo.AddTextLang(lang);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Lang")]
        public async Task<IHttpActionResult> Update(TextLang lang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _cdtRepo.UpdateTextLang(lang);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<TextDTO> GetText(int id, string language="")
        {
            var areas = await _cdtRepo.GetText(id, language);
            return areas;
        }
        
    }
}
