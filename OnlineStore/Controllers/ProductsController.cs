using System;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using Microsoft.Extensions.Logging;
using OnlineStore.Data.Entities;
using AutoMapper;
using OnlineStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly IStoreRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public ProductsController(IStoreRepository repository, 
                                  ILogger<ProductsController> logger,
                                  IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return BadRequest("Failed to get products"); 
            }
            
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody]ProductViewModel model)
        {
                if (ModelState.IsValid)
                {
                    var newProduct = _mapper.Map<ProductViewModel, Product>(model);
                _repository.AddProduct(newProduct);
                return Ok();
                    
                }
                else
                {
                    return BadRequest(ModelState);
                }
        }
    } 
}
    