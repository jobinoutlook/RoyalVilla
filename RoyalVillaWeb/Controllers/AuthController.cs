using Microsoft.AspNetCore.Mvc;
using RoyalVilla.DTO;
using RoyalVilla.DTO.VillaDTOs;
using RoyalVillaWeb.Models;
using RoyalVillaWeb.Services.IServices;
using System.Diagnostics;

namespace RoyalVillaWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly VillaMapper _mapper;

        public AuthController(IAuthService authService, VillaMapper mapper)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var response = await _authService.LoginAsync<ApiResponse<LoginResponseDTO>>(loginRequestDTO);
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    LoginResponseDTO model = response.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
            }


            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            var registrationRequestDTO = new RegisterationRequestDTO()
            {
                Email = string.Empty,
                Name = string.Empty,
                Password = string.Empty,
                Role = "Customer"
            };

            return View(registrationRequestDTO);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            return View();
        }

        
    }
}
