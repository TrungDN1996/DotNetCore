using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Web_App.Helpers;
using Web_App.Entities;
using Web_App.ViewModels;

namespace Web_App.Models
{
    public interface ICandidateService
    {
        IEnumerable<CandidateViewModel> GetCandidatesList();

        CandidateEditViewModel GetCandidateByID(int? id);

        IEnumerable<CandidateViewModel> GetCandidateByRole(int? vacancyId);

        IEnumerable<CandidateViewModel> SearchCandidate(string searchString);

        void CreateCandidate(IFormFile avatar, CandidateCreateViewModel candidateView);

        void EditCandidate(IFormFile avatar, CandidateEditViewModel candidateview);
        
        void DelectCandidate(int? id);
    }
    
    public class CandidateService : ICandidateService
    {
        private readonly DBContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CandidateService(DBContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        
        public IEnumerable<CandidateViewModel> GetCandidatesList()
        {
            var list = _context.Candidates.Include(x => x.Vacancy)
                .Select(x => new CandidateViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    VacancyName = x.Vacancy.Name,
                    Avatar = x.Avatar
                })
                .ToList();
            return list;
        }

        public CandidateEditViewModel GetCandidateByID(int? id)
        {
            var candidate = _context.Candidates.Include(x => x.Vacancy)
                .Where(x => x.Id == id)
                .Select(x => new CandidateEditViewModel()
                {
                    Id = x.Id, 
                    Name = x.Name, 
                    Phone = x.Phone, 
                    VacancyId = x.Vacancy.Id,
                    Avatar = x.Avatar,
                })
                .FirstOrDefault();
            return candidate;
        }

        public IEnumerable<CandidateViewModel> GetCandidateByRole(int? vacancyId)
        {
            var candidate = _context.Candidates.Include(x => x.Vacancy)
                .Where(x => x.VacancyID == vacancyId)
                .Select(x => new CandidateViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    VacancyName = x.Vacancy.Name,
                    Avatar = x.Avatar,
                })
                .ToList();

            return candidate;
        }
        
        public IEnumerable<CandidateViewModel> SearchCandidate(string searchString)
        {
            /*var candidates = from m in _context.Candidates
                select m;*/
            var candidates = _context.Candidates.Include(x => x.Vacancy)
                .Where(x => x.Name.Contains(searchString) ||
                            x.Phone.Contains((searchString)) ||
                            x.Vacancy.Name.Contains(searchString))
                .Select(x => new CandidateViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    VacancyName = x.Vacancy.Name,
                    Avatar = x.Avatar,
                })
                .ToList();
            return candidates;
        }

        public void DelectCandidate(int? id)
        {
            if (id != 0)
            {
                var candidate = _context.Candidates.Find(id);

                if (candidate != null)
                {
                    _context.Candidates.Remove(candidate);
                    _context.SaveChanges();
                }
            }
        }

        public void CreateCandidate(IFormFile avatar, CandidateCreateViewModel candidateView)
        {
            if (avatar != null)
            {
                /*upload file*/
                var storagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", avatar.FileName);
                var path = "/images/" + avatar.FileName;
                var stream = new FileStream(storagePath, FileMode.Create);
                avatar.CopyToAsync(stream);
                
                Candidate candidate = new Candidate()
                {
                    Name = candidateView.Name,
                    Phone = candidateView.Phone,
                    VacancyID = candidateView.VacancyId,
                    Avatar = path,
                };
                
                _context.Candidates.Add(candidate);
                _context.SaveChanges();
            }
            else
            {
                Candidate candidate = new Candidate()
                {
                    Name = candidateView.Name,
                    Phone = candidateView.Phone,
                    VacancyID = candidateView.VacancyId,
                };
                
                _context.Candidates.Add(candidate);
                _context.SaveChanges();
            }
        }

        public void EditCandidate(IFormFile avatar, CandidateEditViewModel candidateView)
        {
            string path;
            
            if (avatar != null)
            {
                /*upload file*/
                var storagePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", avatar.FileName);
                path = "/images/" + avatar.FileName;
                var stream = new FileStream(storagePath, FileMode.Create);
                avatar.CopyToAsync(stream);
                
                Candidate candidate = new Candidate()
                {
                    Id = candidateView.Id,
                    Name = candidateView.Name,
                    Phone = candidateView.Phone,
                    VacancyID = candidateView.VacancyId,
                    Avatar = path,
                };
                
                _context.Candidates.Update(candidate);
                _context.SaveChanges();
            }
            else
            {
                path = GetCandidateByID(candidateView.Id).Avatar;
                
                Candidate candidate = new Candidate()
                {
                    Id = candidateView.Id,
                    Name = candidateView.Name,
                    Phone = candidateView.Phone,
                    VacancyID = candidateView.VacancyId,
                    Avatar = path,
                };
                
                _context.Candidates.Update(candidate);
                _context.SaveChanges();
            }
        }
    }
}