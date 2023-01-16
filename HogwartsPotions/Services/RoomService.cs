using HogwartsPotions.Data;
using HogwartsPotions.Models.Entities;
using HogwartsPotions.Models.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HogwartsPotions.Services.Interfaces;

namespace HogwartsPotions.Services;

public class RoomService : IRoomService
{
    private readonly HogwartsContext _context;

    public RoomService(HogwartsContext context)
    {
        _context = context;
    }

    public async Task AddRoom(Room room)
    {
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
    }

    public async Task<Room> GetRoom(long roomId)
    {
        foreach (var room in await _context.Rooms.Include(room => room.Residents).ToListAsync())
        {
            if (room.ID == roomId)
            {
                return room;
            }
        }
        return null;
    }

    public async Task<List<Room>> GetAllRooms()
    {
        return await _context.Rooms.Include(room => room.Residents).ToListAsync();
    }

    public async Task DeleteRoom(long id)
    {
        foreach (var room in _context.Rooms.ToList())
        {
            if (room.ID == id)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }

    public async Task<List<Room>> GetRoomsForRatOwners()
    {
        var goodRooms =await _context.Rooms.Include(r => r.Residents).ToListAsync();
        var allRooms = await _context.Rooms.Include(r => r.Residents).ToListAsync();
        foreach (var room in allRooms)
        {
            var petTypes = new List<PetType>();
            foreach (var student in room.Residents)
            {
                petTypes.Add(student.PetType);
            }

            if (petTypes.Contains(PetType.Cat) || petTypes.Contains(PetType.Owl))
            {
                goodRooms.Remove(room);
            }
        }
        return goodRooms;
    }

    public void Update(Room updatedRoom)
    {
        throw new System.NotImplementedException();
    }
}