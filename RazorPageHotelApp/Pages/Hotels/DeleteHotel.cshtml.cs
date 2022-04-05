using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        private IHotelService HotelService;

        [BindProperty]
        public Hotel hotel { get; set; }

        public string InfoText;

        public DeleteHotelModel(IHotelService service)
        {
            HotelService = service;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            hotel = await HotelService.GetHotelFromIdAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            InfoText = "";

            try
            {
                await HotelService.DeleteHotelAsync(id);
            }
            catch (SqlException sqlEx)
            {
                InfoText = "Database Error\n" + sqlEx.Message;

                return Page();
            }
            catch (Exception ex)
            {
                InfoText = "General Error\n" + ex.Message;

                return Page();
            }

            return RedirectToPage("GetAllHotels");
        }
    }
}
