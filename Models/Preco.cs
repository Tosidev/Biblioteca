using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models
{
    public class Preco
    {
        [Key]
        public int PrecoId { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Valor { get; set; }

        // Relacionamento com Livro N:N (muitos-para-muitos)
        [Required]
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!; 

        // Propriedade para forma de compra (internet, balcão, etc.)
        [Required]
        [StringLength(100)]
        public string FormaCompra { get; set; } = string.Empty; 

    }
}
