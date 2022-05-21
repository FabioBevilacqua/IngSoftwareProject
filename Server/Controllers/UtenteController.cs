using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgettoIDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtenteController : ControllerBase
    {
        private readonly DatabaseContext context;


        public UtenteController(DatabaseContext context)
        {
            this.context = context;
            
        }

        [HttpPost]
        public async Task<IActionResult> Post(Utente utente)
        {
            try
            {
                await this.context.Database.BeginTransactionAsync();
                
                //Verifica e test utente già esistente
                var verificaUtenteGiàEsistente = await context.Utenti.AnyAsync(find => find.Username == utente.Username);
                if (verificaUtenteGiàEsistente)
                {
                    throw new Exception("Attenzione, username già esistente");
                }

                await context.Utenti.AddAsync(utente); //Aggiungo
                await context.SaveChangesAsync(); //Scrivo sul database

                await this.context.Database.CommitTransactionAsync(); //Eseguo la commit
                return Ok();
            }
            catch (Exception e)
            {
                await this.context.Database.RollbackTransactionAsync();
                return StatusCode(500, e.Message);
            }
        }
       
    }
}
