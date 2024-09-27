namespace Biblioteca.Models
{
    public class LivroAssunto
    {
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!; 

        public int AssuntoId { get; set; }
        public Assunto Assunto { get; set; } = null!; 
    }
}
