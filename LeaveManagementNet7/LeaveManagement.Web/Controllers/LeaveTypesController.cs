using AutoMapper;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
    public class LeaveTypesController : Controller
    {
        private readonly LeaveManagementDbContext _context;
        private readonly IMapper _mapper;

        public LeaveTypesController(LeaveManagementDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            // when i return leaveType, I need to map into leaveTypeVM
            var leaveTypesVM = _mapper.Map<List<LeaveTypeVM>>(await _context.LeaveTypes.ToListAsync());

            return View(leaveTypesVM);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeVM leaveTypeVM)
        {
            if (ModelState.IsValid)
            {
                LeaveType leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
                _context.Add(leaveType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    var leaveType = await leaveTypeRepository.GetAsync(id);
        //    if (leaveType == null)
        //    {
        //        return NotFound();
        //    }

        //    var leaveTypeVM = mapper.Map<LeaveTypeVM>(leaveType);
        //    return View(leaveTypeVM);
        //}

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, LeaveTypeVM leaveTypeVM)
        //{
        //    if (id != leaveTypeVM.Id)
        //    {
        //        return NotFound();
        //    }

        //    var leaveType = await leaveTypeRepository.GetAsync(id);

        //    if (leaveType == null)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            mapper.Map(leaveTypeVM, leaveType);
        //            await leaveTypeRepository.UpdateAsync(leaveType);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!await leaveTypeRepository.Exists(leaveTypeVM.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(leaveTypeVM);
        //}

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            return View(leaveType);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await _context.LeaveTypes.FindAsync(id);
            _context.LeaveTypes.Remove(leaveType!);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); //reloads index page
        }

        private bool LeaveTypeExists(int id)
        {
            return _context.LeaveTypes.Any(e => e.Id == id);
        }
    }
}
