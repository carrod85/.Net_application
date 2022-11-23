using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Crypto;
using WebApp.Data;
using Cesar = WebApp.domain.Cesar;

namespace WebApp.Controllers
{
    public class CesarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CesarController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cesar
        // we are hiding the content of the user
        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationDbContext =
                    _context
                        .Cesar
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

        // GET: Cesar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cesar == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesar
                .Where(c => c.AppUserId == GetLoggedInUserId())//we are fetching only the entries we are entitled to
                .Include(c => c.AppUser)
                .SingleOrDefaultAsync(m => m.Id == id);//we cannot get other id other than the one that it was fetched to us it can go here
            //or in the where statement.
            if (cesar == null)
            {
                return NotFound();
            }

            return View(cesar);
        }

        // GET: Cesar/Create
        public IActionResult Create()
        {
            //ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id"); So to avoid display the list of Users.
            return View();
        }

        // POST: Cesar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Alphabet,PlainText,Key,CypherText,AppUserId")] Cesar cesar)
        {
            cesar.AppUserId = GetLoggedInUserId();
            if (ModelState.IsValid)
            {
                try
                {
                    var alphabet = CesarUtils.CleaningAlpha(cesar.Alphabet);
                    var shift = CesarUtils.CleaningShift(cesar.Key, alphabet);
                    var plainText = CesarUtils.CleaningText(cesar.PlainText, alphabet);
                    var encryption = WebApp.Crypto.Cesar.Encryption(alphabet, shift, plainText);
                    cesar.Alphabet = alphabet;
                    cesar.Key = shift;
                    cesar.PlainText = plainText;
                    cesar.CypherText = encryption;
                    _context.Add(cesar);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(cesar);
                }
            }

            return View(cesar);
        }

        // GET: Cesar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cesar == null)
            {
                return NotFound();
            }

            var cesar = await _context
                .Cesar
                .Where(c=>c.AppUserId == GetLoggedInUserId() && c.Id==id )
                .SingleOrDefaultAsync();//check that list has one only element or no elements at all
            if (cesar == null)
            {
                return NotFound();
            }
            return View(cesar);
        }

        // POST: Cesar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Alphabet,PlainText,Key,CypherText,AppUserId")] Cesar cesar)
        {//id of url
            
            cesar.AppUserId = GetLoggedInUserId(); 
            // not enough because we can change the id url and in form. 
            // c.Id is the one on the form and id is obtained get method (url)
            

            var isOwned = await _context.Cesar.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (id != cesar.Id && !isOwned)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var alphabet = CesarUtils.CleaningAlpha(cesar.Alphabet);
                    var shift = CesarUtils.CleaningShift(cesar.Key, alphabet);
                    var plainText = CesarUtils.CleaningText(cesar.PlainText, alphabet);
                    var encryption = WebApp.Crypto.Cesar.Encryption(alphabet, shift, plainText);
                    cesar.Alphabet = alphabet;
                    cesar.Key = shift;
                    cesar.PlainText = plainText;
                    cesar.CypherText = encryption;

                    _context.Update(cesar);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CesarExists(cesar.Id))
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
                    return View(cesar);
                }
            }
            return View(cesar);
        }

        // GET: Cesar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cesar == null)
            {
                return NotFound();
            }

            var cesar = await _context.Cesar
                .Where(c => c.AppUserId == GetLoggedInUserId())
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cesar == null)
            {
                return NotFound();
            }

            return View(cesar);
        }

        // POST: Cesar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            
            if (_context.Cesar == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cesar'  is null.");
            }
            var cesar = await _context.Cesar.FindAsync(id);
            var isOwned = await _context.Cesar.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (isOwned)
            {
                _context.Cesar.Remove(cesar);
            }

            if (!isOwned)
                return NotFound();
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CesarExists(int id)
        {
          return (_context.Cesar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
