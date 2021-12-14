using API.Entities;
using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using API.Helpers;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper  _mapper;
        public UserRepository(DataContext context, IMapper mapper){
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users.Include(user => user.Photos).ToListAsync();
        }
        
        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
            .Include(user => user.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State  = EntityState.Modified;
        }

        public async Task<IEnumerable<MemberDto>> GetMemebersAsync()
        {
             return await _context.Users.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users.
                Where(user => user.UserName.Equals(username)).
                ProjectTo<MemberDto>(_mapper.ConfigurationProvider).
                SingleOrDefaultAsync();
        }
    }
}