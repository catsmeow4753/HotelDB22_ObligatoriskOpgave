using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        public List<Room> Rooms { get; private set; }

        private IRoomService roomService;

        public string InfoText;

        public GetAllRoomsModel(IRoomService rService)
        {
            roomService = rService;
        }

        public async Task OnGetAsync()
        {
            InfoText = "";

            try
            {
                if (!String.IsNullOrEmpty(FilterCriteria))
                {
                    Rooms = await roomService.GetRoomsByTypeAsync(FilterCriteria);
                }
                else
                {
                    Rooms = await roomService.GetAllRoomAsync();
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
        }
    }
}
