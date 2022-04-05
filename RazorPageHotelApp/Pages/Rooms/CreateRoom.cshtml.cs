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
    public class CreateRoomModel : PageModel
    {
        private IHotelService HotelService;
        private IRoomService RoomService;

        [BindProperty]
        public Room Room { get; set; }

        public string InfoText;

        public List<SelectListItem> TypesOptions { get; set; }

        public CreateRoomModel(IHotelService hotelService, IRoomService roomService)
        {
            HotelService = hotelService;
            RoomService = roomService;

            Room = new Room();
        }

        public IActionResult OnGet(int id)
        {
            Room.HotelNr = id;

            CreateTypesList();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await RoomService.CreateRoomAsync(Room);

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