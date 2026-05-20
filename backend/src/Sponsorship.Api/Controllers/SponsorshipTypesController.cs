using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sponsorship.Application.SponsorshipTypes.Commands;
using Sponsorship.Application.SponsorshipTypes.DTOs;
using Sponsorship.Application.SponsorshipTypes.Queries;
using System.Windows.Input;

namespace Sponsorship.Api.Controllers
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SponsorshipTypesController(ISender mediator) : ControllerBase
    {

        /// <summary>
        /// Retrieves all available sponsorship types.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token context.</param>
        /// <returns>A list of sponsorship types.</returns>
        /// <response code="200">Successfully retrieved the list.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IReadOnlyList<SponsorshipTypeDto>))]
        public async Task<ActionResult<IReadOnlyList<SponsorshipTypeDto>>> GetAll(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetAllSponsorshipTypesQuery(), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Retrieves a specific sponsorship type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique GUID of the sponsorship type.</param>
        /// <param name="cancellationToken">Cancellation token context.</param>
        /// <returns>The requested sponsorship type details.</returns>
        /// <response code="200">Found the sponsorship type matching the ID.</response>
        /// <response code="404">No sponsorship type was found with the specified ID.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SponsorshipTypeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SponsorshipTypeDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetSponsorshipTypeByIdQuery(id), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Creates a new sponsorship type record.
        /// </summary>
        /// <param name="command">The execution data containing the type Name.</param>
        /// <param name="cancellationToken">Cancellation token context.</param>
        /// <returns>The newly created GUID tracking identifier.</returns>
        /// <response code="201">Successfully created the resource.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Guid))]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateSponsorshipTypeCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Modifies an existing sponsorship type record.
        /// </summary>
        /// <param name="id">The explicit target GUID to update.</param>
        /// <param name="command">The modified state payloads.</param>
        /// <param name="cancellationToken">Cancellation token context.</param>
        /// <response code="204">The record was updated successfully.</response>
        /// <response code="400">The route ID mismatch with the command body payload ID.</response>
        /// <response code="404">No target record exists to update.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSponsorshipTypeCommand command, CancellationToken cancellationToken)
        {
            command.Id = id; // Ensure the command's ID is set to the route parameter for consistency

            var result = await mediator.Send(command, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Removes an existing sponsorship type permanently.
        /// </summary>
        /// <param name="id">The targeting GUID to remove.</param>
        /// <param name="cancellationToken">Cancellation token context.</param>
        /// <response code="204">The record was deleted successfully.</response>
        /// <response code="404">No target record exists to delete.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteSponsorshipTypeCommand(id), cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
