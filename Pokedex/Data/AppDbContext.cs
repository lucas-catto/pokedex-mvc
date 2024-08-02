
using System.Formats.Tar;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

namespace Pokedex.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public DbSet<Genero> Generos { get; set; }
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonTipo> PokemonTipos { get; set; }
    public DbSet<Regiao> Regioes { get; set; }
    public DbSet<Tipo> Tipos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Muitos para Muitos do Pokemon Tipo
        // pt => PokemonTipo
            
        builder.Entity<PokemonTipo>()
            .HasKey(
                pt => new {
                    pt.PokemonNumero,
                    pt.TipoId
                }
            );

        /* Um para muitos e a chave estrangeira =>
         * .HasOne
         * .WithMany
         * .HasForeignKey
         */

        // Relacionar o PokemonTipo com Pokemon
        builder.Entity<PokemonTipo>()
            .HasOne(pt => pt.Pokemon)
            .WithMany(p => p.Tipos)
            .HasForeignKey(pt => pt.PokemonNumero);

        builder.Entity<PokemonTipo>()
            .HasOne(pt => pt.Tipo)
            .WithMany(t => t.Pokemons)
            .HasForeignKey(pt => pt.TipoId);

        #endregion

        #region Populate Roles - Perfis de Usuário

        List<IdentityRole> roles = new()
        {
            new IdentityRole()
            {
               Id             = Guid.NewGuid().ToString(),
               Name           = "Administrador",
               NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole()
            {
               Id             = Guid.NewGuid().ToString(),
               Name           = "Usuário",
               NormalizedName = "USUÁRIO"
            }
        };
        
        builder.Entity<IdentityRole>()
            .HasData(roles);

        #endregion

        #region Populate IdentityUser

        List<IdentityUser> users = new(){
            new IdentityUser(){
                Id                 = Guid.NewGuid().ToString(),
                Email              = "admin@pokedex.com",
                NormalizedEmail    = "ADMIN@POKEDEX.COM",
                UserName           = "Admin",
                NormalizedUserName = "ADMIN",
                LockoutEnabled = false,
                EmailConfirmed = true,
            }
        };
        
        foreach (var user in users)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "@Etec123");
        }

        builder.Entity<IdentityUser>()
            .HasData(users);

        List<Usuario> usuarios = new()
        {
            new Usuario(){
                UsuarioId      = users[0].Id,
                Nome           = "José Antonio Gallo Junior",
                DataNascimento = DateTime.Parse("05/08/1981"),
                Foto           = "/img/users/avatar.png"
            }
        };

        builder.Entity<Usuario>()
            .HasData(usuarios);

        #endregion

        #region Populate UserRole - Usuário com Perfil

        List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>() {
                UserId = users[0].Id,
                RoleId = roles[0].Id
            },
            new IdentityUserRole<string>() {
                UserId = users[0].Id,
                RoleId = roles[1].Id
            }
        };

        builder.Entity<IdentityUserRole<string>>()
            .HasData(userRoles);

        #endregion
    }
}
