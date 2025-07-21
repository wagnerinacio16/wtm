using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppMVCBasica.Data;
using AppMVCBasica.Models;
using Microsoft.AspNetCore.Authorization;
using AppMVCBasica.Faker;
using System.Diagnostics;
using wtm.Data;

namespace AppMVCBasica.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        Random rd = new Random();
        private List<Fornecedor> list_fornecedores = new List<Fornecedor>();
        ProdutoFaker faker = new ProdutoFaker();
        Stopwatch stopWacth = new Stopwatch();
        Log desempenho = new Log();
        Produto tipoObjeto;


        public ProdutosController(ApplicationDbContext context)
        {
            tipoObjeto = new Produto();
            _context = context;
            list_fornecedores = _context.Fornecedores.ToList();

        }

        [AllowAnonymous]
        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Produtos.Include(p => p.Fornecedor);
            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto, int quantidade)
        {
            ProdutoFaker fake = new ProdutoFaker();
            if (ModelState.IsValid)
            {
                stopWacth.Start();
                for (int i = 1; i <= quantidade; i++)
                {
                    produto = fake.dataFake();
                    produto.Fornecedor = fornecedorRandomico();
                    _context.Add(produto);
                    await Task.Run(() => _context.SaveChangesAsync());
                }
                stopWacth.Stop();
                desempenho.GerarLogInsert(produto, stopWacth, quantidade);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", produto.FornecedorId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Documento", produto.FornecedorId);
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAll()
        {
            decimal count = 1;
            stopWacth.Start();
            foreach (Produto p in _context.Produtos.ToList())
            {
                p.Nome = faker.UpdateProduto().First();
                p.Valor = count;
                p.Descricao = faker.UpdateProduto().Last();
                p.DataCadastro = DateTime.Now;

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(p);
                        await Task.Run(() => _context.SaveChangesAsync());
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdutoExists(p.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                count++;
            }
            stopWacth.Stop();
            desempenho.GerarLogUpdate(tipoObjeto, stopWacth, _context.Produtos.ToList().Count);
            return RedirectToAction(nameof(Index));
        }


        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Produtos == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Produtos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Produtos'  is null.");
            }
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            int quantidade = _context.Produtos.ToList().Count;
            stopWacth.Start();
            foreach (Produto f in _context.Produtos.ToList())
            {
                _context.Produtos.Remove(f);
                await Task.Run(() => _context.SaveChangesAsync());

            }
            stopWacth.Stop();
            desempenho.GerarLogDelete(tipoObjeto, stopWacth, quantidade);
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
        public Task<IActionResult> CreateCem()
        {
            Produto p = new Produto();
            return this.Create(p, 100);

        }
        public Task<IActionResult> CreateMil()
        {
            Produto p = new Produto();
            return this.Create(p, 1000);

        }
        public Task<IActionResult> CreateDezMil()
        {
            Produto p = new Produto();
            return this.Create(p, 10000);

        }
        public Task<IActionResult> CreateCemMil()
        {
            Produto p = new Produto();
            return this.Create(p, 100000);

        }
        public Task<IActionResult> CreateUmMilhao()
        {
            Produto p = new Produto();
            return this.Create(p, 1000000);

        }

        public Task<IActionResult> EditarTodos()
        {
            return this.EditAll();

        }
        public Task<IActionResult> ExcluirTodos()
        {
            return this.DeleteAll();

        }

        public Fornecedor fornecedorRandomico()
        {
            int randIndex = rd.Next(list_fornecedores.Count);
            return list_fornecedores[randIndex];
        }

    }
}

