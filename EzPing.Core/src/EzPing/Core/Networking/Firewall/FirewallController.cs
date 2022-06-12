namespace EzPing.Core.Networking.Firewall
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
    public static class FirewallController
    {
        private static readonly CultureInfo _culture = new CultureInfo("en-US", false);
        private static readonly Regex _nameRegex = new Regex("^DisplayName *: *([^\r\n]+).*$", RegexOptions.Compiled | RegexOptions.Multiline);
        private const string getAllQuery = "Get-NetFirewallRule -Group EzPing";
        private const string removeAllQuery = "Remove-NetFirewallRule -Group EzPing";

        public static bool AddFirewallRule(FirewallRule rule)
        {
            string str = rule.NewQuery();
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        [AsyncStateMachine(typeof(<AddFirewallRuleAsync>d__9))]
        public static Task<bool> AddFirewallRuleAsync(params FirewallRule[] rule)
        {
            <AddFirewallRuleAsync>d__9 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.rule = rule;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<AddFirewallRuleAsync>d__9>(ref d__);
            return d__.<>t__builder.Task;
        }

        [return: Nullable(new byte[] { 2, 1 })]
        public static string[] GetAllFirewallRules()
        {
            string str = "Get-NetFirewallRule -Group EzPing";
            string[] commands = new string[] { str };
            string input = ShellManager.ExecuteResponse(_culture, commands);
            if (input == null)
            {
                return null;
            }
            Func<Match, string> selector = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<Match, string> local1 = <>c.<>9__7_0;
                selector = <>c.<>9__7_0 = m => m.Groups[1].Value;
            }
            return _nameRegex.Matches(input).Cast<Match>().Select<Match, string>(selector).ToArray<string>();
        }

        [return: Nullable(new byte[] { 1, 2, 1 })]
        [AsyncStateMachine(typeof(<GetAllFirewallRulesAsync>d__13))]
        public static Task<string[]> GetAllFirewallRulesAsync()
        {
            <GetAllFirewallRulesAsync>d__13 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<string[]>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<GetAllFirewallRulesAsync>d__13>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static bool RemoveAllFirewallRules()
        {
            string str = "Remove-NetFirewallRule -Group EzPing";
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        [AsyncStateMachine(typeof(<RemoveAllFirewallRulesAsync>d__14))]
        public static Task<bool> RemoveAllFirewallRulesAsync()
        {
            <RemoveAllFirewallRulesAsync>d__14 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveAllFirewallRulesAsync>d__14>(ref d__);
            return d__.<>t__builder.Task;
        }

        public static bool RemoveFirewallRule(FirewallRule rule)
        {
            string str = rule.RemoveQuery();
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        public static bool RemoveFirewallRule(string name)
        {
            string str = FirewallRule.RemoveQuery(name);
            string[] commands = new string[] { str };
            string str2 = ShellManager.ExecuteResponse(_culture, commands);
            return ((str2 != null) && !str2.Contains("FullyQualifiedErrorId"));
        }

        [AsyncStateMachine(typeof(<RemoveFirewallRuleAsync>d__10))]
        public static Task<bool> RemoveFirewallRuleAsync(FirewallRule rule)
        {
            <RemoveFirewallRuleAsync>d__10 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.rule = rule;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveFirewallRuleAsync>d__10>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<RemoveFirewallRuleAsync>d__11))]
        public static Task<bool> RemoveFirewallRuleAsync(string name)
        {
            <RemoveFirewallRuleAsync>d__11 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.name = name;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveFirewallRuleAsync>d__11>(ref d__);
            return d__.<>t__builder.Task;
        }

        [AsyncStateMachine(typeof(<RemoveFirewallRuleAsync>d__12))]
        public static Task<bool> RemoveFirewallRuleAsync(string[] names)
        {
            <RemoveFirewallRuleAsync>d__12 d__;
            d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
            d__.names = names;
            d__.<>1__state = -1;
            d__.<>t__builder.Start<<RemoveFirewallRuleAsync>d__12>(ref d__);
            return d__.<>t__builder.Task;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            [Nullable((byte) 0)]
            public static readonly FirewallController.<>c <>9 = new FirewallController.<>c();
            [Nullable((byte) 0)]
            public static Func<Match, string> <>9__7_0;
            [Nullable((byte) 0)]
            public static Func<FirewallRule, string> <>9__9_0;
            [Nullable((byte) 0)]
            public static Func<string, string> <>9__12_0;
            [Nullable((byte) 0)]
            public static Func<Match, string> <>9__13_0;

            [NullableContext(0)]
            internal string <AddFirewallRuleAsync>b__9_0(FirewallRule r) => 
                r.NewQuery();

            [NullableContext(0)]
            internal string <GetAllFirewallRules>b__7_0(Match m) => 
                m.Groups[1].Value;

            [NullableContext(0)]
            internal string <GetAllFirewallRulesAsync>b__13_0(Match m) => 
                m.Groups[1].Value.Trim();

            [NullableContext(0)]
            internal string <RemoveFirewallRuleAsync>b__12_0(string name) => 
                FirewallRule.RemoveQuery(name);
        }

        [CompilerGenerated]
        private struct <AddFirewallRuleAsync>d__9 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable(new byte[] { 0, 1 })]
            public FirewallRule[] rule;
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
                        Func<FirewallRule, string> selector = FirewallController.<>c.<>9__9_0;
                        if (FirewallController.<>c.<>9__9_0 == null)
                        {
                            Func<FirewallRule, string> local1 = FirewallController.<>c.<>9__9_0;
                            selector = FirewallController.<>c.<>9__9_0 = new Func<FirewallRule, string>(this.<AddFirewallRuleAsync>b__9_0);
                        }
                        string[] commands = this.rule.Select<FirewallRule, string>(selector).ToArray<string>();
                        awaiter = ShellManager.ExecuteResponseAsync(FirewallController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, FirewallController.<AddFirewallRuleAsync>d__9>(ref awaiter, ref this);
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
        private struct <GetAllFirewallRulesAsync>d__13 : IAsyncStateMachine
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
                        string str = "Get-NetFirewallRule -Group EzPing";
                        string[] commands = new string[] { str };
                        awaiter = ShellManager.ExecuteResponseAsync(FirewallController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0009;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, FirewallController.<GetAllFirewallRulesAsync>d__13>(ref awaiter, ref this);
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
                        Func<Match, string> selector = FirewallController.<>c.<>9__13_0;
                        if (FirewallController.<>c.<>9__13_0 == null)
                        {
                            Func<Match, string> local1 = FirewallController.<>c.<>9__13_0;
                            selector = FirewallController.<>c.<>9__13_0 = new Func<Match, string>(this.<GetAllFirewallRulesAsync>b__13_0);
                        }
                        strArray = FirewallController._nameRegex.Matches(str2).Cast<Match>().Select<Match, string>(selector).ToArray<string>();
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
        private struct <RemoveAllFirewallRulesAsync>d__14 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
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
                        string str = "Remove-NetFirewallRule -Group EzPing";
                        string[] commands = new string[] { str };
                        awaiter = ShellManager.ExecuteResponseAsync(FirewallController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, FirewallController.<RemoveAllFirewallRulesAsync>d__14>(ref awaiter, ref this);
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
        private struct <RemoveFirewallRuleAsync>d__10 : IAsyncStateMachine
        {
            public int <>1__state;
            [Nullable((byte) 0)]
            public AsyncTaskMethodBuilder<bool> <>t__builder;
            [Nullable((byte) 0)]
            public FirewallRule rule;
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
                        string str = this.rule.RemoveQuery();
                        string[] commands = new string[] { str };
                        awaiter = ShellManager.ExecuteResponseAsync(FirewallController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, FirewallController.<RemoveFirewallRuleAsync>d__10>(ref awaiter, ref this);
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
        private struct <RemoveFirewallRuleAsync>d__11 : IAsyncStateMachine
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
                        string str = FirewallRule.RemoveQuery(this.name);
                        string[] commands = new string[] { str };
                        awaiter = ShellManager.ExecuteResponseAsync(FirewallController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, FirewallController.<RemoveFirewallRuleAsync>d__11>(ref awaiter, ref this);
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
        private struct <RemoveFirewallRuleAsync>d__12 : IAsyncStateMachine
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
                        Func<string, string> selector = FirewallController.<>c.<>9__12_0;
                        if (FirewallController.<>c.<>9__12_0 == null)
                        {
                            Func<string, string> local1 = FirewallController.<>c.<>9__12_0;
                            selector = FirewallController.<>c.<>9__12_0 = new Func<string, string>(this.<RemoveFirewallRuleAsync>b__12_0);
                        }
                        string[] commands = this.names.Select<string, string>(selector).ToArray<string>();
                        awaiter = ShellManager.ExecuteResponseAsync(FirewallController._culture, commands).GetAwaiter();
                        if (awaiter.IsCompleted)
                        {
                            goto TR_0004;
                        }
                        else
                        {
                            this.<>1__state = num = 0;
                            this.<>u__1 = awaiter;
                            this.<>t__builder.AwaitUnsafeOnCompleted<TaskAwaiter<string>, FirewallController.<RemoveFirewallRuleAsync>d__12>(ref awaiter, ref this);
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

