using System;
using DataBaseAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RestApiSocialFilm.UserAuthentication;

namespace RestApiSocialFilm.Controllers
{
    //All calls will get the url below: 
    [RoutePrefix("cinemano/user")]
    [RequireHttps]
    public class UserController : ApiController
    {


        //GET ALL REUQEST FOR USER
        [Route("")]
        [Authorize]
        public IEnumerable<Users> GetAllUsers()
        {
            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return entities.Users.ToList();
            }
        }


        //GET REUQEST FOR USER
        [Route("{id}")]
        public Users GetUserById(int id)
        {

            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return (entities.Users.FirstOrDefault(e => e.user_id == id));
            }
        }

        //POST REQUEST FOR USER. CREATES A NEW USER
        [Route("")]
        public IHttpActionResult PostUser([FromBody] Users user)
        {
           
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                entities.Users.Add(new Users()
                {
                        
                    Username = user.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                    Email = user.Email,

                }); ;

                entities.SaveChanges();
                Console.WriteLine("User was created with following information:" + user.Username + user.Password + user.Email);
            }

            return Ok();
        }

        //PUT REQUEST FOR USER. CHANGES USER
        [Route("")]
        public IHttpActionResult PutUser([FromBody] Users user)
        {

            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var entities = new yndlingsfilmDBEntities())
            {
                //DATABASE QUERY. CHECKS THE DATABASE FOR THE USER WITH THE GIVEN USERID. THEN IT CREATES A NEW USER VARIABLE THAT WE DECLARE ALL OF THE GIVEN VARIBLES FROM THE HTTP PUT REQUEST
                var existingUser = entities.Users
                    .FirstOrDefault(s => s.user_id == user.user_id);

                if (existingUser != null)
                {

                    existingUser.Username = user.Username;
                    existingUser.Email = user.Email;
                    existingUser.Password = user.Password;



                    entities.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }


        //DELETE REQUEST FOR USER
        [Route("{id}")]
        public IHttpActionResult DeleteUser(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");
            using (var entities = new yndlingsfilmDBEntities())
            {
                //DATABASE QUERY. CHECKS THE DATABASE FOR THE USER WITH THE GIVEN USERID. THEN IT CREATES A NEW USER VARIABLE THAT WE DECLARE ALL OF THE GIVEN VARIBLES FROM THE HTTP PUT REQUEST
                var existingUser = entities.Users
                    .FirstOrDefault(s => s.user_id == id);

                if (existingUser != null)
                {
                    entities.Entry(existingUser).State = System.Data.Entity.EntityState.Deleted;
                    entities.SaveChanges();
                }
                return Ok();
            }
        }
        [Route("flist/{id}")]
        public IEnumerable<Relationship> GetFriendlist(int id)
        {

            using (var entities = new yndlingsfilmDBEntities())
            {
                var FriendshipsList = entities.Relationship.Where(s => s.userone_id == id).ToList().AsEnumerable();


                return FriendshipsList;
            }

        }
       
        [Route("{Username}/{Password}")]
        [Authorize]
        public Boolean GetLoginUser(String Username, String Password)
        {

            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                var tempUser = entities.Users.FirstOrDefault(u => u.Username == Username);
               
                if (BCrypt.Net.BCrypt.Verify(Password, tempUser.Password))
                {
                    return true;
                }
                return false;
            }
        }

        //GET REUQEST FOR USER + GETS FRIENDSHIP
        [Route("username/{username}")]
        [Authorize]
        public Users GetUserById(String username)
        {

            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;
                return (entities.Users.FirstOrDefault(e => e.Username == username));
            }
        }
    }
}




