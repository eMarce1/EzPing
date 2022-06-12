namespace EzPing.Core.Diagnostic
{
    using EzPing.Core.Shell;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    [NullableContext(1), Nullable((byte) 0)]
    public class ProcessController
    {
        private static readonly CultureInfo _culture = new CultureInfo("en-US", false);

        public static bool AddProcessPriority(string location, int priority)
        {
            string str = AddProcessPriorityQuery(location, priority);
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        [AsyncStateMachine(typeof(<AddProcessPriorityAsync>d__2))]
        public static Task<bool> AddProcessPriorityAsync(string location, int priority)
        {
            <AddProcessPriorityAsync>d__2 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.location = location;
            d__.priority = priority;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<AddProcessPriorityAsync>d__2>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static string AddProcessPriorityQuery(string location, int priority) => 
            $"wmic process where name="{location}" call setpriority "{priority}"";

        [CompilerGenerated]
        private struct <AddProcessPriorityAsync>d__2 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable((byte) 0)]
            public string location;
            public int priority;
            [Nullable(new byte[] { 0, 2 })]
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    string str2;
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
                        string str = ProcessController.AddProcessPriorityQuery(this.location, this.priority);
                        string[] commands = new string[] { str };
                        awaiter = ShellManager.ExecuteResponseAsync(ProcessController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, ProcessController.<AddProcessPriorityAsync>d__2>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    str2 = awaiter.GetResult();
                    bool result = (str2 != null) && !str2.Contains("FullyQualifiedErrorId");
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(result);
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
    }
}

