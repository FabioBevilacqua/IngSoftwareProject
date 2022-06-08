



namespace ProgettoIDS.Controllers;

[ApiController]
[Route("api/[controller]")]
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
        return StatusCode(200,prodotti);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var prodotto = await context.Prodotti.FirstOrDefaultAsync(item => item.Deleted_At == null && item.Id == id);
        if (id < 0){
            return StatusCode(400,"Attenzione, valore non valido");
        }
        if (prodotto == null){
            return StatusCode(404,"Prodotto inesistente");
        }
        if (id == 0){
            return StatusCode(400,"Attenzione, parametro id non inserito o uguale a 0");
        }
        return StatusCode(200,prodotto);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Prodotto prodotto)
    {
        var prodottoFindend = await context.Prodotti.AnyAsync(item => item.Deleted_At == null && item.Descrizione.ToLower() == prodotto.Descrizione.ToLower());
        if (prodottoFindend)
        {
            return StatusCode(400,"Attenzione, esiste gi� un prodotto con la stessa descrizione");
        }
        if (prodotto.Id != 0)
        {
            return StatusCode(400,"Attenzione, la chiave univoca del prodotto pu� essere inserit� solo dal database");
        }
        if (prodotto.Quantita <= 0)
        {
            return StatusCode(400,"Attenzione, la quantit� non pu� essere uguale o minore di 0");
        }
        if (prodotto.Deleted_At != null)
        {
            return StatusCode(400,"Attenzione, non � possibile salvare ed elimnare contestualmente un prodotto ");
        }
        try
        {
            await this.context.Database.BeginTransactionAsync();
            var inserted = await this.context.Prodotti.AddAsync(prodotto);
            await this.context.SaveChangesAsync();
            await this.context.Database.CommitTransactionAsync();

            return StatusCode(200,inserted.Entity.Id);
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
                return StatusCode(200);
            }
            catch (Exception e)
            {
                await this.context.Database.RollbackTransactionAsync();
                return StatusCode(500, e.Message);
            }
        }
        else
        {
            return StatusCode(404,"Prodotto inesistente");
        }
    }
}
