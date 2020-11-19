﻿using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderId}/items")]
    public class OrderItemsController : Controller
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrderItemsController> logger;
        private readonly IMapper mapper;

        public OrderItemsController(IDutchRepository repository, 
            ILogger<OrderItemsController> logger, 
            IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(int orderId)
        {
            var order = repository.GetOdrerById(orderId);
            if (order != null)
            {
                return Ok(mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = repository.GetOdrerById(orderId);
            if (order != null)
            {
                var item = order.Items.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    return Ok(mapper.Map<OrderItem, OrderItemViewModel>(item));
                }                
            }
            
            return NotFound();            
        }
    }
}
