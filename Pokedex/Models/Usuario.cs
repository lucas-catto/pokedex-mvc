using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Pokedex.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    public string UsuarioId { get; set; }
    [ForeignKey("UsuarioId")]
    public IdentityUser ContaUsuario { get; set; }

    [Required]
    [StringLength(60)]
    public string Nome { get; set; }

    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }
    
    [StringLength(200)]
    public string Foto { get; set; }
}
