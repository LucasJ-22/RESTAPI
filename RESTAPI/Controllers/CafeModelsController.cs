using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTAPI.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CafeModelsController : ControllerBase
    {
        private readonly RestContext _context;

        public CafeModelsController(RestContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets created datas. 
        /// </summary>
        /// <returns>All created datas</returns>
        /// <remarks>
        ///     Sample request:     
        ///     
        ///     {
        ///         "id": 1,
        ///         "name": "string",
        ///         "description": "string"
        ///     }...
        /// 
        /// </remarks>
        /// <response code="200">Returns the all created itens</response>
        /// <response code="400">If the item is null</response>
        // GET: api/CafeModels

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CafeModel>>> GetCafeItems()
        {
          if (_context.CafeItems == null)
          {
              return NotFound();
          }
            return await _context.CafeItems.ToListAsync();
        }

        /// <summary>
        /// Gets datas by id. 
        /// </summary>
        /// <returns>Data by id</returns>
        /// <remarks>
        ///     Sample request: 
        ///     
        ///     id = 1    
        ///     
        ///     {
        ///         "id": 1,
        ///         "name": "string",
        ///         "description": "string"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Returns data by id</response>
        /// <response code="404">If data doesn exists</response>
        // GET: api/CafeModels/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CafeModel>> GetCafeModel(long id)
        {
          if (_context.CafeItems == null)
          {
              return NotFound();
          }
            var cafeModel = await _context.CafeItems.FindAsync(id);

            if (cafeModel == null)
            {
                return NotFound();
            }

            return cafeModel;
        }

        /// <summary>
        ///     Update data by id. 
        /// </summary>
        /// <returns>Updated data</returns>
        /// <remarks>
        ///     Sample request: 
        ///     
        ///     id = 1    
        ///     
        ///     {
        ///         "id": 1,
        ///         "name": "updated string",
        ///         "description": "updated string"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Success</response>
        /// <response code="204">Update data and return server responses</response>
        /// <response code="400">If data doesn exists, this will return bad request</response>
        // PUT: api/CafeModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCafeModel(long id, CafeModel cafeModel)
        {
            if (id != cafeModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(cafeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CafeModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        ///     Post data. 
        /// </summary>
        /// <returns>Data by id</returns>
        /// <remarks>
        ///     Sample request: 
        ///     
        ///     id = 1    
        ///     
        ///     {
        ///         "id": 1,
        ///         "name": "string",
        ///         "description": "string"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Post data success</response>
        /// <response code="400">If the item is null</response>
        // POST: api/CafeModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CafeModel>> PostCafeModel(CafeModel cafeModel)
        {
          if (_context.CafeItems == null)
          {
              return Problem("Entity set 'RestContext.CafeItems'  is null.");
          }
            _context.CafeItems.Add(cafeModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCafeModel), new { id = cafeModel.Id }, cafeModel);            
        }

        /// <summary>
        ///     Delete data. 
        /// </summary>
        /// <returns>Data by id</returns>
        /// <response code="204">Delete data and return server responses</response>
        /// <response code="404">If data doesn exists</response>
        // DELETE: api/CafeModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafeModel(long id)
        {
            if (_context.CafeItems == null)
            {
                return NotFound();
            }
            var cafeModel = await _context.CafeItems.FindAsync(id);
            if (cafeModel == null)
            {
                return NotFound();
            }

            _context.CafeItems.Remove(cafeModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        /// <summary>
        ///     Verify if model exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Return model informations</returns>
        private bool CafeModelExists(long id)
        {
            return (_context.CafeItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
