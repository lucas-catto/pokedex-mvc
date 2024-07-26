
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EtecBarraBonita.Models;

public class Curso
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Nome { get; set; }

    [Required]
    public int EixoTecnologicoId { get; set; }
    [ForeignKey("EixoTecnologicoId")]
    public EixoTecnologico EixoTecnologico { get; set; }
}
