using Sponsorship.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sponsorship.Domain.Entities
{
    public class SponsorshipType : BaseEntity
    {
        public string Name { get; set; } = default!;
    }
}
