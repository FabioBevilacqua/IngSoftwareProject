using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgettoIDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdineController : ControllerBase
    {
        private readonly DatabaseContext context;

        public OrdineController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var ordini = await context.Ordini.Where(item => item.Deleted_At == null).ToListAsync();
            if (ordini.Count > 0)
            {
                foreach (var ordine in ordini)
                {
                    ordine.OrdineProdotto = await this.context.OrdineProdotto.Where(item => item.IdOrdine == ordine.Id).ToListAsync();
                }
            }
            return Ok(ordini);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(int idUtente)
        {

            try
            {
                // verificare che l utente esista
                var utenteDaCercare = await this.context.Utenti.FirstAsync(item => item.Id == idUtente);
                if (utenteDaCercare == null)
                {
                    return BadRequest("Ordine non creabile, in quanto utente inesistente");
                }

                await this.context.Database.BeginTransactionAsync();
                var ordine = new Ordine { IdUtente = idUtente, Created_At = DateTime.Now, OrdineProdotto = new List<OrdineProdotto>() };
                var nuovoOrdine = await context.Ordini.AddAsync(ordine);
                await this.context.SaveChangesAsync();
                await this.context.Database.CommitTransactionAsync();
                return Ok(nuovoOrdine.Entity.Id);
            }
            catch (Exception e)
            {
                await this.context.Database.RollbackTransactionAsync();
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("AddProductToOrder")]
        public async Task<IActionResult> AddProductToOrder(int id, int idProdotto)
        {
            try
            {
                //Verifico se l'ordine esiste e non è stato eliminato
                var ordineFinded = await context.Ordini.Include(i => i.OrdineProdotto).FirstOrDefaultAsync(item => item.Id == id && item.Deleted_At == null);
                //Verifico se il prodotto esisten e non è stato cancellato dalla fase di creazione dell'ordine all'aggiunta del prodotto
                var prodotto = await this.context.Prodotti.FirstOrDefaultAsync(item => item.Id == idProdotto && item.Deleted_At == null);
                if (ordineFinded == null)
                {
                    return NotFound("Ordine non trovato");
                }
                if (prodotto == null)
                {
                    return NotFound("Prodotto non trovato");
                }

                await this.context.Database.BeginTransactionAsync();
                if (ordineFinded.OrdineProdotto == null)
                {
                    ordineFinded.OrdineProdotto = new List<OrdineProdotto>();
                }
                if (!ordineFinded.OrdineProdotto.Any(item => item.IdProdotto == idProdotto))
                {
                    ordineFinded.OrdineProdotto.Add(new OrdineProdotto { IdProdotto = idProdotto, IdOrdine = id, Quantità = 1, Totale = prodotto.Prezzo });
                }
                else
                {
                    var prodottoFinded = ordineFinded.OrdineProdotto.FirstOrDefault(item => item.IdProdotto == idProdotto);
                    if (prodottoFinded != null)
                    {
                        prodottoFinded.Quantità += 1;
                        prodottoFinded.Totale = prodottoFinded.Prodotto.Prezzo * prodottoFinded.Quantità;
                    }
                }


                await this.context.SaveChangesAsync();
                await this.context.Database.CommitTransactionAsync();
                return Ok();



            }
            catch (Exception e)
            {
                await this.context.Database.RollbackTransactionAsync();
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var ordineFinded = await context.Ordini.FirstOrDefaultAsync(item => item.Id == id && item.Deleted_At == null);
            if (ordineFinded != null)
            {
                try
                {
                    await this.context.Database.BeginTransactionAsync();
                    ordineFinded.Deleted_At = DateTime.Now;
                    await this.context.SaveChangesAsync();
                    await this.context.Database.CommitTransactionAsync();
                    return Ok();
                }
                catch (Exception e)
                {
                    await this.context.Database.RollbackTransactionAsync();
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return NotFound("Ordine inesistente");
            }
        }
    }
}
