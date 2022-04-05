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
    public class DeleteRoomModel : PageModel
    {
        private IRoomService RoomService;

        [BindProperty]
        public Room Room { get; set; }

        public string InfoText;

        public DeleteRoomModel(IRoomService service)
        {
            RoomService = service;
        }
        
        public async Task<IActionResult> OnGet(int id, int hotelNr)
        {
            Room = await RoomService.GetRoomFromIdAsync(id, hotelNr);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            InfoText = "";

            try
            {
                await RoomService.DeleteRoomAsync(Room.RoomNr, Room.HotelNr);
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

            return RedirectToPage("GetAllRooms");
        }
    }
}