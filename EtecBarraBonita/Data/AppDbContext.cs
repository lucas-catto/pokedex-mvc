
using EtecBarraBonita.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EtecBarraBonita.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Curso> Cursos { get; set; }
    public DbSet<EixoTecnologico> EixoTecnologicos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}
