using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedDomain;
using SharedDomain.Contracts;
using LoggerService;
using AspNetCoreFactory.Api.DataTransferObjects;

namespace AspNetCoreFactory.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CustomerController(IServiceManager serviceManager, ILoggerManager logger, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _serviceManager.Customer.GetAllCustomersAsync(trackChanges: false);
            var customerDto = _mapper.Map<IEnumerable<CustomerDto>>(customers);
            return Ok(customerDto);
        }
        [HttpGet("{id}", Name = "CustomerById")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            return await GetCustomerById(id);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerForCreateDto customer)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the CustomerForCreateDto object");
                return UnprocessableEntity(ModelState);
            }
            if (customer == null)
            {
                _logger.LogError("CustomerForCreationDto object sent from client is null. ");
                return BadRequest("CustomerForCreationDto object is null");
            }
            var customerEntity = _mapper.Map<Customer>(customer);
            await _serviceManager.Customer.CreateCustomerAsync(customerEntity);
            await _serviceManager.SaveAsync();
            var customerToReturn = _mapper.Map<CustomerDto>(customerEntity);
            return CreatedAtRoute("CustomerById", new { id = customerToReturn.Id }, customerToReturn);
        }

        //[HttpPost("collection")]
        //public async Task<IActionResult> CreateCustomerCollection([FromBody] IEnumerable<CustomerForCreateDto> customerCollection)
        //{
        //    if(customerCollection == null)
        //    {
        //        _logger.LogError("Customer Collection  sent from client is null. ");
        //        return BadRequest("Customer collection object is null");
        //    }
        //    var customerEntities = _mapper.Map<IEnumerable<Customer>>(customerCollection);
        //    foreach (var customer in customerEntities)
        //    {
        //        await _serviceManager.Customer.CreateCustomerAsync(customer);
        //    }
        //    await _serviceManager.SaveAsync();


        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerForUpdateDto customer)
        {
            if (customer == null)
            {
                _logger.LogError($"CustomerForCreationDto object sent from client is null. ");
                return BadRequest("CustomerForCreationDto object is null");
            }
            var customerEntity = await _serviceManager.Customer.GetCustomerAsync(id, trackChanges: false);
            if (customerEntity == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(customer, customerEntity);
            _serviceManager.Customer.UpdateCustomer(customerEntity);
            _serviceManager.Save();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _serviceManager.Customer.GetCustomerAsync(id, trackChanges: false);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _serviceManager.Customer.DeleteCustomer(customer);
            _serviceManager.Save();
            return NoContent();
        }

        #region Handlers
        private async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _serviceManager.Customer.GetCustomerAsync(id, trackChanges: false);
            if (customer == null)
            {
                _logger.LogInfo($"Customer with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var customerDto = _mapper.Map<CustomerDto>(customer);
                return Ok(customerDto);
            }
        }
        #endregion
    }
}
