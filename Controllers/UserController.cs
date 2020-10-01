using System;
using DataBaseAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RestApiSocialFilm.Controllers
{
    //All calls will get the url below: 
    [RoutePrefix("cinemano/user")]
    public class UserController : ApiController
    {

        //GET ALL REUQEST FOR USER
        [Route("")]
        public IEnumerable<Users> GetAllUsers()
        {
            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                return entities.Users.ToList();
            }
        }


        //GET REUQEST FOR USER
        [Route("{id}")]
        public Users GetUserById(int id)
        {
            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                return entities.Users.FirstOrDefault(e => e.UserId == id);
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
                    Password = user.Password,
                    Email = user.Email,

                }); ;

                entities.SaveChanges();
                Console.WriteLine("User was created with following information:" +  user.Username + user.Password + user.Email);
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
                    .FirstOrDefault(s => s.UserId == user.UserId);

                if (existingUser != null)
                {
                   
                   
                    existingUser.Email = user.Email;
                    existingUser.MovieId = user.MovieId;
                    existingUser.Movies = user.Movies;
                    existingUser.Password = user.Password;
                    existingUser.Rated = user.Rated;
                    existingUser.RatedId = user.RatedId;
                    existingUser.Relationship = user.Relationship;
                    existingUser.Reviews = user.Reviews;

                    entities.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
    }
    }


}
