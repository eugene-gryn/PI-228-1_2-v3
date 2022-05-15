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
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts100()
        {
            _logger.LogInformation("Product100");
            return Ok(Enumerable.Range(1, 4)
                .Select(
                    i => new Product(
                        "Lison",
                        "Bidon",
                        100,
                        3))
                .ToArray());
        }
        [HttpGet]
        [Route("Product200")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts200()
        {
            _logger.LogInformation("Product200");
            return Ok(Enumerable.Range(1, 4)
                .Select(
                    i => new Product(
                        "Lison",
                        "Bidon",
                        200,
                        3))
                .ToArray());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            _logger.LogInformation("Get ID");
            return Ok(new Product(
                "Lison",
                "Bidon",
                200,
                id + 10));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            _logger.LogInformation("Set " + product.Name);
            product.Name += "SET";

            return Ok(product);
        }
        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            _logger.LogInformation("Put " + product.Name);
            product.Name += "PUT";

            return Ok(product);
        }
        [HttpDelete]
        public async Task<ActionResult<Product>> Delete(Product product)
        {
            _logger.LogInformation("Delete " + product.Name);
            product.Name += "DELETE";

            return Ok(product);
        }
    }
}
