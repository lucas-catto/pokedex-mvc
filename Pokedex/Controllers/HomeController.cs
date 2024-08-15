using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Models;
using Pokedex.ViewModels;

namespace Pokedex.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(
        ILogger<HomeController> logger,
        AppDbContext context
    ) {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        HomeVM home = new()
        {
            Tipos = _context.Tipos.ToList(),

            Pokemons = _context.Pokemons
                .Include(p => p.Tipos)
                .ThenInclude(t => t.Tipo)
                .Include(p => p.Regiao)
                .Include(p => p.Genero)
                .ToList()
        };
        
        return View(home);
    }

    public IActionResult Details(int id)
    {
        Pokemon pokemon = _context.Pokemons
            .Where(p => p.Numero == id)
            .Include(p => p.Tipos)
            .ThenInclude(t => t.Tipo)
            .Include(p => p.Regiao)
            .Include(p => p.Genero)
            .SingleOrDefault();

        DetailVM detailsvm = new()
        {
            Anterior = _context.Pokemons
                .OrderByDescending(p => p.Numero)
                .FirstOrDefault(p => p.Numero < id),
            
            Atual = pokemon,

            Proximo = _context.Pokemons
                .OrderBy(p => p.Numero)
                .FirstOrDefault(p => p.Numero > id)
        };

        return View(pokemon);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
