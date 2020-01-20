using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;

namespace BatchDecimalRepro
{
    public class ExamplesController : ODataController
    {

        public ExamplesController()
        {
        }

        [EnableQuery]
        public IQueryable<Example> Get()
        {
            return new Example[] { new Example() { Id = 1, Price = 1 } }.AsQueryable();
        }

        [EnableQuery]
        public async Task<IActionResult> Patch([FromODataUri] int key, Delta<Example> deltaObject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

    }
}
