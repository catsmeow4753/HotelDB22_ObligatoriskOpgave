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
    public class GetAllHotelsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Hotel> Hotels { get; private set; }

        private IHotelService HotelService;

        public string InfoText;

        public GetAllHotelsModel(IHotelService service)
        {
            HotelService = service;

            Hotels = new List<Hotel>();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            InfoText = "";

            try
            {
                if (!String.IsNullOrEmpty(FilterCriteria))
                {
                    Hotels = await HotelService.GetHotelsByNameAsync(FilterCriteria);
                }
                else
                {
                    Hotels = await HotelService.GetAllHotelAsync();
                }
            }
            catch (SqlException sqlEx)
            {
                InfoText = "Database Error\n" + sqlEx.Message;
            }
            catch (Exception ex)
            {
                InfoText = "General Error\n" + ex.Message;
            }

            return Page();
        }
    }
}