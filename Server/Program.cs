using ProgettoIDS.Midlleware;

var builder = WebApplication.CreateBuilder(args);

//Qui definisco il DatabaseContext globale, da poter iniettare per DependencyInjection

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite($"Data Source={builder.Environment.ContentRootPath}\\IDS_FabioBevilacqua.db"));

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
}) ;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    dataContext.Database.Migrate();
}

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();


app.Run();
