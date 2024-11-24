using CountryLocalDbWork;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

// 1.GET /api
app.MapGet("/api", () => new { Message = "Server is running!" });
// 2.GET/api/ping
app.MapGet("/api/ping", () => new { Message = "pong!" });

// 3.GET /api/country
app.MapGet("/api/country", async (ApplicationDbContext db) =>
{
    return await db.Countries.ToListAsync();
});

// 4.GET /api/country/{id}
app.MapGet("/api/country/{id:int}", async (int id, ApplicationDbContext db) =>
{
    return await db.Countries.FirstOrDefaultAsync(d => d.Id == id);
});

// 6.POST /api/country
app.MapPost("/api/country", async (Country country, ApplicationDbContext db) =>
{
    await db.Countries.AddAsync(country);
    await db.SaveChangesAsync();
    return country;
});

// 7.Delete /api/country/{id}
app.MapDelete("/api/country/{id:int}", async (int id, ApplicationDbContext db) =>
{
    Country? deleted = await db.Countries.FirstOrDefaultAsync(d => d.Id == id);
    if (deleted != null)
    {
        db.Countries.Remove(deleted);
        await db.SaveChangesAsync();
    }
});
//rename country
app.MapPut("/api/country/{id:int}", async (int id, Country country, ApplicationDbContext db) =>
{
    Country? update = await db.Countries.FirstOrDefaultAsync(c => c.Id == id);
    if (update != null)
    {
        update.fullName = country.fullName;
        update.shortName = country.shortName;
        update.alpha2Code = country.alpha2Code;

        db.Countries.Update(update);

        await db.SaveChangesAsync();
    }
    return update;
});

//arr post
app.MapPost("/api/country/list", async (List<Country> list, ApplicationDbContext db) =>
{
    await db.Countries.AddRangeAsync(list);
    await db.SaveChangesAsync();
    return list;
});

//clear
app.MapDelete("/api/country/clear", async (ApplicationDbContext db) =>
{
    List<Country> all = await db.Countries.ToListAsync();
    db.Countries.RemoveRange(all);
    await db.SaveChangesAsync();
});


app.Run();
