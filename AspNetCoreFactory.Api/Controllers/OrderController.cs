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
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public OrderController(IServiceManager serviceManager, ILoggerManager logger, IMapper mapper)
        {
            _serviceManager = serviceManager;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _serviceManager.Order.GetAllOrdersAsync(trackChanges: false);
            var orderDto = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDto);
        }
        [HttpGet("GetOrdersByQuery")]
        public async Task<IActionResult> GetOrdersByQuery([FromBody] OrderDto orderDto)
        {
            await BuildDynamicOrderQuery(orderDto);
            return Ok(orderDto);
        }


        [HttpGet("{id}", Name = "OrderById")]
        public async Task<IActionResult> GetOrder(int id)
        {
            return await GetOrderById(id);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderForCreateDto order)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid model state for the OrderForCreateDto object");
                return UnprocessableEntity(ModelState);
            }
            if (order == null)
            {
                _logger.LogError("OrderForCreateDto object sent from client is null. ");
                return BadRequest("OrderForCreateDto object is null");
            }
            var orderEntity = _mapper.Map<Order>(order);
            await _serviceManager.Order.CreateOrderAsync(orderEntity);
            await _serviceManager.SaveAsync();
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            return CreatedAtRoute("OrderById", new { id = orderToReturn.Id }, orderToReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderForUpdateDto order)
        {
            if (order == null)
            {
                _logger.LogError($"OrderForUpdateDto object sent from client is null. ");
                return BadRequest("OrderForUpdateDto object is null");
            }
            var orderEntity = await _serviceManager.Order.GetOrderAsync(id, trackChanges: false);
            if (orderEntity == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(order, orderEntity);
            _serviceManager.Order.UpdateOrder(orderEntity);
            _serviceManager.Save();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOder(int id)
        {
            var order = await _serviceManager.Order.GetOrderAsync(id, trackChanges: false);
            if (order == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _serviceManager.Order.DeleteOrder(order);
            _serviceManager.Save();
            return NoContent();
        }

        #region Handlers
        private async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _serviceManager.Order.GetOrderAsync(id, trackChanges: false);
            if (order == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var orderDto = _mapper.Map<OrderDto>(order);
                return Ok(orderDto);
            }
        }
        private async Task BuildDynamicOrderQuery(OrderDto model)
        {
            var query = _serviceManager.GetOrderAsQueryable();
            if (model.CustomerId != null && model.CustomerId != 0)
            {
                query = query.Where(o => o.CustomerId == model.CustomerId);
            }
            if (model.ProductId != null && model.ProductId != 0)
            {
                query = query.Where(o => o.ProductId == model.ProductId);
            }
            if (model.OrderDateFrom != null && DateTime.TryParse(model.OrderDateFrom, out DateTime dateFrom))
            {
                query = query.Where(o => o.OrderDate >= dateFrom);
            }
            if (model.OrderDateThru != null && DateTime.TryParse(model.OrderDateThru,
                                                   out DateTime dateThru))
            {
                query = query.Where(o => o.OrderDate <= dateThru);
            }
            foreach (var order in await _serviceManager.ToListOrderAsync(query))
            {
                model.Orders.Add(_mapper.Map<Order, OrderDto>(order));
            }
        }
        #endregion
    }
}
