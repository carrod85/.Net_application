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
    public class VigenereController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VigenereController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vigenere
        public async Task<IActionResult> Index()
        {
            try
            {

                var applicationDbContext = _context
                    .Vigenere
                    .Where(c => c.AppUserId == GetLoggedInUserId())
                    .Include(c => c.AppUser);
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

        // GET: Vigenere/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vigenere == null)
            {
                return NotFound();
            }

            var vigenere = await _context.Vigenere
                .Where(c => c.AppUserId == GetLoggedInUserId())
                .Include(v => v.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vigenere == null)
            {
                return NotFound();
            }

            return View(vigenere);
        }

        // GET: Vigenere/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vigenere/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlainText,KeyPass,Encryption,AppUserId")] Vigenere vigenere)
        {
            vigenere.AppUserId = GetLoggedInUserId();
            if (ModelState.IsValid)
            {
                try
                {
                    
                    Crypto.Vigenere.CheckInput(vigenere.PlainText,vigenere.Encryption);
                    if (vigenere.PlainText == null)
                    {
                        Crypto.Vigenere.IsValidBase64(vigenere.Encryption);
                        vigenere.PlainText= Crypto.Vigenere.Decryption(vigenere.Encryption, vigenere.KeyPass);
                    }
                    if (vigenere.Encryption == null)
                        vigenere.Encryption = Crypto.Vigenere.Encryption(vigenere.PlainText, vigenere.KeyPass);
                    
                    _context.Add(vigenere);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(vigenere);
                }
            }
            
            return View(vigenere);
        }

        // GET: Vigenere/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vigenere == null)
            {
                return NotFound();
            }

            var vigenere = await _context
                .Vigenere
                .Where(c=>c.AppUserId == GetLoggedInUserId() && c.Id==id )
                .SingleOrDefaultAsync();//check that list has one only element or no elements at all
            
            if (vigenere == null)
            {
                return NotFound();
            }
            return View(vigenere);
        }

        // POST: Vigenere/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlainText,KeyPass,Encryption,AppUserId")] Vigenere vigenere)
        {
            vigenere.AppUserId = GetLoggedInUserId();
            var isOwned = await _context.Vigenere.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (id != vigenere.Id && !isOwned)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Crypto.Vigenere.CheckInput(vigenere.PlainText,vigenere.Encryption);
                    if (vigenere.PlainText == null)
                    {
                        Crypto.Vigenere.IsValidBase64(vigenere.Encryption);
                        vigenere.PlainText= Crypto.Vigenere.Decryption(vigenere.Encryption, vigenere.KeyPass);
                    }
                    if (vigenere.Encryption == null)
                        vigenere.Encryption = Crypto.Vigenere.Encryption(vigenere.PlainText, vigenere.KeyPass);
                    
                    _context.Update(vigenere);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VigenereExists(vigenere.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(vigenere);
                }
                
            }
            return View(vigenere);
        }

        // GET: Vigenere/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vigenere == null)
            {
                return NotFound();
            }

            var vigenere = await _context.Vigenere
                .Where(c => c.AppUserId == GetLoggedInUserId())
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vigenere == null)
            {
                return NotFound();
            }

            return View(vigenere);
        }

        // POST: Vigenere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vigenere == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Vigenere'  is null.");
            }
            var vigenere = await _context.Vigenere.FindAsync(id);
            var isOwned = await _context.Vigenere.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (isOwned)
            {
                _context.Vigenere.Remove(vigenere);
            }
            if (!isOwned)
                return NotFound();
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VigenereExists(int id)
        {
          return (_context.Vigenere?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
