using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Pages.Rooms
{
    public class UpdateRoomModel : PageModel
    {
        private IRoomService RoomService;

        [BindProperty]
        public Room Room { get; set; }

        public string InfoText;

        public List<SelectListItem> TypesOptions { get; set; }

        public UpdateRoomModel(IRoomService service)
        {
            RoomService = service;
        }

        public async Task<IActionResult> OnGet(int id, int hotelNr)
        {
            Room = await RoomService.GetRoomFromIdAsync(id, hotelNr);

            CreateTypesList();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await RoomService.UpdateRoomAsync(Room, Room.RoomNr, Room.HotelNr);

            return RedirectToPage("GetAllRooms");
        }

        public void CreateTypesList()
        {
            TypesOptions = new List<SelectListItem>()
            {
                new SelectListItem("Single", "S"),
                new SelectListItem("Family", "F"),
                new SelectListItem("Double", "D")
            };
        }
    }
}