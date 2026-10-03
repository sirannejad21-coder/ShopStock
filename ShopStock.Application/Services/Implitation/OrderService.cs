using ShopStock.Application.Services.Interfaces;
using ShopStock.Domain.Contracts;
using ShopStock.Domain.Models.Prouducts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopStock.Application.Services.Implitation
{
    public class OrderService:IOrderService
    {

        IOrderRepository _OrderService;
        IGenrericRepository<Prouduct> _GenrericRepository;

        public OrderService(IOrderRepository orderService, IGenrericRepository<Prouduct> genrericRepository)
        {
            _OrderService = orderService;
            _GenrericRepository = genrericRepository;
        }
    }
}
