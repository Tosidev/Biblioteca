-- Criação da view vw_LivrosCompleto com os dados completos
CREATE VIEW vw_LivrosCompleto AS
SELECT 
    l.LivroId,
	l.Titulo AS Título,
    a.Nome AS Autor,
    s.Descricao AS Assunto,
    CONVERT(VARCHAR(10), l.DataPublicacao, 120) AS Publicacao,  -- Formatar a data para YYYY-MM-DD
    p.FormaCompra AS FormaCompra,
    p.Valor AS Preco
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
left JOIN 
    Precos p ON l.LivroId = p.LivroId
GROUP BY 
    l.Titulo, a.Nome, l.DataPublicacao, s.Descricao, p.FormaCompra, p.Valor;

