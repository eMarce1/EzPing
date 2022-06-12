namespace EzPing.Core.Shell
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    [NullableContext(1), Nullable((byte) 0)]
    public class ShellManager
    {
        private const bool Hidden = true;
        private static readonly string WindowsPath = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        private const string readBreaKeyword = "TerminalBreakEzPing";
        private readonly Process _process;

        public ShellManager() : this(ShellType.Commandline)
        {
        }

        public ShellManager(ShellType type)
        {
            string str;
            this.<Type>k__BackingField = type;
            this._process = new Process();
            ProcessStartInfo startInfo = this._process.StartInfo;
            if (type == ShellType.Commandline)
            {
                str = "cmd.exe";
            }
            else
            {
                if (type != ShellType.Powershell)
                {
                    throw new ArgumentOutOfRangeException("type");
                }
                str = Path.Combine(WindowsPath, @"System32\WindowsPowerShell\v1.0\powershell.exe");
            }
            startInfo.FileName = str;
            this._process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            this._process.StartInfo.CreateNoWindow = true;
            this._process.StartInfo.RedirectStandardInput = true;
            this._process.StartInfo.RedirectStandardOutput = true;
            this._process.StartInfo.UseShellExecute = false;
        }

        public static void Execute(params string[] commands)
        {
            Execute(ShellType.Commandline, commands);
        }

        public static void Execute(ShellType shellType, params string[] commands)
        {
            ExecuteResponse(shellType, commands);
        }

        public static void Execute(CultureInfo culture, params string[] commands)
        {
            ExecuteResponse(culture, commands);
        }

        public static Task ExecuteAsync(params string[] commands) => 
            ExecuteAsync(ShellType.Commandline, commands);

        [AsyncStateMachine(typeof(<ExecuteAsync>d__27))]
        public static Task ExecuteAsync(ShellType shellType, params string[] commands)
        {
            <ExecuteAsync>d__27 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.shellType = shellType;
            d__.commands = commands;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteAsync>d__27>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<ExecuteAsync>d__32))]
        public static Task ExecuteAsync(CultureInfo culture, params string[] commands)
        {
            <ExecuteAsync>d__32 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder.Create();
            d__.culture = culture;
            d__.commands = commands;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteAsync>d__32>(ref d__);
            return d__.<>t__builder.Task;
        }

        [return: Nullable((byte) 2)]
        public static string ExecuteResponse(params string[] commands) => 
            ExecuteResponse(ShellType.Commandline, commands);

        [return: Nullable((byte) 2)]
        public static string ExecuteResponse(ShellType shellType, params string[] commands)
        {
            ShellManager manager = new ShellManager(shellType);
            manager.Start();
            foreach (string str2 in commands)
            {
                manager.WriteLine(str2);
            }
            manager.WriteLine("TerminalBreakEzPing");
            string str = manager.ReadUntil("TerminalBreakEzPing");
            manager.Kill();
            return str;
        }

        [return: Nullable((byte) 2)]
        public static string ExecuteResponse(CultureInfo culture, params string[] commands)
        {
            ShellManager manager = new ShellManager(ShellType.Powershell);
            manager.Start();
            manager.WriteLine($"[Threading.Thread]::CurrentThread.CurrentUICulture = '{culture}';");
            foreach (string str2 in commands)
            {
                manager.WriteLine(str2);
                manager._process.StandardInput.Flush();
            }
            manager.WriteLine("TerminalBreakEzPing");
            string str = manager.ReadUntil("TerminalBreakEzPing");
            manager.Kill();
            return str;
        }

        [return: Nullable(new byte[] { 1, 2 })]
        public static Task<string> ExecuteResponseAsync(params string[] commands) => 
            ExecuteResponseAsync(ShellType.Commandline, commands);

        [return: Nullable(new byte[] { 1, 2 })]
        [AsyncStateMachine(typeof(<ExecuteResponseAsync>d__29))]
        public static Task<string> ExecuteResponseAsync(ShellType shellType, params string[] commands)
        {
            <ExecuteResponseAsync>d__29 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
            d__.shellType = shellType;
            d__.commands = commands;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteResponseAsync>d__29>(ref d__);
            return d__.<>t__builder.Task;
        }

        [return: Nullable(new byte[] { 1, 2 })]
        [AsyncStateMachine(typeof(<ExecuteResponseAsync>d__33))]
        public static Task<string> ExecuteResponseAsync(CultureInfo culture, params string[] commands)
        {
            <ExecuteResponseAsync>d__33 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
            d__.culture = culture;
            d__.commands = commands;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ExecuteResponseAsync>d__33>(ref d__);
            return d__.<>t__builder.Task;
        }

        public void Kill()
        {
            this._process.Kill();
        }

        [NullableContext(2)]
        public string ReadLine() => 
            this._process.StandardOutput.ReadLine();

        [return: Nullable(new byte[] { 1, 2 })]
        public Task<string> ReadLineAsync() => 
            this._process.StandardOutput.ReadLineAsync();

        public string ReadToEnd()
        {
            StringBuilder builder = new StringBuilder();
            while (this._process.StandardOutput.Peek() > -1)
            {
                builder.AppendLine(this.ReadLine());
            }
            return builder.ToString();
        }

        [return: Nullable(new byte[] { 1, 2 })]
        [AsyncStateMachine(typeof(<ReadToEndAsync>d__18))]
        public Task<string> ReadToEndAsync()
        {
            <ReadToEndAsync>d__18 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<string>.Create();
            d__.<>4__this = this;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<ReadToEndAsync>d__18>(ref d__);
            return d__.<>t__builder.Task;
        }

        public string ReadUntil(string untilWord)
        {
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                string str = this.ReadLine();
                if (str != null)
                {
                    builder.AppendLine(str);
                    if (str.Contains(untilWord))
                    {
                        return builder.ToString();
                    }
                }
            }
        }

        public void Start()
        {
            this._process.Start();
        }

        public void WaitForExit()
        {
            this._process.WaitForExit();
        }

        public void WaitForExit(int milliseconds)
        {
            this._process.WaitForExit(milliseconds);
        }

        public void Write(string command)
        {
            this._process.StandardInput.Write(command);
        }

        public Task WriteAsync(string command) => 
            this._process.StandardInput.WriteAsync(command);

        public void WriteLine(string command)
        {
            this._process.StandardInput.WriteLine(command);
        }

        public Task WriteLineAsync(string command) => 
            this._process.StandardInput.WriteLineAsync(command);

        public ShellType Type { get; }

        [CompilerGenerated]
        private struct <ExecuteAsync>d__27 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            public ShellType shellType;
            [Nullable(new byte[] { 0, 1 })]
            public string[] commands;
            [Nullable(new byte[] { 0, 2 })]
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<string> awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = ShellManager.ExecuteResponseAsync(this.shellType, this.commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, ShellManager.<ExecuteAsync>d__27>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine([Nullable((byte) 0)] IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteAsync>d__32 : IAsyncStateMachine
        {
            public int <>1__state;
            public AsyncTaskMethodBuilder <>t__builder;
            [Nullable((byte) 0)]
            public CultureInfo culture;
            [Nullable(new byte[] { 0, 1 })]
            public string[] commands;
            [Nullable(new byte[] { 0, 2 })]
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    TaskAwaiter<string> awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        awaiter = ShellManager.ExecuteResponseAsync(this.culture, this.commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, ShellManager.<ExecuteAsync>d__32>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    awaiter.GetResult();
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine([Nullable((byte) 0)] IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteResponseAsync>d__29 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<string> <>t__builder;
            public ShellType shellType;
            [Nullable(new byte[] { 0, 1 })]
            public string[] commands;
            [Nullable((byte) 0)]
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    string str;
                    TaskAwaiter<string> awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        ShellType shellType = this.shellType;
                        string[] commands = this.commands;
                        awaiter = Task.Run<string>(new Func<string>(class_1.<ExecuteResponseAsync>b__0)).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, ShellManager.<ExecuteResponseAsync>d__29>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    str = awaiter.GetResult();
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(str);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine([Nullable((byte) 0)] IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ExecuteResponseAsync>d__33 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<string> <>t__builder;
            [Nullable((byte) 0)]
            public CultureInfo culture;
            [Nullable(new byte[] { 0, 1 })]
            public string[] commands;
            [Nullable((byte) 0)]
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    string str;
                    TaskAwaiter<string> awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                        goto TR_0004;
                    }
                    else
                    {
                        CultureInfo culture = this.culture;
                        string[] commands = this.commands;
                        awaiter = Task.Run<string>(new Func<string>(class_1.<ExecuteResponseAsync>b__0)).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, ShellManager.<ExecuteResponseAsync>d__33>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    str = awaiter.GetResult();
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(str);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine([Nullable((byte) 0)] IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }

        [CompilerGenerated]
        private struct <ReadToEndAsync>d__18 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<string> <>t__builder;
            [Nullable((byte) 0)]
            public ShellManager <>4__this;
            [Nullable((byte) 0)]
            private StringBuilder <builder>5__2;
            [Nullable((byte) 0)]
            private StringBuilder <>7__wrap2;
            [Nullable(new byte[] { 0, 2 })]
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                ShellManager manager = this.<>4__this;
                try
                {
                    string str;
                    string str2;
                    TaskAwaiter<string> awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                    }
                    else
                    {
                        this.<builder>5__2 = new StringBuilder();
                        goto TR_0009;
                    }
                TR_0005:
                    str2 = awaiter.GetResult();
                    this.<>7__wrap2.AppendLine(str2);
                    this.<>7__wrap2 = null;
                TR_0009:
                    while (true)
                    {
                        if (manager._process.StandardOutput.Peek() > -1)
                        {
                            this.<>7__wrap2 = this.<builder>5__2;
                            awaiter = manager.ReadLineAsync().GetAwaiter();
                            if (awaiter.IsCompleted)
                            {
                                goto TR_0005;
                            }
                            else
                            {
                                this.<>1__state = num = 0;
                                this.<>u__1 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, ShellManager.<ReadToEndAsync>d__18>(ref awaiter, ref this);
                            }
                            return;
                        }
                        else
                        {
                            str = this.<builder>5__2.ToString();
                        }
                        break;
                    }
                    this.<>1__state = -2;
                    this.<builder>5__2 = null;
                    this.<>t__builder.SetResult(str);
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<builder>5__2 = null;
                    this.<>t__builder.SetException(exception);
                }
            }

            [DebuggerHidden]
            private void SetStateMachine([Nullable((byte) 0)] IAsyncStateMachine stateMachine)
            {
                this.<>t__builder.SetStateMachine(stateMachine);
            }
        }
    }
}

