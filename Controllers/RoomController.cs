using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using HogwartsPotions.Models;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/room")]
    public class RoomController : ControllerBase
    {
        private readonly HogwartsContext _context;

        public RoomController(HogwartsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.GetAllRooms();
        }

        [HttpPost]
        public async  Task AddRoom([FromBody] Room room)
        {
            await _context.AddRoom(room);
        }

        [HttpGet("/{id}")]
        public async Task<Room> GetRoomById(long id)
        {
            return await _context.GetRoom(id);
        }

        [HttpPut("/{id}")]
        public void UpdateRoomById(long id, [FromBody] Room updatedRoom)
        {
            _context.Update(updatedRoom);
        }

        [HttpDelete("/{id}")]
        public async Task DeleteRoomById(long id)
        {
            await _context.DeleteRoom(id);
        }

        [HttpGet("/rat-owners")]
        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _context.GetRoomsForRatOwners();
        }

        [HttpGet("/potions")]
        public async Task<List<Potion>> GetAllPotions()
        {
            return await _context.GetAllPotions();
        }

        [HttpPost("/potions/{id}")]
        public async Task<Potion> BrewPotion(long id,[FromBody] List<Ingredient> data)
        {
            return await _context.BrewPotion(id, data);
        }

        [HttpGet("/potions/{id}")]
        public async Task<List<Potion>> GetAllPotionsOfStudent(long id)
        {
            return await _context.GetAllPotionsOfStudent(id);
        }

        [HttpPost("/potions/brew/{id}")]
        public async Task<Potion> BrewPotionSlowly(long id)
        {
            return await _context.BrewPotionSlowly(id);
        }

        [HttpPut("/potions/{id}/add")]
        public async Task<Potion> AddIngredient(long id ,[FromBody] Ingredient ingredient)
        {
            return await _context.AddIngredientToPotion(id, ingredient);
        }

        [HttpGet("/potions/{id}/help")]
        public async Task<List<Recipe>> GetHelp(long id)
        {
            return await _context.GetHelp(id);
        }
    }
}
