using GestionVoitureFrontOffice.Configurations;
using GestionVoitureFrontOffice.Models;
using GestionVoitureFrontOffice.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Net.Security;
using System.Net;
using Microsoft.AspNetCore.Identity;
using GestionVoitureFrontOffice.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

ServicePointManager.ServerCertificateValidationCallback =
    (sender, certificate, chain, sslPolicyErrors) =>
    {
        Console.WriteLine($"SSL Policy Errors: {sslPolicyErrors}");
        if (chain != null)
        {
            foreach (var status in chain.ChainStatus)
            {
                Console.WriteLine($"Chain Status: {status.StatusInformation}");
            }
        }
        return true; // Toujours retourner true pour ignorer les erreurs
    };


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

// Configuration du secret pour les JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "VotreSecretSuperSecurise";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "VotreApplication";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddRazorPages();
// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddMvcOptions(options =>
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    });
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddScoped<TragerService>();
builder.Services.AddScoped<OffreService>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<AuthorizeFilter>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddScoped<IMissionService, MissionService>();
builder.Services.AddScoped<ITragerVehiculeService, TragerVehiculeService>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "API Gestion Voiture Front Office",
        Description = "Documentation de l'API"
    });
});
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de la session
    options.Cookie.HttpOnly = true; // Protégez le cookie
    options.Cookie.IsEssential = true; // Obligatoire pour la conformité RGPD
});


var app = builder.Build();

app.UseSession();
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    //app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseStatusCodePagesWithReExecute("/Home/NotFound");
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Use(async (context, next) =>
{
    //Console.WriteLine($"Request URL: {context.Request.Path}");
    await next();
});
app.MapRazorPages();

app.Run();
