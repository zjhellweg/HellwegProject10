#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HellwegProject10.Data;
using HellwegProject10.Models;

namespace HellwegProject10.Controllers
{
    public class SalesPersonsController : Controller
    {
        private readonly Adventureworks2019Context _context;

        public SalesPersonsController(Adventureworks2019Context context)
        {
            _context = context;
        }

        // GET: SalesPersons
        public async Task<IActionResult> Index()
        {
            var adventureworks2019Context = _context.SalesPeople.Include(s => s.BusinessEntity).Include(s => s.Territory);
            return View(await adventureworks2019Context.ToListAsync());
        }

        // GET: SalesPersons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPeople
                .Include(s => s.BusinessEntity)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // GET: SalesPersons/Create
        public IActionResult Create()
        {
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender");
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode");
            return View();
        }

        // POST: SalesPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BusinessEntityId,TerritoryId,SalesQuota,Bonus,CommissionPct,SalesYtd,SalesLastYear,Rowguid,ModifiedDate")] SalesPerson salesPerson)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesPerson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", salesPerson.BusinessEntityId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", salesPerson.TerritoryId);
            return View(salesPerson);
        }

        // GET: SalesPersons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPeople.FindAsync(id);
            if (salesPerson == null)
            {
                return NotFound();
            }
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", salesPerson.BusinessEntityId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", salesPerson.TerritoryId);
            return View(salesPerson);
        }

        // POST: SalesPersons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BusinessEntityId,TerritoryId,SalesQuota,Bonus,CommissionPct,SalesYtd,SalesLastYear,Rowguid,ModifiedDate")] SalesPerson salesPerson)
        {
            if (id != salesPerson.BusinessEntityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesPerson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesPersonExists(salesPerson.BusinessEntityId))
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
            ViewData["BusinessEntityId"] = new SelectList(_context.Employees, "BusinessEntityId", "Gender", salesPerson.BusinessEntityId);
            ViewData["TerritoryId"] = new SelectList(_context.SalesTerritories, "TerritoryId", "CountryRegionCode", salesPerson.TerritoryId);
            return View(salesPerson);
        }

        // GET: SalesPersons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesPerson = await _context.SalesPeople
                .Include(s => s.BusinessEntity)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
            if (salesPerson == null)
            {
                return NotFound();
            }

            return View(salesPerson);
        }

        // POST: SalesPersons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesPerson = await _context.SalesPeople.FindAsync(id);
            _context.SalesPeople.Remove(salesPerson);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesPersonExists(int id)
        {
            return _context.SalesPeople.Any(e => e.BusinessEntityId == id);
        }
    }
}
