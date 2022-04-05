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
    public class CreateModel : PageModel
    {
        private IHotelService HotelService;

        [BindProperty]
        public Hotel Hotel { get; set; }

        public string InfoText;

        public CreateModel(IHotelService service)
        {
            HotelService = service;

            Hotel = new Hotel();
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
               return Page();
            }

            InfoText = "";

            try
            {
                await HotelService.CreateHotelAsync(Hotel);
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