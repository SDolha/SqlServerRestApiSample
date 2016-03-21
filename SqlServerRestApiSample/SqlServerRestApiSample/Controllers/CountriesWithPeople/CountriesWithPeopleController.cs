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
using SqlServerRestApiSample.Models.CountriesWithPeople;

namespace SqlServerRestApiSample.Controllers.CountriesWithPeople
{
    public class CountriesWithPeopleController : ApiController
    {
        private CountriesWithPeopleSampleDatabaseEntities db = new CountriesWithPeopleSampleDatabaseEntities();

        // GET: api/CountriesWithPeople
        public IQueryable<CountryWithPeople> GetCountryWithPeoples()
        {
            return db.CountryWithPeoples;
        }

        // GET: api/CountriesWithPeople/5
        [ResponseType(typeof(CountryWithPeople))]
        public IHttpActionResult GetCountryWithPeople(int id)
        {
            CountryWithPeople countryWithPeople = db.CountryWithPeoples.Find(id);
            if (countryWithPeople == null)
            {
                return NotFound();
            }

            return Ok(countryWithPeople);
        }

        // PUT: api/CountriesWithPeople/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCountryWithPeople(int id, CountryWithPeople countryWithPeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != countryWithPeople.Id)
            {
                return BadRequest();
            }

            db.Entry(countryWithPeople).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryWithPeopleExists(id))
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

        // POST: api/CountriesWithPeople
        [ResponseType(typeof(CountryWithPeople))]
        public IHttpActionResult PostCountryWithPeople(CountryWithPeople countryWithPeople)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CountryWithPeoples.Add(countryWithPeople);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = countryWithPeople.Id }, countryWithPeople);
        }

        // DELETE: api/CountriesWithPeople/5
        [ResponseType(typeof(CountryWithPeople))]
        public IHttpActionResult DeleteCountryWithPeople(int id)
        {
            CountryWithPeople countryWithPeople = db.CountryWithPeoples.Find(id);
            if (countryWithPeople == null)
            {
                return NotFound();
            }

            db.CountryWithPeoples.Remove(countryWithPeople);
            db.SaveChanges();

            return Ok(countryWithPeople);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryWithPeopleExists(int id)
        {
            return db.CountryWithPeoples.Count(e => e.Id == id) > 0;
        }
    }
}