using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.IISUrlRewrite;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Web_App.Models;
using Web_App.Helpers;
using Web_App.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_App.ViewModels;

namespace Web_App.Controllers
{
    public class CandidateController : Controller
    {
        private readonly IVacancyService _vacancyService;
        private readonly ICandidateService _candidateService;
        private readonly DBContext _context;
        
        public CandidateController(DBContext context, IVacancyService vacancyService, ICandidateService candidateService)
        {
            _vacancyService = vacancyService;
            _candidateService = candidateService;
            _context = context;
        }
        
        public IActionResult Index()
        {
            ViewData["VacanciesList"] = _vacancyService.GetVacanciesList();
            
            return View(_candidateService.GetCandidatesList().OrderBy(x => x.Id));
        }
        
        public IActionResult Search(string searchString)
        {
            if (searchString != null)
            {
                return new JsonResult(_candidateService.SearchCandidate(searchString).OrderBy(x => x.Id));
            }
            else
            {
                return new JsonResult(_candidateService.GetCandidatesList().OrderBy(x => x.Id));
            }
            /*if (!String.IsNullOrEmpty(searchString))
            {
                ViewData["VacanciesList"] = _vacancyService.GetVacanciesList();
                return View(_candidateService.SearchCandidate(searchString).OrderBy(x => x.Id));
            }
            
            return RedirectToAction(nameof(Index));*/
        }
        
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            ViewData["VacanciesList"] = _vacancyService.GetVacanciesList();
            
            return View(new CandidateCreateViewModel());
        }
        
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormFile avatar, CandidateCreateViewModel candidateView)
        {
            if (ModelState.IsValid)
            {
                _candidateService.CreateCandidate(avatar, candidateView);
                
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Create));
        }
        
        [Authorize(Policy = "Admin")]
        public IActionResult Edit(int? id)
        {
            ViewData["VacanciesList"] = _vacancyService.GetVacanciesList();

            return View(_candidateService.GetCandidateByID(id));
        }
        
        [Authorize(Policy = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(IFormFile avatar, CandidateEditViewModel candidateView)
        {
            if (ModelState.IsValid)
            {
                _candidateService.EditCandidate(avatar, candidateView);
              
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Edit));
        }
        
        [Authorize(Policy = "Admin")]
        public IActionResult Delect(int? id)
        {
            _candidateService.DelectCandidate(id);            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Filter(int? vacancyId)
        {
            if (vacancyId == 0)
            {
                return new JsonResult(_candidateService.GetCandidatesList().OrderBy(x => x.Id));
            }
            else
            {
                return new JsonResult(_candidateService.GetCandidateByRole(vacancyId).OrderBy(x => x.Id));
            }
        }

        public IActionResult Sort(string sortType)
        {
            var candidate = _candidateService.GetCandidatesList();
            switch (sortType)
            {
                case "des":
                    candidate = candidate.OrderByDescending(x => x.Id);
                    break;
                case "asc":
                    candidate = candidate.OrderBy(x => x.Id);
                    break;
            }
            return new JsonResult(candidate);
        }
    }
}