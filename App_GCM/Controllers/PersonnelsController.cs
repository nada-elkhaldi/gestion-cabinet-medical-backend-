using App_GCM.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace App_GCM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelsController : ControllerBase
    {
        private readonly DB_GCMContext _reactContext;
        private readonly IConfiguration _configuration;
        public PersonnelsController(DB_GCMContext reactContext, IConfiguration configuration)
        {
            _reactContext = reactContext;
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pAdm = await _reactContext.Personnels.ToListAsync();
            
            return Ok(pAdm);
 

        }

        [HttpPost]
        public async Task<IActionResult> Post(Personnel newPAdm)
        {
            _reactContext.Personnels.Add(newPAdm);
            await _reactContext.SaveChangesAsync();
            return Ok(newPAdm);

        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var pAdmById = await _reactContext.Personnels.FindAsync(id);
            return Ok(pAdmById);

        }
        [HttpPut]
        public async Task<IActionResult> Put(Personnel pAdmToUpdate)
        {
            _reactContext.Personnels.Update(pAdmToUpdate);
            await _reactContext.SaveChangesAsync();
            return Ok(pAdmToUpdate);
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pAdmToDelete = await _reactContext.Personnels.FindAsync(id);
            if (pAdmToDelete == null)
            {
                return NotFound();
            }
            _reactContext.Personnels.Remove(pAdmToDelete);
            await _reactContext.SaveChangesAsync();
            return Ok();

        }

        //login
        [HttpPost]
        [Route("register")]
        public string registration(Personnel p)
        {
            SqlConnection con=new SqlConnection(_configuration.GetConnectionString("ReactGcmConnection").ToString());
            con.Open();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(p.Password);
            SqlCommand cmd = new SqlCommand("INSERT INTO personnels(nom, prenom, tele, role, email, password) VALUES(@Nom, @Prenom, @Tele, @Role, @Email, @Password)", con);
            cmd.Parameters.AddWithValue("@Nom", p.Nom);
            cmd.Parameters.AddWithValue("@Prenom", p.Prenom);
            cmd.Parameters.AddWithValue("@Tele", p.Tele);
            cmd.Parameters.AddWithValue("@Role", p.Role);
            cmd.Parameters.AddWithValue("@Email", p.Email);
            cmd.Parameters.AddWithValue("@Password", hashedPassword);
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Data inserted";
            }
            else
                return "Error";
        }

        /* public string login(Personnel p)
         {
             SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ReactGcmConnection").ToString());
             SqlDataAdapter data = new SqlDataAdapter("SELECT * FROM personnels WHERE email='" + p.Email + "'", con);
             DataTable dt = new DataTable();
             data.Fill(dt);

             if (dt.Rows.Count > 0)
             {
                 string hashedPasswordFromDB = dt.Rows[0]["password"].ToString();
                 if (BCrypt.Net.BCrypt.Verify(p.Password, hashedPasswordFromDB))
                 {
                     string token = GenerateToken(p);
                     return token;

                 }
             }
             return "Invalid";
         }*/
        [HttpPost]
        [Route("login")]
        public string Login(Personnel p)
        {
            string connectionString = _configuration.GetConnectionString("ReactGcmConnection").ToString();
            string query = "SELECT * FROM personnels WHERE email = @Email";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Email", p.Email);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string hashedPasswordFromDB = reader["password"].ToString();
                            if (BCrypt.Net.BCrypt.Verify(p.Password, hashedPasswordFromDB))
                            {
                                string userRole = reader["role"].ToString();
                                
                                string role = string.IsNullOrEmpty(userRole) ? "patient" : userRole;
                                p.Role = role;

                                string token = GenerateToken(p);
                                HttpContext httpContext = HttpContext;
                                var option = new CookieOptions
                                {
                                    Expires = DateTime.Now.AddHours(1),
                                    HttpOnly = true, // Empêche l'accès au cookie via JavaScript
                                   
                                    SameSite = SameSiteMode.Strict // Restreint le cookie à être envoyé uniquement dans le même site
                                };
                               

                                httpContext.Response.Cookies.Append("token", token, option);
                                return token;
                                //return "success";
                            }
                        }
                    }
                }
            }

            return "Invalid";
        }

        private string GenerateToken(Personnel p)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, p.Email),
            new Claim("id", p.Id.ToString()),
            new Claim("role", p.Role) // Ajout de la revendication de rôle
        };

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
        
    }
 
