using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using documentation;
using documentation.Models;
using System.Data;


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
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return BadRequest();
        }

        //вывод всех пользователей, работает
        [HttpGet("allUsers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> allUsers()
        {
            return await _context.Users
                .Select(x => UserTO(x))
                .ToListAsync();
        }

        //работает
        [HttpGet("oneUser")]
        public async Task<ActionResult<UserDTO>> oneUser(long Id)
        {
            var user = await _context.Users.FindAsync(Id);

            if (user == null)
            {
                return NotFound();
            }
            
            return UserTO(user);
        }

        [HttpPost("createUser")]
        public async Task<ActionResult<UserDTO>> createUser(UserDTO UserDTO)
        {
            var user = new User
            {
                FirstName = UserDTO.FirstName,
                LastName = UserDTO.LastName,
                MiddleName = UserDTO.MiddleName,
                Role = UserDTO.Role,
                JobTitle = UserDTO.JobTitle,
                Department = UserDTO.Department
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction(
                nameof(GetUsers),
                new { id = user.Id },
                UserTO(user));
        }
        //работает
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UserDTO>> UpdateUser(int id, User userDTO)
        {
            if (id != userDTO.Id)
            {
                return BadRequest();
            }

            var user= await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = userDTO.FirstName;
            user.LastName = userDTO.LastName;
            user.MiddleName = userDTO.MiddleName;
            user.Role = userDTO.Role;
            user.JobTitle = userDTO.JobTitle;
            user.Department = userDTO.Department;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!UserExist(id))
            {
                return NotFound();
            }

            return Ok("Обновлено");
        }

        //работает
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("Удалено");
        }

        private bool UserExist(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private static UserDTO UserTO(User user) =>
            new UserDTO
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
