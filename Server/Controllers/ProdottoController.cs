



namespace ProgettoIDS.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdottoController : ControllerBase
{
    private readonly DatabaseContext context;

    public ProdottoController(DatabaseContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var prodotti = await context.Prodotti.Where(item => item.Deleted_At == null).ToListAsync();
        return Ok(prodotti);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var prodotto = await context.Prodotti.FirstOrDefaultAsync(item => item.Deleted_At == null && item.Id == id);
        return Ok(prodotto);
    }

    [HttpPost]
    public async Task<IActionResult> GetById(Prodotto prodotto)
    {
        var prodottoFindend = await context.Prodotti.AnyAsync(item => item.Deleted_At == null && item.Descrizione.ToLower() == prodotto.Descrizione.ToLower());
        if (prodottoFindend)
        {
            return BadRequest("Attenzione, esiste già un prodotto con la stessa descrizione");
        }
        if (prodotto.Id != 0)
        {
            return BadRequest("Attenzione, la chiave univoca del prodotto può essere inserità solo dal database");
        }
        if (prodotto.Quantita <= 0)
        {
            return BadRequest("Attenzione, la quantità non può essere uguale o minore di 0");
        }
        if (prodotto.Deleted_At != null)
        {
            return BadRequest("Attenzione, non è possibile salvare ed elimnare contestualmente un prodotto ");
        }
        try
        {
            await this.context.Database.BeginTransactionAsync();
            var inserted = await this.context.Prodotti.AddAsync(prodotto);
            await this.context.SaveChangesAsync();
            await this.context.Database.CommitTransactionAsync();

            return Ok(inserted.Entity.Id);
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
        var prodottoFindend = await context.Prodotti.FirstOrDefaultAsync(item => item.Deleted_At == null);
        if (prodottoFindend != null)
        {
            try
            {
                await this.context.Database.BeginTransactionAsync();
                prodottoFindend.Deleted_At = DateTime.Now;
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
            return NotFound("Prodotto inesistente");
        }
    }
}
