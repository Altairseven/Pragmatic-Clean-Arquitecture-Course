using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Outbox;
public class OutboxOptions {

    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }

}
