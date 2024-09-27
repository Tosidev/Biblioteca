namespace Biblioteca.Models
{
    public class LivroAssunto
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!;  // Marcado como não nulo

        public int AssuntoId { get; set; }
        public Assunto Assunto { get; set; } = null!;  // Marcado como não nulo
    }
}
