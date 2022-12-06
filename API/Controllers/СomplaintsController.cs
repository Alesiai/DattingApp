using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Authorize]
    public class СomplaintsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _context;
        public СomplaintsController(DataContext context, IUnitOfWork unitOfWork, IMapper mapper,
            IPhotoService photoService)
        {
            _context = context;

            _unitOfWork = unitOfWork;
            _photoService = photoService;
            _mapper = mapper;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Сomplaints>>> GetСomplaints()
        {
            return await _context.Сomplaints.Where(x => x.Processed == false).ToListAsync();
        }

        [HttpPost]
        public ActionResult PutСomplaints(СomplaintsDto complaintsDto)
        {
            try
            {
                _context.Сomplaints.Add(new Сomplaints
                {
                    Message = complaintsDto.Message,
                    UserName = complaintsDto.UserName,
                    Processed = false,
                });

                _context.SaveChanges();

                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to update user");
            }
        }


        [Authorize]
        [HttpPost("set-processed/{id}")]
        public ActionResult SetProcessed(int id)
        {
            try
            {
                var complaints = _context.Сomplaints.Where(x => x.Id == id).FirstOrDefault();

                complaints.Processed = true;
                _context.SaveChanges();

                return NoContent();
            }
            catch
            {
                return BadRequest("Failed to update user");
            }
        }
    }
}