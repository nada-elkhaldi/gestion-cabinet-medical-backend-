using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DossiersMedicauxController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        public DossiersMedicauxController(DB_GCMContext reactContext)
        {
            _reactContext = reactContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dossier = await _reactContext.DossiersMedicauxes
          .Include(m => m.IdMedecinNavigation )
          .Include(m=> m.IdPatientNavigation)
          .Select(m => new {
              m.Id,
              m.TypeTraitement,
              m.AntecedentsMed,
              NomM = m.IdMedecinNavigation.NomM,
              PrenomM = m.IdMedecinNavigation.PrenomM,
              NomP =m.IdPatientNavigation.NomP,
              PrenomP = m.IdPatientNavigation.PrenomP
          })
           .ToListAsync();
            return Ok(dossier);
        }

        [HttpPost]
        public async Task<IActionResult> Post(DossiersMedicaux dossier)
        {
            _reactContext.DossiersMedicauxes.Add(dossier);
            await _reactContext.SaveChangesAsync();
            return Ok(dossier);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var dossier = await _reactContext.DossiersMedicauxes.FindAsync(id);
            return Ok(dossier);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DossiersMedicaux dossier)
        {
            var existingDossier = await _reactContext.DossiersMedicauxes.FindAsync(id);
            if (existingDossier == null)
            {
                return NotFound();
            }

            existingDossier.TypeTraitement = dossier.TypeTraitement;
            existingDossier.AntecedentsMed = dossier.AntecedentsMed;
            existingDossier.IdMedecin = dossier.IdMedecin;
            existingDossier.IdPatient = dossier.IdPatient;

            await _reactContext.SaveChangesAsync();
            return Ok(existingDossier);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var dossierToDelete = await _reactContext.DossiersMedicauxes.FindAsync(id);
            if (dossierToDelete == null)
            {
                return NotFound();
            }
            _reactContext.DossiersMedicauxes.Remove(dossierToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }

    }
}
