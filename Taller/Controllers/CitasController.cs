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
    public class CitasController : Controller
    {
        private readonly Context _context;

        public CitasController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index1()
        {
            return _context.Citas != null ?
                        View(await _context.Citas.ToListAsync()) :
                        Problem("Entity set 'Context.Citas'  is null.");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarCita(int citaID)
        {
            try
            {
                // Buscar la cita en la base de datos
                var cita = await _context.Citas.FindAsync(citaID);

                if (cita == null)
                {
                    return Json(new { success = false, message = "Cita no encontrada" });
                }

                // Eliminar la cita
                _context.Citas.Remove(cita);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cita eliminada con éxito" });
            }
            catch (DbUpdateException ex)
            {
                // Manejar errores de base de datos si es necesario
                return Json(new { success = false, message = "Error al eliminar la cita", error = ex.Message });
            }
        }
        public JsonResult ObtenerCitas()
        {
            var citas = _context.Citas.Select(c => new
            {
                title = "Cita",
                start = $"{c.FechaCita:yyyy-MM-dd}T{c.HoraCita:hh\\:mm}", // Ajusta según tu lógica
                color = "blue"
            });

            return Json(citas);
        }


        // Método para obtener la hora de inicio correspondiente al HorarioID


        // GET: Citas
        public IActionResult Index()
        {
           
            
            return View(_context.Citas.ToList());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas
                .FirstOrDefaultAsync(m => m.CitaID == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewBag.Mecanicos = _context.Mecanicos?.Select(d => new SelectListItem { Value = d.MecanicoID.ToString(), Text = d.Nombre }).ToList() ?? new List<SelectListItem>();
            ViewBag.Servicios = _context.Servicios?.Select(d => new SelectListItem { Value = d.ServicioID.ToString(), Text = d.Descripcion }).ToList() ?? new List<SelectListItem>();
            ViewBag.HorariosDisponibles = _context.HorariosDisponibles
    .Select(d => new SelectListItem
    {
        Value = d.HoraInicio.ToString(),
        Text = $"{(int)d.HoraInicio.TotalMinutes / 60:D2}:{(int)d.HoraInicio.TotalMinutes % 60:D2}"

    })
    .ToList() ?? new List<SelectListItem>();


            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaID,MecanicoID,HorarioID,FechaCita,HoraCita,EstadoCita,ServicioID")] Citas citas)
        {

            ViewBag.Mecanicos = _context.Mecanicos?.Select(d => new SelectListItem { Value = d.MecanicoID.ToString(), Text = d.Nombre }).ToList() ?? new List<SelectListItem>();
            ViewBag.Servicios = _context.Servicios?.Select(d => new SelectListItem { Value = d.ServicioID.ToString(), Text = d.Descripcion }).ToList() ?? new List<SelectListItem>();
            ViewBag.HorariosDisponibles = _context.HorariosDisponibles
    .Select(d => new SelectListItem
    {
        Value = d.HoraInicio.ToString(),
        Text = $"{(int)d.HoraInicio.TotalMinutes / 60:D2}:{(int)d.HoraInicio.TotalMinutes % 60:D2}"

    })
    .ToList() ?? new List<SelectListItem>();

            if (ModelState.IsValid)
            {
                if (_context.Citas.Any(c => c.FechaCita == citas.FechaCita && c.HoraCita == citas.HoraCita))
                {
                    ModelState.AddModelError(string.Empty, "Ya hay una cita programada para esta fecha y hora.");
                    // Puedes también recargar la lista de Mecánicos y Servicios si es necesario
                    return View(citas);
                }
                else {
                    _context.Add(citas);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                
            }
            return View(citas);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas.FindAsync(id);
            if (citas == null)
            {
                return NotFound();
            }
            return View(citas);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaID,MecanicoID,HorarioID,FechaCita,HoraCita,EstadoCita,ServicioID")] Citas citas)
        {
            if (id != citas.CitaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitasExists(citas.CitaID))
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
            return View(citas);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var citas = await _context.Citas
                .FirstOrDefaultAsync(m => m.CitaID == id);
            if (citas == null)
            {
                return NotFound();
            }

            return View(citas);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Citas == null)
            {
                return Problem("Entity set 'Context.Citas'  is null.");
            }
            var citas = await _context.Citas.FindAsync(id);
            if (citas != null)
            {
                _context.Citas.Remove(citas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitasExists(int id)
        {
          return (_context.Citas?.Any(e => e.CitaID == id)).GetValueOrDefault();
        }
    }
}
