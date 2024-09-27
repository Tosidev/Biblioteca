using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Data;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Threading.Tasks;

namespace Biblioteca.Controllers
{
    public class LivroController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Injeção de dependência do ApplicationDbContext
        public LivroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Livro
        public async Task<IActionResult> Index()
        {
            var livrosCompletos = await _context.VwLivrosCompleto.ToListAsync();

            if (livrosCompletos.Count() == 0)
            {
                return NotFound("Nenhum registro encontrado.");
            }

            // Retorna a view com todos os dados
            return View(livrosCompletos);
        }

        // GET: Livro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Livro livro, string NovoAutor, string NovoAssunto, string FormaCompra, decimal Preco)
        {
            if (ModelState.IsValid)
            {
                // Adicionar o novo autor
                var autor = new Autor
                {
                    Nome = NovoAutor,
                    LivrosAutores = new List<LivroAutor>()
                };
                _context.Autores.Add(autor);
                await _context.SaveChangesAsync();

                // Adicionar o novo assunto
                var assunto = new Assunto
                {
                    Descricao = NovoAssunto,
                    LivroAssuntos = new List<LivroAssunto>()
                };
                _context.Assuntos.Add(assunto);
                await _context.SaveChangesAsync();

                // Relacionar o autor e o assunto com o livro
                livro.LivroAutores = new List<LivroAutor>
                {
                    new LivroAutor
                    {
                        Autor = autor,
                        Livro = livro
                    }
                };

                livro.LivroAssuntos = new List<LivroAssunto>
                {
                    new LivroAssunto
                    {
                        Assunto = assunto,
                        Livro = livro
                    }
                };

                // Adicionar informações de Preco ao livro
                livro.Precos.Add(new Preco
                {
                    FormaCompra = FormaCompra,
                    Valor = Preco,
                    Livro = livro // Associar o livro ao preço
                });

                // Salvar o livro
                _context.Add(livro);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(livro);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Carregar o livro com todas as informações relacionadas, incluindo os preços
            var livro = await _context.Livros
                .Include(l => l.Precos) 
                .Include(l => l.LivroAutores)
                    .ThenInclude(la => la.Autor)
                .Include(l => l.LivroAssuntos)
                    .ThenInclude(la => la.Assunto)            
                .FirstOrDefaultAsync(l => l.LivroId == id);

            if (livro == null)
            {
                return NotFound();
            }

            // Verificar se há preços e definir os valores em ViewData
            ViewData["FormaCompra"] = livro.Precos.FirstOrDefault()?.FormaCompra;
            ViewData["Preco"] = livro.Precos.FirstOrDefault()?.Valor;

            return View(livro);
        }

        // POST: Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Livro livro, string AutorNome, string AssuntoDescricao, string FormaCompra, string Preco)
        {
            if (id != livro.LivroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Este try-catch utilizado para capturar erros específicos durante a atualização do banco de dados e lidar com possíveis problemas de concorrência
                try 
                {
                    // Atualizar o preço e forma de compra
                    var precoExistente = _context.Precos.FirstOrDefault(p => p.LivroId == livro.LivroId);
                    if (precoExistente != null)
                    {
                        precoExistente.FormaCompra = FormaCompra;
                        precoExistente.Valor = decimal.Parse(Preco); // Converter o valor do campo para decimal
                    }
                    else
                    {
                        livro.Precos = new List<Preco>
                        {
                            new Preco
                            {
                                LivroId = livro.LivroId,
                                FormaCompra = FormaCompra,
                                Valor = decimal.Parse(Preco) // Converter o valor do campo para decimal
                            }
                        };
                    }

                    // Atualizar o assunto
                    var assuntoExistente = _context.LivroAssunto.FirstOrDefault(a => a.LivroId == livro.LivroId);
                    if (assuntoExistente != null)
                    {
                        // Atualizar a descrição do assunto existente
                        var assunto = _context.Assuntos.FirstOrDefault(a => a.AssuntoId == assuntoExistente.AssuntoId);
                        if (assunto != null)
                        {
                            assunto.Descricao = AssuntoDescricao;
                        }
                    }
                    else
                    {
                        // Criar um novo assunto e relacionar com o livro
                        var novoAssunto = new Assunto { Descricao = AssuntoDescricao };
                        _context.Assuntos.Add(novoAssunto);
                        await _context.SaveChangesAsync(); // Salvar as informações para obter o ID do novo assunto

                        livro.LivroAssuntos = new List<LivroAssunto>
                        {
                            new LivroAssunto
                            {
                                LivroId = livro.LivroId,
                                AssuntoId = novoAssunto.AssuntoId
                            }
                        };
                    }

                    // Atualizar o autor
                    var autorExistente = _context.LivroAutor.FirstOrDefault(a => a.LivroId == livro.LivroId);
                    if (autorExistente != null)
                    {
                        // Atualizar o nome do autor existente
                        var autor = _context.Autores.FirstOrDefault(a => a.AutorId == autorExistente.AutorId);
                        if (autor != null)
                        {
                            autor.Nome = AutorNome;
                        }
                    }
                    else
                    {
                        // Criar um novo autor e relacionar com o livro
                        var novoAutor = new Autor { Nome = AutorNome };
                        _context.Autores.Add(novoAutor);
                        await _context.SaveChangesAsync(); // Salvar as informações para obter o ID do novo autor

                        livro.LivroAutores = new List<LivroAutor>
                        {
                            new LivroAutor
                            {
                                LivroId = livro.LivroId,
                                AutorId = novoAutor.AutorId
                            }
                        };
                    }

                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Livros.Any(e => e.LivroId == livro.LivroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw; // Utilizado para relançar a exceção se não for uma situação de ausência do registro. (Situção crítica durante o desenvolvimento)
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            // Manter o valor do preço no ViewData em caso de erro
            ViewData["Preco"] = Preco;
            ViewData["PrecoError"] = "Por favor, insira um valor válido.";
            return View(livro);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var livro = await _context.Livros
                .Include(l => l.LivroAutores)
                .ThenInclude(la => la.Autor)
                .Include(l => l.LivroAssuntos)
                .ThenInclude(la => la.Assunto)
                .Include(l => l.Precos)
                .FirstOrDefaultAsync(l => l.LivroId == id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros
                .Include(l => l.LivroAutores)
                .Include(l => l.LivroAssuntos)
                .Include(l => l.Precos)
                .FirstOrDefaultAsync(l => l.LivroId == id);

            if (livro == null)
            {
                return NotFound();
            }

            // Remover os registros relacionados primeiro
            _context.LivroAutor.RemoveRange(livro.LivroAutores);
            _context.LivroAssunto.RemoveRange(livro.LivroAssuntos);
            _context.Precos.RemoveRange(livro.Precos);

            // Remover o livro
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
                .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
                .Include(l => l.Precos)
                .FirstOrDefaultAsync(m => m.LivroId == id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // Método para gerar o relatório em PDF
        public async Task<IActionResult> GerarRelatorioPdf()
        {
                // Definir o caminho para salvar o arquivo PDF no desktop com um identificador exclusivo
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string pdfPath = Path.Combine(desktopPath, $"RelatorioAutorLivros_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"); // (Desenvolvimento crítico, cria o pdf mas dá erro)

                // Este try-catch utilizado para capturar capturar quaisquer exceções que possam ocorrer durante o processo de geração do PDF e retornar uma mensagem de erro amigável caso algo dê errado
                try
                {
                    // Criar o PDF usando iTextSharp
                    using (var writer = new PdfWriter(pdfPath))
                    {
                        using (var pdfDoc = new PdfDocument(writer))
                        {
                            Document document = new Document(pdfDoc);
                            
                            // Adicionar título ao PDF
                            document.Add(new Paragraph("Relatório de Livros por Autor"));

                            // Buscar os dados do banco
                            var livros = await _context.VwAutorLivros.ToListAsync();

                            // Adicionar os dados ao PDF
                            foreach (var livro in livros)
                            {                    
                                document.Add(new Paragraph($"Autor: {livro.Autor}"));
                                document.Add(new Paragraph($"Título: {livro.Titulo}"));
                                document.Add(new Paragraph($"Assunto: {livro.Assunto}"));
                            }

                            document.Close();
                        }
                    }

                    // Ler o arquivo PDF e retornar o conteúdo como uma resposta
                    var fileBytes = System.IO.File.ReadAllBytes(pdfPath);

                    // Remover o arquivo temporário após ler o conteúdo
                    System.IO.File.Delete(pdfPath);

                    return File(fileBytes, "application/pdf", "RelatorioAutorLivros.pdf");
                }
                catch (Exception ex)
                {
                    // Tratar erros de geração de PDF
                    return Content($"Erro ao gerar o PDF: {ex.Message}");
                }
            }
        }

}
