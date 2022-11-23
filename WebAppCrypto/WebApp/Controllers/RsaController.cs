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
using Rsa = WebApp.domain.Rsa;

namespace WebApp.Controllers
{
    public class RsaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RsaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rsa
        public async Task<IActionResult> Index()
        {
            try
            {
                var applicationDbContext =
                    _context
                        .Rsa
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

        // GET: Rsa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rsa == null)
            {
                return NotFound();
            }

            var rsa = await _context.Rsa
                .Where(c => c.AppUserId == GetLoggedInUserId())//we are fetching only the entries we are entitled to
                .Include(c => c.AppUser)
                .SingleOrDefaultAsync(m => m.Id == id);//we cannot get other id other than the one that it was fetched to us it can go here
            //or in the where statement.
            if (rsa == null)
            {
                return NotFound();
            }

            return View(rsa);
        }

        // GET: Rsa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rsa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PrimeP,PrimeQ,E,Message,Base64EncryptedMessage,AppUserId")] Rsa rsa)
        {
            rsa.AppUserId = GetLoggedInUserId();

            if (ModelState.IsValid)
            {
                try
                {
                    Crypto.RsaUtils.CheckInput(rsa.Message, rsa.Base64EncryptedMessage);
                    rsa.PrimeP = Crypto.Rsa.BiggestPrime(rsa.PrimeP);
                    rsa.PrimeQ = Crypto.Rsa.BiggestPrime(rsa.PrimeQ);
                    Crypto.RsaUtils.PandQ(rsa.PrimeP, rsa.PrimeQ);
                    var n = Crypto.Rsa.NandPhi(rsa.PrimeP, rsa.PrimeQ).Item1;
                    var phi = Crypto.Rsa.NandPhi(rsa.PrimeP, rsa.PrimeQ).Item2;
                    var eValid = Crypto.Rsa.ValidExponent(rsa.E, phi);
                    if (rsa.Base64EncryptedMessage == null)
                    {
                        //we encrypt


                        var textNumber = Crypto.Rsa.TextToNumber(n, rsa.Message!);
                        var encryptedNumber = Crypto.Rsa.Modpow_expsqr(textNumber, eValid, n);
                        var nBytesEncryption = RsaUtils.NumberOfBytes(encryptedNumber);
                        var byteEncrypted = Crypto.Rsa.NumberToBytes(encryptedNumber, nBytesEncryption);
                        rsa.Base64EncryptedMessage = Crypto.Rsa.Encryption(byteEncrypted);
                    }

                    if (rsa.Message == null)
                    {
                        // we decrypt
                        var d = Crypto.Rsa.ExtendedEuclid(eValid, phi);
                        var encryptedNumber2 = Crypto.Rsa.Base64ToNumber(n, rsa.Base64EncryptedMessage);
                        var textNumber2 = Crypto.Rsa.Modpow_expsqr(encryptedNumber2, d, n);
                        var nBytesDecryption = RsaUtils.NumberOfBytes(textNumber2);
                        var byteDecrypted = Crypto.Rsa.NumberToBytes(textNumber2, nBytesDecryption);
                        rsa.Message = Crypto.Rsa.Decryption(byteDecrypted);
                    }

                    _context.Add(rsa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    return View(rsa);
                }
                

            }
            return View(rsa);
        }

        // GET: Rsa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rsa == null)
            {
                return NotFound();
            }

            var rsa = await _context
                .Rsa
                .Where(c=>c.AppUserId == GetLoggedInUserId() && c.Id==id )
                .SingleOrDefaultAsync();//check that list has one only element or no elements at all
            if (rsa == null)
            {
                return NotFound();
            }
            return View(rsa);
        }

        // POST: Rsa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PrimeP,PrimeQ,E,Message,Base64EncryptedMessage,AppUserId")] Rsa rsa)
        {
            rsa.AppUserId = GetLoggedInUserId(); 
            var isOwned = await _context.Rsa.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());

            if (id != rsa.Id && !isOwned)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Crypto.RsaUtils.CheckInput(rsa.Message, rsa.Base64EncryptedMessage);
                    rsa.PrimeP = Crypto.Rsa.BiggestPrime(rsa.PrimeP);
                    rsa.PrimeQ = Crypto.Rsa.BiggestPrime(rsa.PrimeQ);
                    Crypto.RsaUtils.PandQ(rsa.PrimeP, rsa.PrimeQ);
                    var n = Crypto.Rsa.NandPhi(rsa.PrimeP, rsa.PrimeQ).Item1;
                    var phi = Crypto.Rsa.NandPhi(rsa.PrimeP, rsa.PrimeQ).Item2;
                    var eValid = Crypto.Rsa.ValidExponent(rsa.E, phi);
                    if (rsa.Base64EncryptedMessage == null)
                    {
                        //we encrypt


                        var textNumber = Crypto.Rsa.TextToNumber(n, rsa.Message!);
                        var encryptedNumber = Crypto.Rsa.Modpow_expsqr(textNumber, eValid, n);
                        var nBytesEncryption = RsaUtils.NumberOfBytes(encryptedNumber);
                        var byteEncrypted = Crypto.Rsa.NumberToBytes(encryptedNumber, nBytesEncryption);
                        rsa.Base64EncryptedMessage = Crypto.Rsa.Encryption(byteEncrypted);
                    }

                    if (rsa.Message == null)
                    {
                        // we decrypt
                        var d = Crypto.Rsa.ExtendedEuclid(eValid, phi);
                        var encryptedNumber2 = Crypto.Rsa.Base64ToNumber(n, rsa.Base64EncryptedMessage);
                        var textNumber2 = Crypto.Rsa.Modpow_expsqr(encryptedNumber2, d, n);
                        var nBytesDecryption = RsaUtils.NumberOfBytes(textNumber2);
                        var byteDecrypted = Crypto.Rsa.NumberToBytes(textNumber2, nBytesDecryption);
                        rsa.Message = Crypto.Rsa.Decryption(byteDecrypted);
                    }

                    _context.Update(rsa);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RsaExists(rsa.Id))
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
                    return View(rsa);
                }
               
            }
            return View(rsa);
        }

        // GET: Rsa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rsa == null)
            {
                return NotFound();
            }

            var rsa = await _context.Rsa
                .Where(c => c.AppUserId == GetLoggedInUserId())
                .Include(c => c.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rsa == null)
            {
                return NotFound();
            }

            return View(rsa);
        }

        // POST: Rsa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rsa == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Rsa'  is null.");
            }
            var rsa = await _context.Rsa.FindAsync(id);
            var isOwned = await _context.Rsa.AnyAsync(c => c.Id == id && c.AppUserId == GetLoggedInUserId());
            if (isOwned)
            {
                _context.Rsa.Remove(rsa);
            }

            if (!isOwned)
                return NotFound();
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RsaExists(int id)
        {
          return (_context.Rsa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
