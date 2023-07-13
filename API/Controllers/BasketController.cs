using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        IBasketRepository _repo;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string Id)
        {
            var basket = await _repo.GetBasketAsync(Id);
            return Ok(basket ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var updatedBasket = await _repo.UpdateBasketAsync(customerBasket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string Id)
        {
            await _repo.DeleteBasketAsync(Id);
        }
    }
}