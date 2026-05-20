using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.DTOs
{
    public class WorkflowHistoryDto()
    {
        public Guid Id { get; set; }
        public Guid SponsorshipRequestId { get; set; }
        public string? PreviousStatus { get; set; }
        public string? NewStatus { get; set; }
        public string? Action { get; set; }
        public required string PerformedByUserId { get; set; }
        public string? PerformedByUserName { get; set; }
        public string? Remarks { get; set; }
        public DateTime PerformedAtUtc { get; set; }
    };
}
