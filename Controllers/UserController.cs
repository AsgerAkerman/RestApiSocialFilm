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


        
    }

}
