# Biblioteca - README

## Visão Geral
	Este projeto é uma aplicação web desenvolvida em C#/ASP.NET Core para gerenciar um sistema de biblioteca. Ele inclui funcionalidades para adicionar, editar, excluir e visualizar livros, autores, assuntos e preços. 
	O aplicativo utiliza o Entity Framework Core para interações com um banco de dados SQL Server

## Estrutura do Projeto - Padrão MVC (Model-View-Controller)

	/Models
	  ├── Assunto.cs
	  ├── Autor.cs
	  ├── ErrorViewModel.cs
	  ├── Livros.cs
	  ├── LivroAssunto.cs
	  ├── LivroAutor.cs
	  ├── Preco.cs
	  ├── VwAutorLivros
	  ├── VwLivrosCompletos
	  
	Os modelos representam as entidades do domínio da aplicação, como `Livro`, `Autor`, `Assunto`, `LivroAutor`, `LivroAssunto`, `Preco` e `VwLivrosCompleto`. Eles são essenciais para definir a estrutura de dados e as regras de negócio da aplicação.

	- **Livro**: Representa um livro na biblioteca. Contém propriedades como `Titulo`, `DataPublicacao`, e relacionamentos com `LivroAutores`, `LivroAssuntos` e `Precos`.

	- **Autor**: Representa um autor associado a um livro. Inclui propriedades como `AutorId` e `Nome`. Possui relacionamento com a tabela de junção `LivroAutor`.

	- **Assunto**: Representa o assunto do livro. Inclui propriedades como `AssuntoId` e `Descricao`. Possui relacionamento com a tabela de junção `LivroAssunto`.

	- **LivroAutor**: Tabela de junção para representar a relação muitos-para-muitos entre `Livro` e `Autor`. Inclui `LivroId`, `AutorId` e as referências aos modelos `Livro` e `Autor`.

	- **LivroAssunto**: Tabela de junção para representar a relação muitos-para-muitos entre `Livro` e `Assunto`. Inclui `LivroId`, `AssuntoId` e as referências aos modelos `Livro` e `Assunto`.

	- **Preco**: Representa as informações de preço de um livro, incluindo `FormaCompra` e `Valor`.

	- **VwAutorLivros**: Modelo baseado em uma view no banco de dados que fornece informações completas sobre os Autores, Livros, Assunto e Data de Publicação

	- **VwLivrosCompleto**: Modelo baseado em uma view no banco de dados que fornece informações completas sobre os livros, incluindo autor, assunto, forma de compra e preço.

	/Database
	  ├── AlterTable.sql
	  ├── CreateTable.sql
	  ├── CreateView vw_AutorLivros.sql
	  ├── CreateView vw_LivorsCompletos.sql
	  ├── CreateView Relacionamentos
	  ├── InsertData.sql
	  
	O banco de dados SQL Server contém várias tabelas e views que fornecem dados para a aplicação. A seguir, as principais tabelas e views:

	- **Tabelas**:
	  - `Livros`: Armazena informações sobre os livros, incluindo título e data de publicação.
	  - `Autores`: Armazena informações sobre os autores.
	  - `Assuntos`: Armazena informações sobre os assuntos dos livros.
	  - `LivroAutor`: Tabela de junção para o relacionamento muitos-para-muitos entre livros e autores.
	  - `LivroAssunto`: Tabela de junção para o relacionamento muitos-para-muitos entre livros e assuntos.
	  - `Precos`: Armazena informações de preço e forma de compra dos livros.

	- **View**:
	  - `VwAutorLivros`: Modelo baseado em uma view no banco de dados que fornece informações completas sobre os Autores, Livros, Assunto e Data de Publicação
	  - `vw_LivrosCompleto`: Uma view no banco de dados que fornece uma lista completa de livros, incluindo autor, assunto, data de publicação, forma de compra e preço.

	/Data
	  ├── ApplicationDbContext.cs
	  
	A camada de dados (`Biblioteca.Data`) contém a configuração do contexto do banco de dados com a classe `ApplicationDbContext`, que é responsável pela comunicação com o banco de dados.

	- **ApplicationDbContext**: 
	  - Configura os modelos e relacionamentos para o Entity Framework Core.
	  - Configura as chaves compostas para as tabelas de junção (`LivroAutor` e `LivroAssunto`).
	  - Inclui as views `vw_LivrosCompleto` e `VwAutorLivros` como entidades sem chave (`Keyless`).

	/Controllers
	  ├── LivroController.cs
	  
	Os controladores são responsáveis por gerenciar as requisições do usuário, interagir com os modelos e retornar as respostas adequadas.

	- **LivroController**: 
	  - **Index**: Exibe a lista completa de livros utilizando a view `vw_LivrosCompleto`.
	  - **Create**: Exibe o formulário de criação e adiciona um novo livro, autor, assunto e preço ao banco de dados.
	  - **Edit**: Permite a edição de um livro, autor, assunto e preço. Lida com a atualização das informações no banco de dados.
	  - **Delete**: Remove um livro e suas relações com autores, assuntos e preços do banco de dados.
	  - **Details**: Exibe os detalhes do livro, incluindo autor, assunto, forma de compra e preço.
	  - **GerarRelatorioPdf**: Gera um relatório em PDF com a lista de livros por autor utilizando a view `vw_LivrosCompleto`.

	/Views/Livro
	  ├── Create..cshtmlIndex.cshtml
	  ├── Delete.cshtml
	  ├── Details..cshtml
	  ├── Edit..cshtml
	  ├── Index..cshtml
	  
	As Views definem a interface do usuário, utilizando Razor Pages para apresentar as informações e formular as interações.

	- **Index**: Exibe uma tabela com a lista de livros, incluindo opções para editar, excluir e gerar relatórios.
	- **Create**: Formulário para adicionar um novo livro, autor, assunto e preço.
	- **Edit**: Formulário para editar as informações de um livro, incluindo autor, assunto e preço.
	- **Delete**: Exibe os detalhes do livro a ser excluído, confirmando a remoção.
	- **Details**: Mostra os detalhes de um livro específico, sem opção de edição.
	- **Relatório**: Implementado com iTextSharp para gerar relatórios em PDF com os dados da view `vw_LivrosCompleto`.

	/Views/Shared
	  ├── _layout.cshtml
	  ├── _layout.cshtml.css
	  ├── _validationScriptsPartial.cshtml
	  ├── Error.cshtml

	- **_layout.cshtml** e **_layout.cshtml.css**: Páginas com o modelo HTML e CSS partilhado entre todas as telas

	/wwwroot/css
	  ├── Site.css
	  
	O projeto inclui um arquivo CSS que define estilos personalizados, como a alternância de cores nas linhas da tabela e a cor de fundo da barra de navegação.

### Como Executar o Projeto
	1. Certifique-se de ter o .NET SDK e o SQL Server e SQL SMS 20.2 instalados.
	2. Restaure os pacotes NuGet: `dotnet restore`.
	3. Configure a string de conexão com o banco de dados no arquivo `appsettings.json`.
	4. Atualize o banco de dados com as migrações existentes: `dotnet ef database update`.
	5. Execute o projeto: `dotnet run`.
	6. Acesse o aplicativo no navegador em `http://localhost:5130`.

### Considerações Finais
	Este projeto foi desenvolvido utilizando boas práticas do desenvolvimento em C#/ASP.NET Core e Entity Framework Core. A aplicação inclui funcionalidades essenciais para o gerenciamento de uma biblioteca, como CRUD (Create, Read, Update, Delete) para livros, autores, assuntos e preços, além de recursos de geração de relatórios.

