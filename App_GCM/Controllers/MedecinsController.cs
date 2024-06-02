using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedecinsController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        public MedecinsController(DB_GCMContext reactContext)
        {
            _reactContext = reactContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var medecins = await _reactContext.Medecins
           .Include(m => m.IdSpecialiteNavigation)
           .Select(m => new {
               m.Id,
               m.NomM,
               m.PrenomM,
               m.Numtel,
               m.Email,
               m.Occupe,
               Specialite = m.IdSpecialiteNavigation.Specialite1
           })
            .ToListAsync();
            /*var medecins = await _reactContext.Medecins.ToListAsync();*/
            return Ok(medecins);
 

        }

        [HttpPost]
        public async Task<IActionResult> Post(Medecin newMedecin)
        {
            _reactContext.Medecins.Add(newMedecin);
            await _reactContext.SaveChangesAsync();
            return Ok(newMedecin);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var medecinById = await _reactContext.Medecins.FindAsync(id);
            return Ok(medecinById);

        }
        [HttpPut]
        public async Task<IActionResult> Put(Medecin medecinToUpdate)
        {
            _reactContext.Medecins.Update(medecinToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(medecinToUpdate);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var medecinToDelete = await _reactContext.Medecins.FindAsync(id);
            if (medecinToDelete == null)
            {
                return NotFound();
            }
            _reactContext.Medecins.Remove(medecinToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }

    }
}
