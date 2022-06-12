namespace EzPing.Core.Diagnostic
{
    using Microsoft.Diagnostics.Tracing.Parsers;
    using Microsoft.Diagnostics.Tracing.Session;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [NullableContext(2), Nullable((byte) 0)]
    public class ProcessObserver
    {
        [Nullable((byte) 1)]
        private TraceEventSession _etwSession = new TraceEventSession("MyKernelAndClrEventsSession", TraceEventSessionOptions.Create);
        [Nullable((byte) 1)]
        private Thread _observerThread;
        [Nullable((byte) 1)]
        private Thread _traceThread;

        public ProcessObserver()
        {
            this._observerThread = new Thread(new ThreadStart(this.Oserver));
            this._traceThread = new Thread(new ThreadStart(this.Trace));
        }

        private void Oserver()
        {
            Dictionary<int, Process> dictionary = new Dictionary<int, Process>();
            while (true)
            {
                HashSet<int> set = dictionary.Keys.ToHashSet<int>();
                Process[] processes = Process.GetProcesses();
                int index = 0;
                while (true)
                {
                    if (index >= processes.Length)
                    {
                        foreach (int num2 in set)
                        {
                            EzPing.Core.Diagnostic.ProcessClosed processClosed = this.ProcessClosed;
                            if (processClosed == null)
                            {
                                EzPing.Core.Diagnostic.ProcessClosed local2 = processClosed;
                                continue;
                            }
                            processClosed(dictionary[num2]);
                        }
                        Thread.Sleep(this.Tick);
                        break;
                    }
                    Process process = processes[index];
                    set.Remove(process.Id);
                    if (!dictionary.ContainsKey(process.Id))
                    {
                        dictionary.Add(process.Id, process);
                        EzPing.Core.Diagnostic.ProcessOpened processOpened = this.ProcessOpened;
                        if (processOpened == null)
                        {
                            EzPing.Core.Diagnostic.ProcessOpened local1 = processOpened;
                        }
                        else
                        {
                            processOpened(process);
                        }
                        this._etwSession.Source.Kernel.TcpIpRecv += delegate (TcpIpTraceData data) {
                            EzPing.Core.Diagnostic.ProcessDownload processDownload = this.ProcessDownload;
                            if (processDownload == null)
                            {
                                EzPing.Core.Diagnostic.ProcessDownload local1 = processDownload;
                            }
                            else
                            {
                                processDownload(data);
                            }
                        };
                        this._etwSession.Source.Kernel.TcpIpSend += delegate (TcpIpSendTraceData data) {
                            EzPing.Core.Diagnostic.ProcessUpload processUpload = this.ProcessUpload;
                            if (processUpload == null)
                            {
                                EzPing.Core.Diagnostic.ProcessUpload local1 = processUpload;
                            }
                            else
                            {
                                processUpload(data);
                            }
                        };
                        this._etwSession.Source.Kernel.TcpIpConnect += delegate (TcpIpConnectTraceData data) {
                            EzPing.Core.Diagnostic.ProcessConnect processConnect = this.ProcessConnect;
                            if (processConnect == null)
                            {
                                EzPing.Core.Diagnostic.ProcessConnect local1 = processConnect;
                            }
                            else
                            {
                                processConnect(data);
                            }
                        };
                        this._etwSession.Source.Kernel.TcpIpDisconnect += delegate (TcpIpTraceData data) {
                            EzPing.Core.Diagnostic.ProcessDisconnect processDisconnect = this.ProcessDisconnect;
                            if (processDisconnect == null)
                            {
                                EzPing.Core.Diagnostic.ProcessDisconnect local1 = processDisconnect;
                            }
                            else
                            {
                                processDisconnect(data);
                            }
                        };
                    }
                    index++;
                }
            }
        }

        public void Start()
        {
            if (!this.IsActive)
            {
                this._etwSession.EnableKernelProvider(KernelTraceEventParser.Keywords.NetworkTCPIP, KernelTraceEventParser.Keywords.None);
                this._traceThread.Start();
                Thread.Sleep(100);
                this._observerThread.Start();
            }
        }

        public void Stop()
        {
            if (this.IsActive)
            {
                this._etwSession.Source.StopProcessing();
                this._observerThread.Abort();
                this._traceThread.Abort();
                this._etwSession = new TraceEventSession("MyKernelAndClrEventsSession", TraceEventSessionOptions.Create);
                this._observerThread = new Thread(new ThreadStart(this.Oserver));
                this._traceThread = new Thread(new ThreadStart(this.Trace));
            }
        }

        private void Trace()
        {
            this._etwSession.Source.Process();
        }

        public bool IsActive =>
            this._etwSession.IsActive;

        public int Tick { get; set; }

        public EzPing.Core.Diagnostic.ProcessOpened ProcessOpened { get; set; }

        public EzPing.Core.Diagnostic.ProcessClosed ProcessClosed { get; set; }

        public EzPing.Core.Diagnostic.ProcessDownload ProcessDownload { get; set; }

        public EzPing.Core.Diagnostic.ProcessUpload ProcessUpload { get; set; }

        public EzPing.Core.Diagnostic.ProcessConnect ProcessConnect { get; set; }

        public EzPing.Core.Diagnostic.ProcessDisconnect ProcessDisconnect { get; set; }
    }
}

