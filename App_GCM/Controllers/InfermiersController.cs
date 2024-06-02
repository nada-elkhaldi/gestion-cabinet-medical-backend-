using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfermiersController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        public InfermiersController(DB_GCMContext reactContext)
        {
            _reactContext = reactContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var infermiers = await _reactContext.Infermiers
           .Include(m => m.IdMedecinNavigation)
           .Select(m => new {
               m.Id,
               m.NomInf,
               m.PrenomInf,
               m.Nomtele,
               m.Email,
              
               NomM = m.IdMedecinNavigation.NomM,
               PrenomM = m.IdMedecinNavigation.PrenomM
           })
            .ToListAsync();
            //var infermiers = await _reactContext.Infermiers.ToListAsync();
            return Ok(infermiers);
 

        }

        [HttpPost]
        public async Task<IActionResult> Post(Infermier newInfermier)
        {
            _reactContext.Infermiers.Add(newInfermier);
            await _reactContext.SaveChangesAsync();
            return Ok(newInfermier);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var infermierById = await _reactContext.Infermiers.FindAsync(id);
            return Ok(infermierById);

        }
        [HttpPut]
        public async Task<IActionResult> Put(Infermier infermierToUpdate)
        {
            _reactContext.Infermiers.Update(infermierToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(infermierToUpdate);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var infermierToDelete = await _reactContext.Infermiers.FindAsync(id);
            if (infermierToDelete == null)
            {
                return NotFound();
            }
            _reactContext.Infermiers.Remove(infermierToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }

    }
}
