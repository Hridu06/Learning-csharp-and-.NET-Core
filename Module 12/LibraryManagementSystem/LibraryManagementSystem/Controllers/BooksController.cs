using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .ToListAsync();

            return View(books);
        }

        
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BorrowRecords)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(
                _context.Authors.Select(a => new
                {
                    a.Id,
                    Name = a.FirstName + " " + a.LastName
                }),
                "Id",
                "Name"
            );
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ISBN,PublicationYear,TotalCopies,AvailableCopies,AuthorId")] Book book)
        {
            if (book.TotalCopies < 1)
            {
                ModelState.AddModelError("TotalCopies", "Total Copies must be at least 1.");
            }

            // AvailableCopies must equal TotalCopies when creating
            book.AvailableCopies = book.TotalCopies;

            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthorId"] = new SelectList(
                _context.Authors.Select(a => new
                {
                    a.Id,
                    Name = a.FirstName + " " + a.LastName
                }),
                "Id",
                "Name",
                book.AuthorId
            );
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(
                _context.Authors.Select(a => new
                {
                    a.Id,
                    Name = a.FirstName + " " + a.LastName
                }),
                "Id",
                "Name",
                book.AuthorId
);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ISBN,PublicationYear,TotalCopies,AvailableCopies,AuthorId")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["AuthorId"] = new SelectList(
            _context.Authors.Select(a => new
            {
                a.Id,
                Name = a.FirstName + " " + a.LastName
            }),
            "Id",
            "Name",
            book.AuthorId
            );
            return View(book);
        }


        // GET: Books/Borrow/5
        public async Task<IActionResult> Borrow(int? bookId)
        {
            if (bookId == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(bookId);

            if (book == null)
            {
                return NotFound();
            }

            ViewBag.BookTitle = book.Title;

            BorrowRecord borrowRecord = new BorrowRecord
            {
                BookId = book.Id,
                DueDate = DateTime.Now.AddDays(14)
            };

            return View(borrowRecord);
        }

        // POST: Books/Borrow
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrow(BorrowRecord borrowRecord)
        {
            var book = await _context.Books.FindAsync(borrowRecord.BookId);

            if (book == null)
            {
                return NotFound();
            }

            if (book.AvailableCopies <= 0)
            {
                ModelState.AddModelError("", "No copies available");

                ViewBag.BookTitle = book.Title;

                return View(borrowRecord);
            }

            borrowRecord.BorrowDate = DateTime.Now;

            _context.BorrowRecords.Add(borrowRecord);

            book.AvailableCopies--;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = book.Id });
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
