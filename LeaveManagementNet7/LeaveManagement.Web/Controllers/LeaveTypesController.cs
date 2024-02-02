using AutoMapper;
using LeaveManagement.Web.Data;
using LeaveManagement.Web.Models;
using LeaveManagement.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagement.Web.Controllers
{
    public class LeaveTypesController : Controller
    {

        private readonly IMapper _mapper;

        public ILeaveTypeRepository LeaveTypeRepository { get; }

        public LeaveTypesController(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            LeaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            // when i return leaveType, I need to map into leaveTypeVM
            var leaveTpes = await LeaveTypeRepository.GetAllAsync();
            var leaveTypesVM = _mapper.Map<List<LeaveTypeVM>>(leaveTpes);

            return View(leaveTypesVM);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {   //why id.value
            var leaveType = await LeaveTypeRepository.GetAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }

            var leaveTypeVM = _mapper.Map<LeaveTypeVM>(leaveType);

            return View(leaveTypeVM);
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
                await LeaveTypeRepository.AddAsync(leaveType);
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeVM);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var leaveType = await LeaveTypeRepository.GetAsync(id);

            if (leaveType == null)
            {
                return NotFound();
            }

            var leaveTypeVM = _mapper.Map<LeaveTypeVM>(leaveType);

            return View(leaveTypeVM);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeVM leaveTypeVM)
        {
            if (id != leaveTypeVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    LeaveType leaveType = _mapper.Map<LeaveType>(leaveTypeVM);
                    await LeaveTypeRepository.UpdateAsync(leaveType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LeaveTypeRepository.Exists(id))
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
            return View(leaveTypeVM);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            await LeaveTypeRepository.DeleteAsync(id);

            return RedirectToAction(nameof(Index)); //reloads index page
        }

        //// POST: LeaveTypes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var leaveType = await LeaveTypeRepository.GetAsync(id);
        //    await LeaveTypeRepository.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index)); //reloads index page
        //}


    }
}
