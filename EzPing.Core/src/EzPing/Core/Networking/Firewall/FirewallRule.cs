namespace EzPing.Core.Networking.Firewall
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    [NullableContext(1), Nullable((byte) 0)]
    public class FirewallRule
    {
        public FirewallRule(string displayName, string location)
        {
            this.DisplayName = displayName;
            this.Program = location;
        }

        private static string ActionName(FirewallAction action)
        {
            string str;
            switch (action)
            {
                case FirewallAction.NotConfigured:
                    str = "NotConfigured";
                    break;

                case FirewallAction.Allow:
                    str = "Allow";
                    break;

                case FirewallAction.Block:
                    str = "Block";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("action");
            }
            return str;
        }

        private static string DirectionName(FirewallDirection direction)
        {
            string str;
            if (direction == FirewallDirection.Inbound)
            {
                str = "Inbound";
            }
            else
            {
                if (direction != FirewallDirection.Outbound)
                {
                    throw new ArgumentOutOfRangeException("direction");
                }
                str = "Outbound";
            }
            return str;
        }

        public string NewQuery()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("New-NetFirewallRule ");
            builder.Append("-DisplayName \"" + this.DisplayName + "\" ");
            builder.Append("-Program \"" + this.Program + "\" ");
            builder.Append("-Group \"" + this.Group + "\" ");
            if (this.Direction != null)
            {
                builder.Append("-Direction " + DirectionName(this.Direction.Value) + " ");
            }
            if (this.Action != null)
            {
                builder.Append("-Action " + ActionName(this.Action.Value) + " ");
            }
            return builder.ToString();
        }

        public string RemoveQuery() => 
            RemoveQuery(this.DisplayName);

        public static string RemoveQuery(string displayName) => 
            "Remove-NetFirewallRule -DisplayName \"" + displayName + "\"";

        public string DisplayName { get; set; }

        public string Program { get; set; }

        public FirewallDirection? Direction { get; set; }

        public FirewallAction? Action { get; set; }

        public string Group =>
            "EzPing";
    }
}

