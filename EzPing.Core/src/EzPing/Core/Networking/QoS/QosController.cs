namespace EzPing.Core.Networking.QoS
{
    using EzPing.Core.Shell;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    [NullableContext(1), Nullable((byte) 0)]
    public static class QosController
    {
        private static readonly CultureInfo _culture = new CultureInfo("en-US", false);
        private static readonly Regex _nameRegex = new Regex("^Name *: *([^\r\n]+).*$", RegexOptions.Compiled | RegexOptions.Multiline);
        private const string getAllQuery = "Get-NetQoSPolicy";

        public static bool AddQosPolicy(QosPolicy policy)
        {
            if (policy.Empty)
            {
                return true;
            }
            string str = policy.NewQuery();
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        [AsyncStateMachine(typeof(<AddQosPolicyAsync>d__8))]
        public static Task<bool> AddQosPolicyAsync(params QosPolicy[] policy)
        {
            <AddQosPolicyAsync>d__8 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.policy = policy;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<AddQosPolicyAsync>d__8>(ref d__);
            return d__.<>t__builder.Task;
        }

        [return: Nullable(new byte[] { 2, 1 })]
        public static string[] GetAllQosPolicies()
        {
            string str = "Get-NetQoSPolicy";
            string[] commands = new string[] { str };
            string input = ShellManager.ExecuteResponse(_culture, commands);
            if (input == null)
            {
                return null;
            }
            Func<Match, string> selector = <>c.<>9__6_0;
            if (<>c.<>9__6_0 == null)
            {
                Func<Match, string> local1 = <>c.<>9__6_0;
                selector = <>c.<>9__6_0 = m => m.Groups[1].Value.Trim();
            }
            return _nameRegex.Matches(input).Cast<Match>().Select<Match, string>(selector).ToArray<string>();
        }

        [return: Nullable(new byte[] { 1, 2, 1 })]
        [AsyncStateMachine(typeof(<GetAllQosPoliciesAsync>d__12))]
        public static Task<string[]> GetAllQosPoliciesAsync()
        {
            <GetAllQosPoliciesAsync>d__12 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<string[]>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetAllQosPoliciesAsync>d__12>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static bool RemoveAllQosPolicy()
        {
            Func<string, bool> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<string, bool> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = p => p.StartsWith("EzPing");
            }
            Func<string, string> selector = <>c.<>9__7_1;
            if (<>c.<>9__7_1 == null)
            {
                Func<string, string> local2 = <>c.<>9__7_1;
                selector = <>c.<>9__7_1 = name => QosPolicy.RemoveQuery(name);
            }
            string[] commands = GetAllQosPolicies().Where<string>(predicate).Select<string, string>(selector).ToArray<string>();
            string str = ShellManager.ExecuteResponse(_culture, commands);
            return ((str != null) && !str.Contains("FullyQualifiedErrorId"));
        }

        [AsyncStateMachine(typeof(<RemoveAllQosPolicyAsync>d__13))]
        public static Task<bool> RemoveAllQosPolicyAsync()
        {
            <RemoveAllQosPolicyAsync>d__13 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveAllQosPolicyAsync>d__13>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static bool RemoveQosPolicy(QosPolicy policy)
        {
            string str = policy.RemoveQuery();
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        public static bool RemoveQosPolicy(string name)
        {
            string str = QosPolicy.RemoveQuery(name);
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        [AsyncStateMachine(typeof(<RemoveQosPolicyAsync>d__9))]
        public static Task<bool> RemoveQosPolicyAsync(params QosPolicy[] policy)
        {
            <RemoveQosPolicyAsync>d__9 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.policy = policy;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveQosPolicyAsync>d__9>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<RemoveQosPolicyAsync>d__10))]
        public static Task<bool> RemoveQosPolicyAsync(string name)
        {
            <RemoveQosPolicyAsync>d__10 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.name = name;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveQosPolicyAsync>d__10>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<RemoveQosPolicyAsync>d__11))]
        public static Task<bool> RemoveQosPolicyAsync(string[] names)
        {
            <RemoveQosPolicyAsync>d__11 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.names = names;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveQosPolicyAsync>d__11>(ref d__);
            return d__.<>t__builder.Task;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            [Nullable((byte) 0)]
            public static readonly QosController.<>c <>9 = new QosController.<>c();
            [Nullable((byte) 0)]
            public static Func<Match, string> <>9__6_0;
            [Nullable((byte) 0)]
            public static Func<string, bool> <>9__7_0;
            [Nullable((byte) 0)]
            public static Func<string, string> <>9__7_1;
            [Nullable((byte) 0)]
            public static Func<QosPolicy, bool> <>9__8_0;
            [Nullable((byte) 0)]
            public static Func<QosPolicy, string> <>9__8_1;
            [Nullable((byte) 0)]
            public static Func<QosPolicy, string> <>9__9_0;
            [Nullable((byte) 0)]
            public static Func<string, string> <>9__11_0;
            [Nullable((byte) 0)]
            public static Func<Match, string> <>9__12_0;
            [Nullable((byte) 0)]
            public static Func<string, bool> <>9__13_0;
            [Nullable((byte) 0)]
            public static Func<string, string> <>9__13_1;

            [NullableContext(0)]
            internal bool <AddQosPolicyAsync>b__8_0(QosPolicy p) => 
                !p.Empty;

            [NullableContext(0)]
            internal string <AddQosPolicyAsync>b__8_1(QosPolicy p) => 
                p.NewQuery();

            [NullableContext(0)]
            internal string <GetAllQosPolicies>b__6_0(Match m) => 
                m.Groups[1].Value.Trim();

            [NullableContext(0)]
            internal string <GetAllQosPoliciesAsync>b__12_0(Match m) => 
                m.Groups[1].Value;

            [NullableContext(0)]
            internal bool <RemoveAllQosPolicy>b__7_0(string p) => 
                p.StartsWith("EzPing");

            [NullableContext(0)]
            internal string <RemoveAllQosPolicy>b__7_1(string name) => 
                QosPolicy.RemoveQuery(name);

            [NullableContext(0)]
            internal bool <RemoveAllQosPolicyAsync>b__13_0(string p) => 
                p.StartsWith("EzPing");

            [NullableContext(0)]
            internal string <RemoveAllQosPolicyAsync>b__13_1(string name) => 
                QosPolicy.RemoveQuery(name);

            [NullableContext(0)]
            internal string <RemoveQosPolicyAsync>b__11_0(string name) => 
                QosPolicy.RemoveQuery(name);

            [NullableContext(0)]
            internal string <RemoveQosPolicyAsync>b__9_0(QosPolicy p) => 
                p.RemoveQuery();
        }

        [CompilerGenerated]
        private struct <AddQosPolicyAsync>d__8 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable(new byte[] { 0, 1 })]
            public QosPolicy[] policy;
            [Nullable(new byte[] { 0, 2 })]
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
                        Func<QosPolicy, bool> predicate = QosController.<>c.<>9__8_0;
                        if (QosController.<>c.<>9__8_0 == null)
                        {
                            Func<QosPolicy, bool> local1 = QosController.<>c.<>9__8_0;
                            predicate = QosController.<>c.<>9__8_0 = new Func<QosPolicy, bool>(this.<AddQosPolicyAsync>b__8_0);
                        }
                        Func<QosPolicy, string> selector = QosController.<>c.<>9__8_1;
                        if (QosController.<>c.<>9__8_1 == null)
                        {
                            Func<QosPolicy, string> local2 = QosController.<>c.<>9__8_1;
                            selector = QosController.<>c.<>9__8_1 = new Func<QosPolicy, string>(this.<AddQosPolicyAsync>b__8_1);
                        }
                        string[] commands = this.policy.Where<QosPolicy>(predicate).Select<QosPolicy, string>(selector).ToArray<string>();
                        awaiter = ShellManager.ExecuteResponseAsync(QosController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, QosController.<AddQosPolicyAsync>d__8>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    str = awaiter.GetResult();
                    bool result = (str != null) && !str.Contains("FullyQualifiedErrorId");
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

        [CompilerGenerated]
        private struct <GetAllQosPoliciesAsync>d__12 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable(new byte[] { 0, 0, 1 })]
            public AsyncTaskMethodBuilder<string[]> <>t__builder;
            [Nullable(new byte[] { 0, 2 })]
            private TaskAwaiter<string> <>u__1;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    string[] strArray;
                    string str2;
                    TaskAwaiter<string> awaiter;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                        goto TR_0009;
                    }
                    else
                    {
                        string str = "Get-NetQoSPolicy";
                        string[] commands = new string[] { str };
                        awaiter = ShellManager.ExecuteResponseAsync(QosController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0009;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, QosController.<GetAllQosPoliciesAsync>d__12>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0009:
                    str2 = awaiter.GetResult();
                    if (str2 == null)
                    {
                        strArray = null;
                    }
                    else
                    {
                        Func<Match, string> selector = QosController.<>c.<>9__12_0;
                        if (QosController.<>c.<>9__12_0 == null)
                        {
                            Func<Match, string> local1 = QosController.<>c.<>9__12_0;
                            selector = QosController.<>c.<>9__12_0 = new Func<Match, string>(this.<GetAllQosPoliciesAsync>b__12_0);
                        }
                        strArray = QosController._nameRegex.Matches(str2).Cast<Match>().Select<Match, string>(selector).ToArray<string>();
                    }
                    this.<>1__state = -2;
                    this.<>t__builder.SetResult(strArray);
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
        private struct <RemoveAllQosPolicyAsync>d__13 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable(new byte[] { 0, 2, 1 })]
            private TaskAwaiter<string[]> <>u__1;
            [Nullable(new byte[] { 0, 2 })]
            private TaskAwaiter<string> <>u__2;

            private void MoveNext()
            {
                int num = this.<>1__state;
                try
                {
                    string str;
                    TaskAwaiter<string[]> awaiter;
                    TaskAwaiter<string> awaiter2;
                    if (num == 0)
                    {
                        awaiter = this.<>u__1;
                        this.<>u__1 = new TaskAwaiter<string[]>();
                        this.<>1__state = num = -1;
                    }
                    else if (num == 1)
                    {
                        awaiter2 = this.<>u__2;
                        this.<>u__2 = new TaskAwaiter<string>();
                        this.<>1__state = num = -1;
                        goto TR_0005;
                    }
                    else
                    {
                        awaiter = QosController.GetAllQosPoliciesAsync().GetAwaiter();
                        if (!awaiter.IsCompleted)
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string[]>, QosController.<RemoveAllQosPolicyAsync>d__13>(ref awaiter, ref this);
                            return;
                        }
                    }
                    Func<string, bool> predicate = QosController.<>c.<>9__13_0;
                    if (QosController.<>c.<>9__13_0 == null)
                    {
                        Func<string, bool> local1 = QosController.<>c.<>9__13_0;
                        predicate = QosController.<>c.<>9__13_0 = new Func<string, bool>(this.<RemoveAllQosPolicyAsync>b__13_0);
                    }
                    Func<string, string> selector = QosController.<>c.<>9__13_1;
                    if (QosController.<>c.<>9__13_1 == null)
                    {
                        Func<string, string> local2 = QosController.<>c.<>9__13_1;
                        selector = QosController.<>c.<>9__13_1 = new Func<string, string>(this.<RemoveAllQosPolicyAsync>b__13_1);
                    }
                    string[] commands = awaiter.GetResult().Where<string>(predicate).Select<string, string>(selector).ToArray<string>();
                    awaiter2 = ShellManager.ExecuteResponseAsync(QosController._culture, commands).GetAwaiter();
                    if (awaiter2.IsCompleted)
                    {
                        goto TR_0005;
                    }
                    else
                    {
                        this.<>1__state = num = 1;
                        this.<>u__2 = awaiter2;
                        this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, QosController.<RemoveAllQosPolicyAsync>d__13>(ref awaiter2, ref this);
                    }
                    return;
                TR_0005:
                    str = awaiter2.GetResult();
                    bool result = (str != null) && !str.Contains("FullyQualifiedErrorId");
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

        [CompilerGenerated]
        private struct <RemoveQosPolicyAsync>d__10 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable((byte) 0)]
            public string name;
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
                        string str = QosPolicy.RemoveQuery(this.name);
                        string[] commands = new string[] { str };
                        awaiter = ShellManager.ExecuteResponseAsync(QosController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, QosController.<RemoveQosPolicyAsync>d__10>(ref awaiter, ref this);
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

        [CompilerGenerated]
        private struct <RemoveQosPolicyAsync>d__11 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable(new byte[] { 0, 1 })]
            public string[] names;
            [Nullable(new byte[] { 0, 2 })]
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
                        Func<string, string> selector = QosController.<>c.<>9__11_0;
                        if (QosController.<>c.<>9__11_0 == null)
                        {
                            Func<string, string> local1 = QosController.<>c.<>9__11_0;
                            selector = QosController.<>c.<>9__11_0 = new Func<string, string>(this.<RemoveQosPolicyAsync>b__11_0);
                        }
                        string[] commands = this.names.Select<string, string>(selector).ToArray<string>();
                        awaiter = ShellManager.ExecuteResponseAsync(QosController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, QosController.<RemoveQosPolicyAsync>d__11>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    str = awaiter.GetResult();
                    bool result = (str != null) && !str.Contains("FullyQualifiedErrorId");
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

        [CompilerGenerated]
        private struct <RemoveQosPolicyAsync>d__9 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable(new byte[] { 0, 1 })]
            public QosPolicy[] policy;
            [Nullable(new byte[] { 0, 2 })]
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
                        Func<QosPolicy, string> selector = QosController.<>c.<>9__9_0;
                        if (QosController.<>c.<>9__9_0 == null)
                        {
                            Func<QosPolicy, string> local1 = QosController.<>c.<>9__9_0;
                            selector = QosController.<>c.<>9__9_0 = new Func<QosPolicy, string>(this.<RemoveQosPolicyAsync>b__9_0);
                        }
                        string[] commands = this.policy.Select<QosPolicy, string>(selector).ToArray<string>();
                        awaiter = ShellManager.ExecuteResponseAsync(QosController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, QosController.<RemoveQosPolicyAsync>d__9>(ref awaiter, ref this);
                        }
                    }
                    return;
                TR_0004:
                    str = awaiter.GetResult();
                    bool result = (str != null) && !str.Contains("FullyQualifiedErrorId");
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

