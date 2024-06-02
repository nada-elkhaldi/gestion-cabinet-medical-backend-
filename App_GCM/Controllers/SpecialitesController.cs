using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialitesController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        public SpecialitesController(DB_GCMContext reactContext)
        {
            _reactContext = reactContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            var specialites = await _reactContext.Specialites.ToListAsync();
            return Ok(specialites);
 

        }

        [HttpPost]
        public async Task<IActionResult> Post(Specialite newSpecialite)
        {
            _reactContext.Specialites.Add(newSpecialite);
            await _reactContext.SaveChangesAsync();
            return Ok(newSpecialite);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var specialiteById = await _reactContext.Specialites.FindAsync(id);
            return Ok(specialiteById);

        }
        [HttpPut]
        public async Task<IActionResult> Put(Specialite specialiteToUpdate)
        {
            _reactContext.Specialites.Update(specialiteToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(specialiteToUpdate);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var specialiteToDelete = await _reactContext.Specialites.FindAsync(id);
            if (specialiteToDelete == null)
            {
                return NotFound();
            }
            _reactContext.Specialites.Remove(specialiteToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }

    }
}
