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
    public class VendasController : Controller
    {
        private readonly ApplicationDbContext _context;
        Random rd = new Random();
        private List<Fornecedor> list_fornecedores = new List<Fornecedor>();
        private List<Produto> list_produtos = new List<Produto>();
        Stopwatch stopWatch = new Stopwatch();
        Log desempenho = new Log();
        Venda tipoObjeto;

        public VendasController(ApplicationDbContext context)
        {
            tipoObjeto = new Venda();
            _context = context;
            list_fornecedores = _context.Fornecedores.ToList();
            list_produtos = _context.Produtos.ToList();
        }

        [AllowAnonymous]
        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Venda.Include(v => v.Fornecedor).Include(v => v.Produto);
            return View(await applicationDbContext.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Fornecedor)
                .Include(v => v.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venda venda, int quantidade)
        {
            if (ModelState.IsValid)
            {
                VendaFaker faker = new VendaFaker();
                stopWatch.Start();
                for (int i = 1; i <= quantidade; i++)
                {
                    venda = faker.dataFake();
                    venda.Fornecedor = fornecedorRandomico();
                    venda.Produto = produtoRandomico();
                    venda.Id = Guid.NewGuid();
                    _context.Add(venda);
                    await Task.Run(() => _context.SaveChangesAsync());
                }
                stopWatch.Stop();
                desempenho.GerarLogInsert(venda, stopWatch, quantidade);
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", venda.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", venda.ProdutoId);
            return View(venda);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.FindAsync(id);
            if (venda == null)
            {
                return NotFound();
            }
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", venda.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", venda.ProdutoId);
            return View(venda);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Venda venda)
        {
            if (id != venda.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venda);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendaExists(venda.Id))
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
            ViewData["FornecedorId"] = new SelectList(_context.Fornecedores, "Id", "Nome", venda.FornecedorId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Nome", venda.ProdutoId);
            return View(venda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAll()
        {
            int count = 1;
            stopWatch.Start();
            foreach (Venda v in _context.Venda.ToList())
            {

                v.Quantidade = count;
                v.DataVenda = DateTime.Now;

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(v);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!VendaExists(v.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    count++;
                }
            }
            stopWatch.Stop();
            desempenho.GerarLogUpdate(tipoObjeto, stopWatch, _context.Venda.ToList().Count);
            return RedirectToAction(nameof(Index));
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Venda == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda
                .Include(v => v.Fornecedor)
                .Include(v => v.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venda == null)
            {
                return NotFound();
            }

            return View(venda);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Venda == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Venda'  is null.");
            }
            var venda = await _context.Venda.FindAsync(id);
            if (venda != null)
            {
                _context.Venda.Remove(venda);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            int quantidade = _context.Venda.ToList().Count;
            stopWatch.Start();
            foreach (Venda v in _context.Venda.ToList())
            {
                _context.Venda.Remove(v);
                await _context.SaveChangesAsync();
            }
            stopWatch.Stop();
            desempenho.GerarLogDelete(tipoObjeto, stopWatch, quantidade);
            return RedirectToAction(nameof(Index));
        }

        private bool VendaExists(Guid id)
        {
            return _context.Venda.Any(e => e.Id == id);
        }
        public Task<IActionResult> CreateCem()
        {
            Venda v = new Venda();
            return this.Create(v, 100);

        }
        public Task<IActionResult> CreateMil()
        {
            Venda v = new Venda();
            return this.Create(v, 1000);

        }
        public Task<IActionResult> CreateDezMil()
        {
            Venda v = new Venda();
            return this.Create(v, 10000);

        }
        public Task<IActionResult> CreateCemMil()
        {
            Venda v = new Venda();
            return this.Create(v, 100000);

        }
        public Task<IActionResult> CreateUmMilhao()
        {
            Venda v = new Venda();
            return this.Create(v, 1000000);

        }
        public Fornecedor fornecedorRandomico()
        {
            int randIndexFornecedor = rd.Next(list_fornecedores.Count);
            return list_fornecedores[randIndexFornecedor];

        }

        public Task<IActionResult> EditarTodos()
        {
            return EditAll();
        }

        public Task<IActionResult> ExcluirTodos()
        {
            return DeleteAll();
        }
        public Produto produtoRandomico()
        {
            int randIndexProduto = rd.Next(list_produtos.Count);
            return list_produtos[randIndexProduto];
        }
    }
}