using DataBaseAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RestApiSocialFilm.Controllers
{
    [RoutePrefix("Users")]
    public class UserController : ApiController
    {

        public IEnumerable<Users> GetAllUsers()
        {
            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                return entities.Users.ToList();
            }
        }

        public Users GetUserById(int id)
        {
            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                return entities.Users.FirstOrDefault(e => e.UserId == id);
            }
        }
        //POST REQUEST FOR USER

        public IHttpActionResult CreateUser([FromBody] Users user)
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
            }

            return Ok();
        }

    }
}
