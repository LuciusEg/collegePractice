using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using documentation;
using documentation.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace documentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DocumentationContext _context;

        public UsersController(DocumentationContext context)
        {
            _context = context;
        }
        //не забыть поменять подключение к бд
        //Продолжить работу тут
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return BadRequest();
        }

        //вывод всех пользователей, работает
        [HttpGet("allUsers")]
        public async Task<ActionResult<IEnumerable<User>>> allUsers()
        {
            return await _context.Users.ToListAsync();
        }

        //работает
        [HttpGet("oneUsers")]
        public async Task<ActionResult<IEnumerable<User>>> oneUsers(int id)
        {
            return await _context.Users
               .Select(x => UserTO(x))
               .ToListAsync();
        }

        [HttpPost("createUsers")]
        public async Task<ActionResult<IEnumerable<User>>> createUsers(User userTO)
        {
            var user = new User
            {
                FirstName = userTO.FirstName,
                LastName = userTO.LastName,
                MiddleName = userTO.MiddleName,
                Role = userTO.Role,
                JobTitle = userTO.JobTitle,
                Department = userTO.Department
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetUsers),
                new { id = user.Id },
                UserTO(user));
        }
        //работает
        [HttpPut("UpdateUsers")]
        public async Task<ActionResult<IEnumerable<User>>> UpdateUsers(int id, [Bind("Id,FirstName,LastName,MiddleName,Role,JobTitle,Department")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExist(user.Id))
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
            return NoContent();
        }

        //работает
        [HttpDelete("DeleteUsers")]
        public async Task<ActionResult<IEnumerable<User>>> DeleteUsers(int id)
        {
            var todoItem = await _context.Users.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Users.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExist(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private static User UserTO(User user) =>
            new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MiddleName = user.MiddleName,
                Role = user.Role,
                JobTitle = user.JobTitle,
                Department = user.Department
            };
    }
}