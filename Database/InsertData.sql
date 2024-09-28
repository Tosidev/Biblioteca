/*
Script SQL para inserir livros com autores em suas respectivas tabelas, 
considerando os relacionamentos de "Livro", "Autor", "LivroAutor", "Assunto", e "LivroAssunto"

IDs criados "automaticamente" para efeitos de relacionamentos entre todas as tabelas
*/

-- Inserir autores
INSERT INTO Autores (Nome) VALUES ('J.K. Rowling'); -- ID 1
INSERT INTO Autores (Nome) VALUES ('George R.R. Martin'); -- ID 2
INSERT INTO Autores (Nome) VALUES ('J.R.R. Tolkien'); -- ID 3

-- Inserir assuntos
INSERT INTO Assuntos (Descricao) VALUES ('Fantasia'); -- ID 1
INSERT INTO Assuntos (Descricao) VALUES ('Aventura'); -- ID 2
INSERT INTO Assuntos (Descricao) VALUES ('Mitologia'); -- ID 3

-- Inserir livros
INSERT INTO Livros (Titulo, DataPublicacao) VALUES ('Harry Potter e a Pedra Filosofal', '1997-06-26'); -- ID 1
INSERT INTO Livros (Titulo, DataPublicacao) VALUES ('Game of Thrones', '1996-08-06'); -- ID 2
INSERT INTO Livros (Titulo, DataPublicacao) VALUES ('O Hobbit', '1937-09-21'); -- ID 3
INSERT INTO Livros (Titulo, DataPublicacao) VALUES ('O Senhor dos Anéis: A Sociedade do Anel', '1954-07-29'); -- ID 4
INSERT INTO Livros (Titulo, DataPublicacao) VALUES ('Harry Potter e a Câmara Secreta', '1998-07-02'); -- ID 5

-- Relacionar livros com autores
-- Livro 1: 'Harry Potter e a Pedra Filosofal' com 1 autor (J.K. Rowling)
INSERT INTO LivroAutor (LivroId, AutorId) VALUES (1, 1);

-- Livro 2: 'Game of Thrones' com 2 autores (George R.R. Martin e J.K. Rowling)
INSERT INTO LivroAutor (LivroId, AutorId) VALUES (2, 2);
INSERT INTO LivroAutor (LivroId, AutorId) VALUES (2, 1);

-- Livro 3: 'O Hobbit' com 1 autor (J.R.R. Tolkien)
INSERT INTO LivroAutor (LivroId, AutorId) VALUES (3, 3);

-- Livro 4: 'O Senhor dos Anéis: A Sociedade do Anel' com 2 autores (J.R.R. Tolkien e George R.R. Martin)
INSERT INTO LivroAutor (LivroId, AutorId) VALUES (4, 3);
INSERT INTO LivroAutor (LivroId, AutorId) VALUES (4, 2);

-- Livro 5: 'Harry Potter e a Câmara Secreta' com 1 autor (J.K. Rowling)
INSERT INTO LivroAutor (LivroId, AutorId) VALUES (5, 1);

-- Relacionar livros com assuntos
-- Livro 1: 'Harry Potter e a Pedra Filosofal' com 'Fantasia'
INSERT INTO LivroAssunto (LivroId, AssuntoId) VALUES (1, 1);

-- Livro 2: 'Game of Thrones' com 'Aventura'
INSERT INTO LivroAssunto (LivroId, AssuntoId) VALUES (2, 2);

-- Livro 3: 'O Hobbit' com 'Mitologia'
INSERT INTO LivroAssunto (LivroId, AssuntoId) VALUES (3, 3);

-- Livro 4: 'O Senhor dos Anéis: A Sociedade do Anel' com 'Aventura' e 'Mitologia'
INSERT INTO LivroAssunto (LivroId, AssuntoId) VALUES (4, 2);
INSERT INTO LivroAssunto (LivroId, AssuntoId) VALUES (4, 3);

-- Livro 5: 'Harry Potter e a Câmara Secreta' com 'Fantasia'
INSERT INTO LivroAssunto (LivroId, AssuntoId) VALUES (5, 1);

