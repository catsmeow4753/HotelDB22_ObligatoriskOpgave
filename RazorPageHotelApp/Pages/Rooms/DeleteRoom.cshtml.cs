using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            await RoomService.DeleteRoomAsync(Room.RoomNr, Room.HotelNr);

            return RedirectToPage("GetAllRooms");
        }
    }
}