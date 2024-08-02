using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pokedex.Models;

[Table("Pokemon")]
public class Pokemon
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Numero { get; set; }

    [Required]
    public int RegiaoId { get; set; }
    [ForeignKey("RegiaoId")]
    public Regiao Regiao { get; set; }

    [Required]
    public int GeneroId { get; set; }
    [ForeignKey("GeneroId")]
    public Genero Genero { get; set; }

    [Required]
    [StringLength(30)]
    public string Nome { get; set; }

    [StringLength(1000)]
    public string Descricao { get; set; }

    [Required]
    [Column(TypeName = "double(5,2)")]
    public double Altura { get; set; }

    [Required]
    [Column(TypeName = "double(7,3)")]
    public double Peso { get; set; }

    [StringLength(200)]
    public string Imagem { get; set; }

    [StringLength(400)]
    public string Animacao { get; set; }

    public ICollection<PokemonTipo> Tipos { get; set; }
}
