using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB;
using ShoppingWEB.Extension_Method;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers
{
    [Route("api/image")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ImageUrlController : ControllerBase
    {
        private readonly ShoppingContext _context;

        public ImageUrlController(ShoppingContext context)
        {
            _context = context;
        }


        // DELETE: api/ImageUrlController/5
        [HttpDelete]
        public async Task<IActionResult> DeleteImageUrl([FromQuery] string id)
        {
            var imageUrl = _context.ImageUrls.FirstOrDefault(x => x.ImagePath == id);
            if (imageUrl == null)
            {
                return NotFound();
            }

            imageUrl.ImagePath.DeleteFile();
            _context.ImageUrls.Remove(imageUrl);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageUrlExists(string id)
        {
            return (_context.ImageUrls?.Any(e => e.ImagePath == id)).GetValueOrDefault();
        }
    }
}