using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;
using WebAPI_PL.Controllers.Demo;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoApiController : ControllerBase
    {
        private readonly ILogger<DemoApiController> _logger;

        
        public DemoApiController(ILogger<DemoApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Product100")]
        public async Task<ActionResult<IEnumerable<TestProduct>>> GetProducts100()
        {
            _logger.LogInformation("Product100");
            return Ok(Enumerable.Range(1, 4)
                .Select(
                    i => new TestProduct(
                        "Lison",
                        "Bidon",
                        100,
                        3))
                .ToArray());
        }
        [HttpGet]
        [Route("Product200")]
        public async Task<ActionResult<IEnumerable<TestProduct>>> GetProducts200()
        {
            _logger.LogInformation("Product200");
            return Ok(Enumerable.Range(1, 4)
                .Select(
                    i => new TestProduct(
                        "Lison",
                        "Bidon",
                        200,
                        3))
                .ToArray());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TestProduct>> Get(int id)
        {
            _logger.LogInformation("Get ID");
            return Ok(new TestProduct(
                "Lison",
                "Bidon",
                200,
                id + 10));
        }

        [HttpPost]
        public async Task<ActionResult<TestProduct>> Post(TestProduct testProduct)
        {
            _logger.LogInformation("Set " + testProduct.Name);
            testProduct.Name += "SET";

            return Ok(testProduct);
        }
        [HttpPut]
        public async Task<ActionResult<TestProduct>> Put(TestProduct testProduct)
        {
            _logger.LogInformation("Put " + testProduct.Name);
            testProduct.Name += "PUT";

            return Ok(testProduct);
        }
        [HttpDelete]
        public async Task<ActionResult<TestProduct>> Delete(TestProduct testProduct)
        {
            _logger.LogInformation("Delete " + testProduct.Name);
            testProduct.Name += "DELETE";

            return Ok(testProduct);
        }
    }
}
