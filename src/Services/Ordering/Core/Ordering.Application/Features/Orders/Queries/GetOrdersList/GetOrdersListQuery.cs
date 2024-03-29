﻿using MediatR;
using Ordering.Application.Models.Dtos.Orders;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersList
{
    public class GetOrdersListQuery : IRequest<List<OrderListDto>>
    {
        public string Username { get; set; }

        public GetOrdersListQuery(string username)
        {
            Username = username;
        }
    }
}
