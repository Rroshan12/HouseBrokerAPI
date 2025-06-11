using HouseBroker.Domain.Models;
using HouseBroker.Infra.Dtos;
using HouseBroker.Infra.Dtos.FilterDtos;
using HouseBroker.Infra.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseBroker.Api.Controllers
{

    public class PropertyListingController : BaseApiController
    {
        private readonly IPropertyListingService _propertyListingService;

        public PropertyListingController(IPropertyListingService propertyListingService)
        {
            _propertyListingService = propertyListingService;
        }

        // GET: api/PropertyListings
        [HttpGet]
        [Route("GetAllProperty")]
        [Authorize(Policy = "SeekerOrBroker")]
        public async Task<IActionResult> GetAll([FromQuery]PropertyListingFilter filter)
        {
            var listings = await _propertyListingService.GetAllListingsAsync(filter);
            return Ok(listings);
        }

        // GET: api/PropertyListings/{id}
        [HttpGet]
        [Route("GetAllPropertyById")]
        [Authorize(Policy = "SeekerOrBroker")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var listing = await _propertyListingService.GetListingByIdAsync(id);
            if (listing == null)
                return NotFound();

            return Ok(listing);
        }

        // POST: api/PropertyListings
        [HttpPost]
        [Route("CreateProperty")]
        [Authorize(Policy = "BrokerOnly")]
        public async Task<IActionResult> Create([FromBody] PropertyListingDtos dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _propertyListingService.AddListingAsync(dto);
            if (created == null)
                return StatusCode(500, "An error occurred while creating the listing.");

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/PropertyListings/{id}
        [HttpPut]
        [Route("UpdateProperty")]
        [Authorize(Policy = "BrokerOnly")]
        public async Task<IActionResult> Update( [FromBody] PropertyListingDtos dto)
        {

            var updated = await _propertyListingService.UpdateListingAsync(dto);
            return Ok(updated);
        }

        // DELETE: api/PropertyListings/{id}
        [HttpDelete]
        [Route("DeleteProperty")]
        [Authorize(Policy = "BrokerOnly")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var success = await _propertyListingService.DeleteListingAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
