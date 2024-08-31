using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppSchoolApiClientDemo.Data;
using WebAppSchoolApiClientDemo.Models;
using WebAppSchoolApiClientDemo.ViewModels;

namespace WebAppSchoolApiClientDemo.Controllers
{
    public class CoursesController : Controller
    {
        private readonly SchoolDemoContext _context;
        private readonly IMapper _mapper;
        private readonly SchoolApiClient _schoolApiClient;

        public CoursesController(SchoolDemoContext context, IMapper mapper, SchoolApiClient schoolApiClient)
        {
            _context = context;
            _mapper = mapper;
            _schoolApiClient = schoolApiClient;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            //var courses = await _context.Courses.ToListAsync();
            var coursesDto = await _schoolApiClient.CoursesAllAsync();
            var coursesVm = _mapper.Map<List<CourseViewModel>>(coursesDto);
            return View(coursesVm);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            //var course = await _context.Courses
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var courseDto = await _schoolApiClient.CoursesGETAsync(id);

            if (courseDto == null)
            {
                return NotFound();
            }
            var courseVm = _mapper.Map<CourseViewModel>(courseDto);

            return View(courseVm);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CourseViewModel courseVm)
        {
            if (ModelState.IsValid)
            {
                var courseDto = _mapper.Map<CourseDto>(courseVm);
                //_context.Add(course);
                //await _context.SaveChangesAsync();
                courseDto = await _schoolApiClient.CoursesPOSTAsync(courseDto);

                return RedirectToAction(nameof(Index));
            }
            return View(courseVm);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            //var course = await _context.Courses.FindAsync(id);

            var courseDto = await _schoolApiClient.CoursesGETAsync(id);

            if (courseDto == null)
            {
                return NotFound();
            }
            var courseVm = _mapper.Map<CourseViewModel>(courseDto);
            return View(courseVm);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CourseViewModel courseVm)
        {
            if (id != courseVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var courseDto = _mapper.Map<CourseDto>(courseVm);
                    //_context.Update(course);
                    //await _context.SaveChangesAsync();

                    await _schoolApiClient.CoursesPUTAsync(id, courseDto);

                }
                catch (Exception e)
                {
                    if (!CourseExists(courseVm.Id))
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
            return View(courseVm);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            //var course = await _context.Courses
            //    .FirstOrDefaultAsync(m => m.Id == id);
            
            var courseDto = await _schoolApiClient.CoursesGETAsync(id);

            if (courseDto == null)
            {
                return NotFound();
            }

            var courseVm = _mapper.Map<CourseViewModel>(courseDto);

            return View(courseVm);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var course = await _context.Courses.FindAsync(id);

            var courseDto = await _schoolApiClient.CoursesGETAsync(id);

            if (courseDto != null)
            {
                await _schoolApiClient.CoursesDELETEAsync(id);
            }

            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
