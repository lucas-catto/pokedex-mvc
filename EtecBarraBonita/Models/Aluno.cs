

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EtecBarraBonita.Models;

[Table("Aluno")]
public class Aluno
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(11)]
    public string RM { get; set; }

    [Required]
    [StringLength(60)]
    public string Nome { get; set; }

    [Required]
    public string ContaUsuarioId { get; set; }
    [ForeignKey("ContaUsuarioId")]
    public IdentityUser ContaUsuario { get; set; }
}
