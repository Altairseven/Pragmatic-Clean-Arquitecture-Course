using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Outbox;
public sealed class OutboxMessage {

    public OutboxMessage(Guid id, DateTime occurredOnUtc, string type, string content) {
        Id = id;
        OccurredOnUtc = occurredOnUtc;
        Type = type;
        Content = content;
    }

    public Guid Id { get; private set; }
    public DateTime OccurredOnUtc { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public string Content { get; private set; } = string.Empty;
    public DateTime? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

}
