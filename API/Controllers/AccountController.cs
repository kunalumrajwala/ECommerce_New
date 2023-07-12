using System.Security.Claims;
using API.Dtos;
using API.Error;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInmanager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController (UserManager<AppUser> userManager
        , SignInManager<AppUser> signInmanager
        , ITokenService tokenService
        , IMapper mapper) 
        {
            this._signInmanager = signInmanager;
            this._tokenService = tokenService;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser(){

            var user = await _userManager.FindByEmailFromClaimsPrincipal(User);

            return new UserDto{
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }


        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> checkEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> getUserAddress()
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(User);

            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> updateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            user.Address = _mapper.Map<AddressDto, Address>(address);
            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded) 
                return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            
            return BadRequest("Problem to updating the user");
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user == null) return Unauthorized(new ApiResponse(401));
            var result =  await _signInmanager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        
            if(!result.Succeeded) return Unauthorized(new ApiResponse(401));
        
            return new UserDto{
                Email = loginDto.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }
    
    
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if(!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto{
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email                
            };
        } 
    }
}