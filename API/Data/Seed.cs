using API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace API.Data
{
     public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach (var user in users)
            {
                using var hmack = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmack.ComputeHash(Encoding.UTF8.GetBytes("pa$$w0rd"));
                user.PasswordSalt = hmack.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}