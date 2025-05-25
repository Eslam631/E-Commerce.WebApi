using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.IdentityModel;
using Domain.Models.OrderModules;
using Domain.Models.ProductModel;
using Service.Specification;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;
using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto orderDto, string Email)
        {
            //Address
            var OrderAddress = _mapper.Map<AddressDto,OrderAddress>(orderDto.shipToAddress);


            //OrderItems

            var Basket=await _basketRepository.GetBasketAsync(orderDto.BasketId)??throw new BasketNotFoundException(orderDto.BasketId);


            ICollection<OrderItem> OrderItems = [];

            var Repo = _unitOfWork.GetRepository<Product, int>();

            foreach (var items in Basket.Items)
            {
                var Product = await Repo.GetByIdAsync(items.Id) ?? throw new ProductNotFoundException(items.Id);




                OrderItems.Add(CreateOrder(items, Product));


            }


            var DeliveryMethod=await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)??throw new DeliveryNotFoundException(orderDto.DeliveryMethodId);


            var SubTotal = OrderItems.Sum(I => I.Quantity * I.Price);


            var Order=new Order(Email,OrderAddress,OrderItems, SubTotal,DeliveryMethod);


            await _unitOfWork.GetRepository<Order,Guid>().AddASYNC(Order);

            var Result = await _unitOfWork.SaveChangeAsync();

            return Result >0 ? _mapper.Map<Order, OrderToReturnDto>(Order):throw new Exception("Can't Create Order Please Try Again!") ; 



        }

        private static OrderItem CreateOrder(Domain.Models.BasketModel.BasketItem items, Product Product)
        {
            return new OrderItem()
            {

                ProductItemOrdered = new ProductItemOrdered()
                {
                    ProductId = Product.Id,
                    PictureUrl = Product.PictureUrl,
                    ProductName = Product.Name,
                },
                Quantity = items.Quantity,

                Price = Product.Price
            };
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethodAsync()
        {


           var Delivery=await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();


           
            return _mapper.Map<IEnumerable<DeliveryMethod>,IEnumerable<DeliveryMethodDto>>(Delivery);
          
             
           
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string Email)
        {
            var Spec = new OrderSpecification(Email);
            var Orders =await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);

            if (Orders is null)
                throw new OrderNotFoundByEmail(Email);
            else
                return _mapper.Map<IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid Id)
        {
            var Spec=new OrderSpecification(Id);
           var Order=await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);


            if (Order is null)
                throw new OrderNotFoundByEmail(Id);
            else
                return _mapper.Map<OrderToReturnDto>(Order);
        }
    }
}
