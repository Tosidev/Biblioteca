using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Models
{
    public class Assunto
    {
        [Key]
        public int AssuntoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Descricao { get; set; } = string.Empty;

        // Inicializar a coleção para evitar problemas de referência nula
        public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
    }
}
