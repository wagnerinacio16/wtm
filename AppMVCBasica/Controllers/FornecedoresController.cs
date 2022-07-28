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

namespace AppMVCBasica.Controllers
{
    [Authorize]
    public class FornecedoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FornecedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: Fornecedores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fornecedores.ToListAsync());
        }

        [AllowAnonymous]
        // GET: Fornecedores/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Fornecedores == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            return View();
        } // GET: Fornecedores/Create


        // POST: Fornecedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create( Fornecedor fornecedor)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        fornecedor.Id = Guid.NewGuid();
        //        _context.Add(fornecedor);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(fornecedor);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fornecedor fornecedor, int quantidade)
        {
            FornecedorFaker fake = new FornecedorFaker();
            if (ModelState.IsValid)
            {
                for (int i = 1; i <= quantidade; i++)
                {
                    fornecedor.Id = Guid.NewGuid();
                    fornecedor = fake.dataFake();
                    _context.Add(fornecedor);
                }
                await _context.SaveChangesAsync();
                return RedirectToActionPermanent(nameof(Index));
            }
            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Fornecedores == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fornecedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
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
            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Fornecedores == null)
            {
                return NotFound();
            }

            var fornecedor = await _context.Fornecedores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fornecedor == null)
            {
                return NotFound();
            }

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Fornecedores == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Fornecedores'  is null.");
            }
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor != null)
            {
                _context.Fornecedores.Remove(fornecedor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(Guid id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
        public Task<IActionResult> CreateCem()
        {
            Fornecedor f = new Fornecedor();
            return this.Create(f, 100);

        }
        public Task<IActionResult> CreateMil()
        {
            Fornecedor f = new Fornecedor();
            return this.Create(f, 1000);

        }
        public Task<IActionResult> CreateDezMil()
        {
            Fornecedor f = new Fornecedor();
            return this.Create(f, 10000);

        }
        public Task<IActionResult> CreateCemMil()
        {
            Fornecedor f = new Fornecedor();
            return this.Create(f, 100000);

        }
        public Task<IActionResult> CreateUmMilhao()
        {
            Fornecedor f = new Fornecedor();
            return this.Create(f, 1000000);

        }
    }
}
