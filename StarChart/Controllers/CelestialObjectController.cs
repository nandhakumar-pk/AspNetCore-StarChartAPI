using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var celestialObject = await _context.CelestialObjects.FirstOrDefaultAsync(e => e.Id == Id);
            if (celestialObject == null)
                return NotFound();
            var satellite = await _context.CelestialObjects.FindAsync(celestialObject.Id);
            if (satellite != null)
                celestialObject.Satellites.Add(satellite);
            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var celestialObject = await _context.CelestialObjects.FirstOrDefaultAsync(e => e.Name == name);
            if (celestialObject == null)
                return NotFound();
            var satellite = await _context.CelestialObjects.FindAsync(celestialObject.Id);    
            if (satellite != null)
                celestialObject.Satellites.Add(satellite);
            return Ok(celestialObject);
        }

        [HttpGet]
        public void GetAll()
        {
           // var celestials = _context.CelestialObjects.ToArrayAsync();
           // return celestials;
        }
    }
}
