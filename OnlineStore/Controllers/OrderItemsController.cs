﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using Microsoft.Extensions.Logging;
using AutoMapper;
using OnlineStore.Data.Entities;
using OnlineStore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace OnlineStore.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IStoreRepository _repository;
        private readonly ILogger<OrderItemsController> _logger;
        private readonly IMapper _mapper;

        public OrderItemsController(
            IStoreRepository repository,
            ILogger<OrderItemsController> logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Get(int orderId)
        {
            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order != null) return Ok(_mapper.Map <IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repository.GetOrderById(User.Identity.Name, orderId);
            if (order != null)
            {
                var item = order.Items.Where(i => i.Id == id).FirstOrDefault();
                if(item != null)
                {
                    return Ok(_mapper.Map<OrderItem, OrderItemViewModel>(item));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderItemViewModel model)
        {
            //add it to the db
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrderItem = _mapper.Map<OrderItemViewModel, OrderItem>(model);

                    _repository.AddEntity(newOrderItem);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrderItem.Id}", _mapper.Map<OrderItem, OrderItemViewModel>(newOrderItem));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new order: {ex}");
            }
            return BadRequest("Failed to save new order");

        }
    }
}