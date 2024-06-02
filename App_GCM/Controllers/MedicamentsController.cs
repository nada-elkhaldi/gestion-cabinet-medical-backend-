using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentsController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        public MedicamentsController(DB_GCMContext reactContext)
        {
            _reactContext = reactContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var medicaments = await _reactContext.Medicaments.ToListAsync();
            return Ok(medicaments);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Medicament newMedicament)
        {
            _reactContext.Medicaments.Add(newMedicament);
            await _reactContext.SaveChangesAsync();
            return Ok(newMedicament);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var medicamentById = await _reactContext.Medicaments.FindAsync(id);
            return Ok(medicamentById);

        }
        [HttpPut]
        public async Task<IActionResult> Put(Medicament medicamentToUpdate)
        {
            _reactContext.Medicaments.Update(medicamentToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(medicamentToUpdate);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var medicamentToDelete = await _reactContext.Medicaments.FindAsync(id);
            if (medicamentToDelete == null)
            {
                return NotFound();
            }
            _reactContext.Medicaments.Remove(medicamentToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }

    }
}
