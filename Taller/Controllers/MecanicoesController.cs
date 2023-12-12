using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Taller.Models;

namespace Taller.Controllers
{
    public class MecanicoesController : Controller
    {
        private readonly Context _context;

        public MecanicoesController(Context context)
        {
            _context = context;
        }

        // GET: Mecanicoes
        public async Task<IActionResult> Index()
        {
              return _context.Mecanicos != null ? 
                          View(await _context.Mecanicos.ToListAsync()) :
                          Problem("Entity set 'Context.Mecanicos'  is null.");
        }

        // GET: Mecanicoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mecanicos == null)
            {
                return NotFound();
            }

            var mecanico = await _context.Mecanicos
                .FirstOrDefaultAsync(m => m.MecanicoID == id);
            if (mecanico == null)
            {
                return NotFound();
            }

            return View(mecanico);
        }

        // GET: Mecanicoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mecanicoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MecanicoID,Nombre,Apellido,UsuarioMecanico,ContrasenaMecanico")] Mecanico mecanico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mecanico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mecanico);
        }

        // GET: Mecanicoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mecanicos == null)
            {
                return NotFound();
            }

            var mecanico = await _context.Mecanicos.FindAsync(id);
            if (mecanico == null)
            {
                return NotFound();
            }
            return View(mecanico);
        }

        // POST: Mecanicoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MecanicoID,Nombre,Apellido,UsuarioMecanico,ContrasenaMecanico")] Mecanico mecanico)
        {
            if (id != mecanico.MecanicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mecanico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MecanicoExists(mecanico.MecanicoID))
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
            return View(mecanico);
        }

        // GET: Mecanicoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mecanicos == null)
            {
                return NotFound();
            }

            var mecanico = await _context.Mecanicos
                .FirstOrDefaultAsync(m => m.MecanicoID == id);
            if (mecanico == null)
            {
                return NotFound();
            }

            return View(mecanico);
        }

        // POST: Mecanicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mecanicos == null)
            {
                return Problem("Entity set 'Context.Mecanicos'  is null.");
            }
            var mecanico = await _context.Mecanicos.FindAsync(id);
            if (mecanico != null)
            {
                _context.Mecanicos.Remove(mecanico);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MecanicoExists(int id)
        {
          return (_context.Mecanicos?.Any(e => e.MecanicoID == id)).GetValueOrDefault();
        }
    }
}
