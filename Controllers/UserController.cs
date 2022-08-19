using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frir.Datacontext;
using Microsoft.AspNetCore.Mvc;
using Frir.Models;
namespace Frir.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (var db = new UserDataContext())
            {
                foreach(var item in db.Users)
                {
                    users.Add(item);
                }
            }
            return users;
        }
        [HttpPost]
        public void AddUser([FromForm] User _user)
        {
            using(var db = new UserDataContext())
            {
                db.Users.Add(_user);
                db.SaveChanges();
            }
        }
        [HttpDelete]
        public void DeleteUserByID(int id)
        {
            using(var db = new UserDataContext())
            {
                var item = db.Users.FirstOrDefault(e => e.ID == id);
                if ( item != null)
                {
                    db.Remove(item);
                    db.SaveChanges();
                }
            }
        }
        [HttpPut]
        public void UpdateUserByID([FromBody] User _user)
        {
            int id = _user.ID;
            using (var db = new UserDataContext())
            {
                var item = db.Users.FirstOrDefault(e => e.ID == id);
                if(item != null)
                {
                    item.ID = id;
                    item.Username = _user.Username;
                    item.Email = _user.Email;
                    item.Password = _user.Password;
                    db.Update(item);
                    db.SaveChanges();
                }
                
            }
        }
        [HttpGet]
        public User GetUserByID(int id)
        {
            using(var db = new UserDataContext())
            {
                var item = db.Users.FirstOrDefault(e=>e.ID == id);
                if (item != null)
                {
                    return item;
                }
                return null;
            }
        }
    }
}