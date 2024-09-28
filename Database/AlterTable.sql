-- Desabilitar restrições de chaves estrangeiras temporariamente
ALTER TABLE LivroAutor NOCHECK CONSTRAINT ALL;
ALTER TABLE LivroAssunto NOCHECK CONSTRAINT ALL;
ALTER TABLE Precos NOCHECK CONSTRAINT ALL;

-- Apagar os dados das tabelas
DELETE FROM LivroAutor;
DELETE FROM LivroAssunto;
DELETE FROM Precos;
DELETE FROM Livros;
DELETE FROM Autores;
DELETE FROM Assuntos;

-- Reiniciar os valores de identidade (auto-incremento)
DBCC CHECKIDENT ('LivroAutor', RESEED, 0);
DBCC CHECKIDENT ('LivroAssunto', RESEED, 0);
DBCC CHECKIDENT ('Precos', RESEED, 0);
DBCC CHECKIDENT ('Livros', RESEED, 0);
DBCC CHECKIDENT ('Autores', RESEED, 0);
DBCC CHECKIDENT ('Assuntos', RESEED, 0);

-- Habilitar restrições de chaves estrangeiras
ALTER TABLE LivroAutor CHECK CONSTRAINT ALL;
ALTER TABLE LivroAssunto CHECK CONSTRAINT ALL;
ALTER TABLE Precos CHECK CONSTRAINT ALL;
