using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlTagsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HtmlTagsCounterController : ControllerBase
    {
        private readonly IHtmlTagsCounterService _service;
        private readonly ILogger<HtmlTagsCounterController> _logger;

        public HtmlTagsCounterController(ILogger<HtmlTagsCounterController> logger, IHtmlTagsCounterService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost("generate")]
        public ActionResult<IEnumerable<HtmlTagsCounter>> Generate(
            [FromBody]WebsiteNameRequest request)
        {
            var result = _service.Get(request.siteName);
            return Ok(result);
        }

    }
}
