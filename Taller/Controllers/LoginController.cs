using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Taller.Models;

public class LoginController : Controller
{
    private readonly Context _context;

    public LoginController(Context context)
    {
        _context = context;
    }

    // Acción para mostrar el formulario de inicio de sesión
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult LoginM()
    {
        return View();
    }



    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
           
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            

            return RedirectToAction("Index", "Home"); 
        }

        return View(usuario);
    }


    // Acción para procesar el inicio de sesión
   [HttpPost]
public async Task<IActionResult> LoginUsuario(Usuario usuario)
{
    var usuarioEncontrado = await _context.Usuarios
        .FirstOrDefaultAsync(u => u.CorreoElectronico == usuario.CorreoElectronico && u.Contrasena == usuario.Contrasena);

    if (usuarioEncontrado != null)
    {
        HttpContext.Session.SetInt32("USU_ID", usuarioEncontrado.UsuarioID);
        HttpContext.Session.SetString("USU_Nombre", usuarioEncontrado.Nombre);
        return RedirectToAction("Index", "Home");
    }

    ModelState.AddModelError("", "Credenciales inválidas");
    return View("Login"); // Vista para Usuarios
}

[HttpPost]
public async Task<IActionResult> LoginMecanico(Mecanico usuario)
{
    var mecanicoEncontrado = await _context.Mecanicos
        .FirstOrDefaultAsync(m => m.UsuarioMecanico == usuario.UsuarioMecanico && m.ContrasenaMecanico == usuario.ContrasenaMecanico);

    if (mecanicoEncontrado != null)
    {
        HttpContext.Session.SetInt32("MEC_ID", mecanicoEncontrado.MecanicoID);
        HttpContext.Session.SetString("MEC_Nombre", mecanicoEncontrado.Nombre);
        return RedirectToAction("Index", "Home");
    }

    ModelState.AddModelError("", "Credenciales inválidas");
    return View("LoginM"); // Vista para Mecánicos
}

    // Acción para cerrar sesión
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home"); 
    }
}

