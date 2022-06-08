using ProgettoIDS.Midlleware;

var builder = WebApplication.CreateBuilder(args);

//Qui definisco il DatabaseContext globale, da poter iniettare per DependencyInjection

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite($"Data Source={builder.Environment.ContentRootPath}\\IDS_FabioBevilacqua.db"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


app.UseMiddleware(typeof(ExceptionHandlingMiddleware));
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();


app.Run();
