using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.domain;

namespace WebApp.Controllers
{
    public class DiffieHellmanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiffieHellmanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiffieHellman
        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationDbContext =
                    _context
                        .DHellman
                        .Where(c => c.AppUserId == GetLoggedInUserId())
                        .Include(c => c.AppUser); //using navigator to access other tables fields.
                return View(await applicationDbContext.ToListAsync());
            }
            catch (Exception e)
            {
                return NotFound(Problem(detail:"You are not logged in"));
            }
        }
        
        public string GetLoggedInUserId()
        {
            return User.Claims.First(cm => cm.Type == ClaimTypes.NameIdentifier).Value;
        }

        // GET: DiffieHellman/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || _context.DHellman == null)
            {
                return NotFound();
            }

            var dHellman = await _context.DHellman
                .Where(c => c.AppUserId == GetLoggedInUserId())//we are fetching only the entries we are entitled to
                .Include(c => c.AppUser)
                .SingleOrDefaultAsync(m => m.Id == id);//we cannot get other id other than the one that it was fetched to us it can go here
            //or in the where statement.
            if (dHellman == null)
            {
                return NotFound();
            }

            return View(dHellman);
        }

        // GET: DiffieHellman/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DiffieHellman/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Modulo,Group,PrivateX,PrivateY,SharedSecret,AppUserId")] DHellman dHellman)
        {
            dHellman.AppUserId = GetLoggedInUserId();

            if (ModelState.IsValid)
            {
                try
                {
                    dHellman.Modulo = Crypto.Dh.BiggestPrime(dHellman.Modulo);
                    Crypto.Dh.CheckGroup(dHellman.Group);
                    Crypto.Dh.CheckSecret(dHellman.PrivateX, dHellman.Modulo);
                    Crypto.Dh.CheckSecret(dHellman.PrivateY, dHellman.Modulo);
                    var gX = Crypto.Dh.Modpow_expsqr(dHellman.Group, dHellman.PrivateX, dHellman.Modulo);
                    var gY = Crypto.Dh.Modpow_expsqr(dHellman.Group, dHellman.PrivateY, dHellman.Modulo);
                    var sharedX = Crypto.Dh.Modpow_expsqr(gX, dHellman.PrivateY, dHellman.Modulo);
                    var sharedY = Crypto.Dh.Modpow_expsqr(gY, dHellman.PrivateX, dHellman.Modulo);
                    if (sharedX != sharedY)
                        throw new ArgumentException("something wrong happened. Try again.");

                    dHellman.SharedSecret = sharedX;

                    _context.Add(dHellman);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(dHellman);
                }
            }

            return View(dHellman);
        }

        // GET: DiffieHellman/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DHellman == null)
            {
                return NotFound();
            }

            var dHellman = await _context
                .DHellman
                .Where(c=>c.AppUserId == GetLoggedInUserId() && c.Id==id )
                .SingleOrDefaultAsync();//check that list has one only element or no elements at all            if (dHellman == null)
            
            if (dHellman == null)
            {
                return NotFound();
            }
            return View(dHellman);
        }

        // POST: DiffieHellman/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Modulo,Group,PrivateX,PrivateY,SharedSecret,AppUserId")] DHellman dHellman)
        {
            dHellman.AppUserId = GetLoggedInUserId(); 
            var isOwned = await _context.DHellman.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (id != dHellman.Id && !isOwned)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dHellman.Modulo = Crypto.Dh.BiggestPrime(dHellman.Modulo);
                    Crypto.Dh.CheckGroup(dHellman.Group);
                    Crypto.Dh.CheckSecret(dHellman.PrivateX, dHellman.Modulo);
                    Crypto.Dh.CheckSecret(dHellman.PrivateY, dHellman.Modulo);
                    var gX = Crypto.Dh.Modpow_expsqr(dHellman.Group, dHellman.PrivateX, dHellman.Modulo);
                    var gY = Crypto.Dh.Modpow_expsqr(dHellman.Group, dHellman.PrivateY, dHellman.Modulo);
                    var sharedX = Crypto.Dh.Modpow_expsqr(gX, dHellman.PrivateY, dHellman.Modulo);
                    var sharedY = Crypto.Dh.Modpow_expsqr(gY, dHellman.PrivateX, dHellman.Modulo);
                    if (sharedX != sharedY)
                        throw new ArgumentException("something wrong happened. Try again.");

                    dHellman.SharedSecret = sharedX;
                    _context.Update(dHellman);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DHellmanExists(dHellman.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(dHellman);
                }
            }
            return View(dHellman);
        }

        // GET: DiffieHellman/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DHellman == null)
            {
                return NotFound();
            }

            var dHellman = await _context.DHellman
                .Where(c => c.AppUserId == GetLoggedInUserId())
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dHellman == null)
            {
                return NotFound();
            }

            return View(dHellman);
        }

        // POST: DiffieHellman/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DHellman == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DHellman'  is null.");
            }
            var dHellman = await _context.DHellman.FindAsync(id);
            var isOwned = await _context.DHellman.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (isOwned)
            {
                _context.DHellman.Remove(dHellman);
            }
            if (!isOwned)
                return NotFound();
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DHellmanExists(int id)
        {
          return (_context.DHellman?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
