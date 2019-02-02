using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;

namespace WebApiODataServiceTest.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApiODataServiceTest;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<MyTable>("MyTable");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MyTablesController : ODataController
    {
        private NWINDEntities db = new NWINDEntities();

        // GET: odata/MyTables
        [EnableQuery]
        public IQueryable<MyTable> GetMyTables()
        {
            return db.MyTables;
        }

        // GET: odata/MyTables(5)
        [EnableQuery]
        public SingleResult<MyTable> GetMyTable([FromODataUri] int key)
        {
            return SingleResult.Create(db.MyTables.Where(myTable => myTable.Id == key));
        }

        // PUT: odata/MyTables(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<MyTable> patch)
        {
        //    Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MyTable myTable = db.MyTables.Find(key);
            if (myTable == null)
            {
                return NotFound();
            }

            patch.Put(myTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(myTable);
        }

        // POST: odata/MyTables
        public IHttpActionResult Post(MyTable myTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MyTables.Add(myTable);
            db.SaveChanges();

            return Created(myTable);
        }

        // PATCH: odata/MyTables(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<MyTable> patch)
        {
           // Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MyTable myTable = db.MyTables.Find(key);
            if (myTable == null)
            {
                return NotFound();
            }

            patch.Patch(myTable);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyTableExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(myTable);
        }

        // DELETE: odata/MyTables(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            MyTable myTable = db.MyTables.Find(key);
            if (myTable == null)
            {
                return NotFound();
            }

            db.MyTables.Remove(myTable);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MyTableExists(int key)
        {
            return db.MyTables.Count(e => e.Id == key) > 0;
        }
    }
}
