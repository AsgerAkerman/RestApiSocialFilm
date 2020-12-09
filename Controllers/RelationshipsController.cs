using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataBaseAccess;
using Microsoft.Ajax.Utilities;

namespace RestApiSocialFilm.Controllers
{
    [RoutePrefix("cinemano/relationship")]
    public class RelationshipsController : ApiController
    {
        private yndlingsfilmDBEntities db = new yndlingsfilmDBEntities();



        [Route("")]
        public IQueryable<Relationship> GetRelationship()
        {
            return db.Relationship;
        }


        [Route("{id}")]
        [ResponseType(typeof(Relationship))]
        public IHttpActionResult GetRelationship(int id)
        {
            Relationship relationship = db.Relationship.Find(id);
            if (relationship == null)
            {
                return NotFound();
            }

            return Ok(relationship);
        }

        [Route("")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRelationship(int id, Relationship relationship)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != relationship.userOneId)
            {
                return BadRequest();
            }

            db.Entry(relationship).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationshipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        //POST REQUEST FOR RELATIONSHIP. CREATES RELATIONSHIP
        [Route("")]
        [ResponseType(typeof(Relationship))]
        public IHttpActionResult PostRelationship(Relationship relationship)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            using (yndlingsfilmDBEntities entities = new yndlingsfilmDBEntities())
            {
                entities.Relationship.Add(new Relationship()
                {
                    userOneId = relationship.userOneId,
                    userTwoId = relationship.userTwoId


                }); ;

                entities.SaveChanges();

            }

            return Ok();
        }

        [Route("")]
        [ResponseType(typeof(Relationship))]
        public IHttpActionResult DeleteRelationship(int id)
        {
            Relationship relationship = db.Relationship.Find(id);
            if (relationship == null)
            {
                return NotFound();
            }

            db.Relationship.Remove(relationship);
            db.SaveChanges();

            return Ok(relationship);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RelationshipExists(int id)
        {
            return db.Relationship.Count(e => e.userOneId == id) > 0;
        }


        [Route("flist/{id}")]
        public IEnumerable<Users> GetFriendlist(int id)
        {
            UserController uC = new UserController();
            List<Users> UserList = new List<Users>();

            using (var entities = new yndlingsfilmDBEntities())
            {
                entities.Configuration.LazyLoadingEnabled = false;

                var FriendshipsList = from s in entities.Relationship where s.userOneId == id || s.userTwoId == id select s;

                foreach (var relationship in FriendshipsList.ToList())
                {
                    if (relationship.userOneId != id)
                    {
                        int idd = relationship.userOneId;
                        Users user = uC.GetUserById(relationship.userOneId);
                        Console.WriteLine(user.UserId);
                        UserList.Add(user);
                    }
                    if(relationship.userTwoId != id)
                    {
                        Users user = uC.GetUserById(relationship.userTwoId);
                        Console.WriteLine(user.UserId);
                        UserList.Add(user);

                    }
                    

                }
                return UserList;
            }


        }

    }
}

