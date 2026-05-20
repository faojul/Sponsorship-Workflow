using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Application.SponsorshipRequests.DTOs
{
    public record WorkflowHistoryDto(
    Guid Id,
    Guid SponsorshipRequestId,
    string PreviousStatus,
    string NewStatus,
    string Action,
    string PerformedByUserId,
    string? Remarks,
    DateTime PerformedAtUtc
);
}
