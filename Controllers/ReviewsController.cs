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

namespace RestApiSocialFilm.Controllers
    {
    //All calls will get the url below:     
    [RoutePrefix("cinemano/reviews")]
    public class ReviewsController : ApiController
    {

        private yndlingsfilmDBEntities db = new yndlingsfilmDBEntities();

        [Route("")]
        public IQueryable<Reviews> GetReviews()
        {
            return db.Reviews;
        }

       
        [ResponseType(typeof(Reviews))]
        [Route("{id}")]
        public IHttpActionResult GetReviews(int id)
        {
            Reviews reviews = db.Reviews.Find(id);
            if (reviews == null)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        // PUT: api/Reviews/5
        [ResponseType(typeof(void))]
        [Route("{id}")]
        public IHttpActionResult PutReviews(int id, Reviews reviews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reviews.review_id)
            {
                return BadRequest();
            }

            db.Entry(reviews).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewsExists(id))
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

        // POST: api/Reviews
        [ResponseType(typeof(Reviews))]
        [Route("")]
        public IHttpActionResult PostReviews(Reviews reviews)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Reviews.Add(reviews);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = reviews.review_id }, reviews);
        }

        
        [ResponseType(typeof(Reviews))]
        [Route("{id}")]
        public IHttpActionResult DeleteReviews(int id)
        {
            Reviews reviews = db.Reviews.Find(id);
            if (reviews == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(reviews);
            db.SaveChanges();

            return Ok(reviews);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ReviewsExists(int id)
        {
            return db.Reviews.Count(e => e.review_id == id) > 0;
        }
    }
}