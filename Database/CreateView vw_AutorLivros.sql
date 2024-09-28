CREATE VIEW vw_AutorLivros AS
SELECT 
    a.Nome AS Autor,
	l.Titulo AS Título,
    s.Descricao AS Assunto
FROM 
    Livros l
left JOIN 
    LivroAutor la ON la.LivroId = l.LivroId
left JOIN 
    Autores a ON la.AutorId = a.AutorId
left JOIN 
    LivroAssunto la2 ON l.LivroId = la2.LivroId
left JOIN 
    Assuntos s ON la2.AssuntoId = s.AssuntoId
GROUP BY 
    a.Nome, l.Titulo, s.Descricao