using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sponsorship.Application.SponsorshipRequests.Commands;
using Sponsorship.Application.SponsorshipRequests.Queries;

namespace Sponsorship.Api.Controllers
{
    /// <summary>
    /// Manages the lifecycle, approvals, and retrieval of sponsorship requests.
    /// </summary>
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SponsorshipRequestsController(IMediator mediator): ControllerBase
    {
        /// <summary>
        /// Creates a new draft sponsorship request.
        /// </summary>
        /// <param name="command">The details required to create the initial draft.</param>
        /// <response code="201">Draft created successfully.</response>
        /// <response code="400">Validation failed or invalid payload.</response>
        [HttpPost("draft")]
        public async Task<IActionResult> CreateDraft(CreateDraftCommand command)
        {
            var result = await mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Submits a draft sponsorship request into the workflow process.
        /// </summary>
        /// <param name="id">The unique identifier of the sponsorship request.</param>
        /// <response code="200">Request submitted successfully.</response>
        /// <response code="404">Sponsorship request not found.</response>
        /// <response code="400">Request is not in a valid state to be submitted.</response>
        [HttpPost("{id}/submit")]
        public async Task<IActionResult> Submit(Guid id)
        {
            var result =await mediator.Send(new SubmitRequestCommand(id));
            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Cancels an active or submitted sponsorship request.
        /// </summary>
        /// <param name="id">The unique identifier of the sponsorship request.</param>
        /// <response code="200">Request canceled successfully.</response>
        /// <response code="404">Sponsorship request not found.</response>
        /// <response code="400">Request cannot be canceled in its current state.</response>
        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> Cancel(
            Guid id)
        {
            var result =
                await mediator.Send(
                    new CancelRequestCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Approves the sponsorship request at the manager stage.
        /// </summary>
        /// <param name="id">The unique identifier of the sponsorship request.</param>
        /// <response code="200">Manager approval recorded successfully.</response>
        /// <response code="404">Sponsorship request not found.</response>
        /// <response code="403">User is not authorized to approve this request.</response>
        [HttpPost("{id}/manager-approve")]
        public async Task<IActionResult> ManagerApprove(
            Guid id)
        {
            var result =
                await mediator.Send(
                    new ManagerApproveCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Rejects the sponsorship request at the manager stage.
        /// </summary>
        /// <param name="id">The unique identifier of the sponsorship request.</param>
        /// <response code="200">Manager rejection recorded successfully.</response>
        /// <response code="404">Sponsorship request not found.</response>
        /// <response code="403">User is not authorized to reject this request.</response>
        [HttpPost("{id}/manager-reject")]
        public async Task<IActionResult> ManagerReject(
            Guid id)
        {
            var result =
                await mediator.Send(
                    new ManagerRejectCommand(id));

            return StatusCode(result.StatusCode, result); ;
        }

        /// <summary>
        /// Approves the sponsorship request at the finance stage.
        /// </summary>
        /// <param name="id">The unique identifier of the sponsorship request.</param>
        /// <response code="200">Finance approval recorded successfully.</response>
        /// <response code="404">Sponsorship request not found.</response>
        /// <response code="403">User is not authorized to approve this request.</response>
        [HttpPost("{id}/finance-approve")]
        public async Task<IActionResult> FinanceApprove(
            Guid id)
        {
            var result =
                await mediator.Send(
                    new FinanceApproveCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Rejects the sponsorship request at the finance stage.
        /// </summary>
        /// <param name="id">The unique identifier of the sponsorship request.</param>
        /// <response code="200">Finance rejection recorded successfully.</response>
        /// <response code="404">Sponsorship request not found.</response>
        /// <response code="403">User is not authorized to reject this request.</response>
        [HttpPost("{id}/finance-reject")]
        public async Task<IActionResult> FinanceReject(
            Guid id)
        {
            var result =
                await mediator.Send(
                    new FinanceRejectCommand(id));

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Retrieves all sponsorship requests created by the authenticated user.
        /// </summary>
        /// <response code="200">List of personal requests retrieved successfully.</response>
        /// <response code="401">User is unauthenticated.</response>
        [HttpGet("mine")]
        public async Task<IActionResult> GetMine()
        {
            var result =
                await mediator.Send(
                    new GetMyRequestsQuery());

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Retrieves a filtered list of sponsorship requests based on query parameters.
        /// </summary>
        /// <param name="query">The filtering, sorting, and pagination parameters.</param>
        /// <response code="200">Filtered list of requests retrieved successfully.</response>
        [HttpGet]
        public async Task<IActionResult> GetRequests(
            [FromQuery] GetRequestsQuery query)
        {
            var result =
                await mediator.Send(query);

            return StatusCode(result.StatusCode, result);
        }

        /// <summary>
        /// Retrieves the full workflow history and audit trail for a specific request.
        /// </summary>
        /// <param name="id">The unique identifier of the sponsorship request.</param>
        /// <response code="200">Workflow history log retrieved successfully.</response>
        /// <response code="404">Sponsorship request or history logs not found.</response>
        [HttpGet("{id}/history")]
        public async Task<IActionResult> GetHistory(
            Guid id)
        {
            var result =
                await mediator.Send(
                    new GetWorkflowHistoryByRequestIdQuery(id));

            return StatusCode(result.StatusCode, result);
        }
    }
}
