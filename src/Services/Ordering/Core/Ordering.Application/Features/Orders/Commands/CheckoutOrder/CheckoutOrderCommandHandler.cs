﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Infrastructure;
using Ordering.Application.Models;
using Ordering.Domain.Entities;
using Shared.Exceptions;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
            IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<Guid> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var isCreated = await _orderRepository.AddAsync(orderEntity);
            if (isCreated == 0)
                throw new BadRequestException("An error occurred during order checkout! Please try again!");

            _logger.LogInformation($"Order {orderEntity.Id} is successfully created.");
            await SendMail(orderEntity);

            return orderEntity.Id;
        }

        private async Task SendMail(Order order)
        {
            var email = new Email() { To = "usersMail@gmail.com", Body = "Order was created", Subject = "Order was created." };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
