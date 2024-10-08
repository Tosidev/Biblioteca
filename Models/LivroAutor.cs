namespace Biblioteca.Models
{
    public class LivroAutor
    {
        // Chave estrangeira para Livro
        public int LivroId { get; set; }
        public Livro Livro { get; set; } = null!; 

        // Chave estrangeira para Autor
        public int AutorId { get; set; }
        public Autor Autor { get; set; } = null!; 
    }
}
