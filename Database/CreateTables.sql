-- Criação das tabelas principais
CREATE TABLE Autores (
    AutorId INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(40) NOT NULL
);

CREATE TABLE Assuntos (
    AssuntoId INT PRIMARY KEY IDENTITY(1,1),
    Descricao NVARCHAR(20) NOT NULL
);

CREATE TABLE Livros (
    LivroId INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(40) NOT NULL,
    DataPublicacao DATETIME
);

CREATE TABLE Precos (
    PrecoId INT PRIMARY KEY IDENTITY(1,1),
    LivroId INT NOT NULL,
    FormaCompra NVARCHAR(50) NOT NULL,
    Valor DECIMAL(19, 9) NOT NULL,
    CONSTRAINT FK_Preco_Livro FOREIGN KEY (LivroId) REFERENCES Livros(LivroId)
);

-- Tabela de relacionamento N:N (muitos-para-muitos) entre Livros e Autores
CREATE TABLE LivroAutor (
    LivroId INT NOT NULL,
    AutorId INT NOT NULL,
    PRIMARY KEY (LivroId, AutorId),
    CONSTRAINT FK_LivroAutor_Livro FOREIGN KEY (LivroId) REFERENCES Livros(LivroId),
    CONSTRAINT FK_LivroAutor_Autor FOREIGN KEY (AutorId) REFERENCES Autores(AutorId)
);

-- Tabela de relacionamento N:N (muitos-para-muitos) entre Livros e Assuntos
CREATE TABLE LivroAssunto (
    LivroId INT NOT NULL,
    AssuntoId INT NOT NULL,
    PRIMARY KEY (LivroId, AssuntoId),
    CONSTRAINT FK_LivroAssunto_Livro FOREIGN KEY (LivroId) REFERENCES Livros(LivroId),
    CONSTRAINT FK_LivroAssunto_Assunto FOREIGN KEY (AssuntoId) REFERENCES Assuntos(AssuntoId)
);


