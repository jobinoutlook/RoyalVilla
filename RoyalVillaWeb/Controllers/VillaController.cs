using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoyalVilla.DTO;
using RoyalVilla.DTO.VillaDTOs;
using RoyalVillaWeb.Models;
using RoyalVillaWeb.Services.IServices;
using System.Diagnostics;

namespace RoyalVillaWeb.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly VillaMapper _mapper;

        public VillaController(IVillaService villaService, VillaMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            List<VillaDTO> villaList = new();
            try
            {
                var response = await _villaService.GetAllAsync<ApiResponse<List<VillaDTO>>>("");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    villaList = response.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
            }


            return View(villaList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VillaCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createDTO);
            }

            try
            {
                var response = await _villaService.CreateAsync<ApiResponse<VillaDTO>>(createDTO, "");
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Error creating villa.";

                    return View(createDTO);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An Error Occured {ex.Message}";
                return View(createDTO);
            }

            
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["error"] = "Invalid villa ID";

                return RedirectToAction(nameof(Index));

            }

            try
            {
                var response = await _villaService.GetAsync<ApiResponse<VillaDTO>>(id, "");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    
                    return View(response.Data);
                }
                
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An Error Occured {ex.Message}";
               
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VillaDTO villaDTO)
        {
            

            try
            {
                var response = await _villaService.DeleteAsync<ApiResponse<object>>(villaDTO.Id,"");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    TempData["success"] = "Villa deleted successfully!";
                   
                }
                
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An Error Occured {ex.Message}";
                
            }

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                TempData["error"] = "Invalid villa ID";

                return RedirectToAction(nameof(Index));

            }

            try
            {
                var response = await _villaService.GetAsync<ApiResponse<VillaDTO>>(id, "");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    var updateDTO =_mapper.ToUpdateDTO(response.Data);
                    return View(updateDTO);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = $"An Error Occured {ex.Message}";

            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VillaUpdateDTO updateDTO)
        {


            try
            {
                var response = await _villaService.UpdateAsync<ApiResponse<object>>(updateDTO, "");
                if (response != null && response.IsSuccess && response.Data != null)
                {
                    TempData["success"] = "Villa updated successfully!";

                }

            }
            catch (Exception ex)
            {
                TempData["error"] = $"An Error Occured {ex.Message}";

            }

            return RedirectToAction(nameof(Index));

        }

    }
}
