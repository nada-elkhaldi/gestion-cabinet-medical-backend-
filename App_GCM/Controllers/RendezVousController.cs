using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RendezVousController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        public RendezVousController(DB_GCMContext reactContext)
        {
            _reactContext = reactContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var rendezVous = await _reactContext.RendezVous
          .Include(m => m.IdMedecinNavigation )
          .Include(m=> m.IdPatientNavigation)
          .Select(m => new {
              m.Id,
              m.DateR,
              m.HeureR,
              m.Status,
              m.Email,
              NomM  = m.IdMedecinNavigation.NomM,
              PrenomM  = m.IdMedecinNavigation.PrenomM,
              EmailM = m.IdMedecinNavigation.Email,

              NomP  =m.IdPatientNavigation.NomP,
              PrenomP = m.IdPatientNavigation.PrenomP,
              EmailP  = m.IdPatientNavigation.Email
          })
           .ToListAsync();
            return Ok(rendezVous);
        }
        /*public async Task<IActionResult> Get()
        {
            // Récupérer le token d'authentification de l'utilisateur
            string token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Vérifier et décoder le token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Récupérer l'ID du médecin à partir du token
            int medecinId = int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value);

            // Vérifier le rôle de l'utilisateur
            string userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            if (userRole != "medecin")
            {
                return BadRequest("Rôle d'utilisateur non autorisé");
            }

            // Récupérer les rendez-vous du médecin connecté
            var rendezVous = await _reactContext.RendezVous
                .Include(m => m.IdMedecinNavigation)
                .Include(m => m.IdPatientNavigation)
                .Where(rv => rv.IdMedecin == medecinId)
                .Select(m => new
                {
                    m.Id,
                    m.DateR,
                    m.HeureR,
                    m.Status,
                    NomMedecin = m.IdMedecinNavigation.NomM,
                    PrenomMedecin = m.IdMedecinNavigation.PrenomM,
                    NomPatient = m.IdPatientNavigation.NomP,
                    PrenomPatient = m.IdPatientNavigation.PrenomP
                })
                .ToListAsync();

            return Ok(rendezVous);
        } */

        /*public async Task<IActionResult> Post(RendezVou newRv)
        {
            _reactContext.RendezVous.Add(newRv);
            await _reactContext.SaveChangesAsync();
            return Ok(newRv);

        }*/
        [HttpPost]
        public async Task<IActionResult> Post(RendezVou newRv)
        {
            // Calculer la date et l'heure de fin du rendez-vous en ajoutant une heure à l'heure de début
            DateTime finRendezVous = newRv.DateR.Value.Date + newRv.HeureR.Value + TimeSpan.FromHours(1);

            // Vérifier si le médecin a déjà un rendez-vous qui chevauche la plage horaire du nouveau rendez-vous
             bool isConflict = _reactContext.RendezVous
               .AsEnumerable()
               .Any(rv => rv.IdMedecin == newRv.IdMedecin
               && rv.DateR == newRv.DateR
               && rv.HeureR < finRendezVous.TimeOfDay
               && rv.HeureR + TimeSpan.FromHours(1) > newRv.HeureR);
            if (isConflict)
            {
                return BadRequest("Le médecin a déjà un rendez-vous qui chevauche la plage horaire choisie. Veuillez choisir une autre date/heure.");
            }

            _reactContext.RendezVous.Add(newRv);
            await _reactContext.SaveChangesAsync();
            return Ok(newRv);
        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var rvById = await _reactContext.RendezVous.FindAsync(id);
            return Ok(rvById);

        }
        [HttpPut]
        public async Task<IActionResult> Put(RendezVou rvToUpdate)
        {
            _reactContext.RendezVous.Update(rvToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(rvToUpdate);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var rvToDelete = await _reactContext.RendezVous.FindAsync(id);
            if (rvToDelete == null)
            {
                return NotFound();
            }
            _reactContext.RendezVous.Remove(rvToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }


        [HttpGet("medecin/{idMedecin}")]
        public IActionResult GetRendezVousByMedecin(int idMedecin)
        {
            var rendezVous = _reactContext.RendezVous
                .Include(rv => rv.IdMedecinNavigation)
                .Include(rv => rv.IdPatientNavigation)
                .Where(rv => rv.IdMedecin == idMedecin)
                .Select(rv => new RendezVousDto
                {
                    Id = rv.Id,
                    Email = rv.Email,
                    DateR = rv.DateR.Value,
                    HeureR = rv.HeureR.Value,
                    Status = rv.Status,
                    NomPatient = rv.IdPatientNavigation.NomP,
                  
                    PrenomPatient = rv.IdPatientNavigation.PrenomP,
                    NomMedecin = rv.IdMedecinNavigation.NomM,
                    PrenomMedecin = rv.IdMedecinNavigation.PrenomM,
                    
                })
                .ToList();

            return Ok(rendezVous);
        }

        [HttpGet("patient/{idPatient}")]
        public IActionResult GetRendezVousByPatient(int idPatient)
        {
            var rendezVous = _reactContext.RendezVous
                .Include(rv => rv.IdMedecinNavigation)
                .Include(rv => rv.IdPatientNavigation)
                .Where(rv => rv.IdPatient == idPatient)
                .Select(rv => new RendezVousDto
                {
                    Id = rv.Id,
                    Email = rv.Email,
                    DateR = rv.DateR.Value,
                    HeureR = rv.HeureR.Value,
                    Status = rv.Status,
                    NomPatient = rv.IdPatientNavigation.NomP,

                    PrenomPatient = rv.IdPatientNavigation.PrenomP,
                    NomMedecin = rv.IdMedecinNavigation.NomM,
                    PrenomMedecin = rv.IdMedecinNavigation.PrenomM,

                })
                .ToList();

            return Ok(rendezVous);
        }

    }
}
