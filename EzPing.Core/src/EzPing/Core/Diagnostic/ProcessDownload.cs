namespace EzPing.Core.Diagnostic
{
    using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
    using System;
    using System.Runtime.CompilerServices;

    public delegate void ProcessDownload(TcpIpTraceData data);
}

