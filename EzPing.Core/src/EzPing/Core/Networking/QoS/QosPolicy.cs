namespace EzPing.Core.Networking.QoS
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    [NullableContext(1), Nullable((byte) 0)]
    public class QosPolicy
    {
        public QosPolicy(string name, string location)
        {
            this.Name = name;
            this.AppPathNameMatchCondition = location;
        }

        private static string IPProtocolName(IPProtocolType protocolType)
        {
            string str;
            switch (protocolType)
            {
                case IPProtocolType.TCP:
                    str = "TCP";
                    break;

                case IPProtocolType.UDP:
                    str = "UDP";
                    break;

                case IPProtocolType.Both:
                    str = "Both";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("protocolType");
            }
            return str;
        }

        private static string NetworkProfileName(NetworkProfileType networkProfile)
        {
            string str;
            switch (networkProfile)
            {
                case NetworkProfileType.Domain:
                    str = "Domain";
                    break;

                case NetworkProfileType.Public:
                    str = "Public";
                    break;

                case NetworkProfileType.Private:
                    str = "Private";
                    break;

                case NetworkProfileType.All:
                    str = "All";
                    break;

                default:
                    throw new ArgumentOutOfRangeException("networkProfile");
            }
            return str;
        }

        public string NewQuery()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("New-NetQosPolicy ");
            builder.Append("-Name \"" + this.Name + "\" ");
            builder.Append("-AppPathNameMatchCondition \"" + this.AppPathNameMatchCondition + "\" ");
            if (this.NetworkProfile != null)
            {
                builder.Append("-NetworkProfile " + NetworkProfileName(this.NetworkProfile.Value) + " ");
            }
            if (this.DSCPAction != null)
            {
                builder.Append($"-DSCPAction {this.DSCPAction.Value} ");
            }
            if (this.IPProtocolMatchCondition != null)
            {
                builder.Append("-IPProtocolMatchCondition " + IPProtocolName(this.IPProtocolMatchCondition.Value) + " ");
            }
            if (this.ThrottleRateActionBitsPerSecond != null)
            {
                builder.Append($"-ThrottleRateActionBitsPerSecond {this.ThrottleRateActionBitsPerSecond.Value}");
            }
            return builder.ToString();
        }

        public string RemoveQuery() => 
            RemoveQuery(this.Name);

        public static string RemoveQuery(string name) => 
            "Remove-NetQosPolicy -Name \"" + name + "\" -Confirm:$false";

        public string Name { get; set; }

        public string AppPathNameMatchCondition { get; set; }

        public bool Empty =>
            (this.NetworkProfile == null) && ((this.DSCPAction == null) && ((this.IPProtocolMatchCondition == null) && (this.ThrottleRateActionBitsPerSecond == null)));

        public NetworkProfileType? NetworkProfile { get; set; }

        public int? DSCPAction { get; set; }

        public IPProtocolType? IPProtocolMatchCondition { get; set; }

        public long? ThrottleRateActionBitsPerSecond { get; set; }

        public string Group =>
            "EzPing";
    }
}

