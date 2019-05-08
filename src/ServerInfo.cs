using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Gridcoin
{
    [DataContract]
    public class ServerInfo
    {
        [DataMember(Name="version")]
        public string Version { get; set; }

        [DataMember(Name="minor_version")]
        public int MinorVersion { get; set; }

        [DataMember(Name="protocolversion")]
        public int ProtocolVersion { get; set; }

        [DataMember(Name="walletversion")]
        public int WalletVersion { get; set; }

        [DataMember(Name="balance")]
        public decimal Balance { get; set; }

        [DataMember(Name="newmint")]
        public decimal NewMint { get; set; }

        [DataMember(Name="stake")]
        public decimal Stake { get; set; }

        [DataMember(Name="blocks")]
        public long Blocks { get; set; }

        [DataMember(Name="timeoffset")]
        public int TimeOffset { get; set; }

        [DataMember(Name="moneysupply")]
        public decimal MoneySupply { get; set; }

        [DataMember(Name="connections")]
        public int Connections { get; set; }

        [DataMember(Name="proxy")]
        public string Proxy { get; set; }

        [DataMember(Name="ip")]
        public string IpAddress { get; set; }

        [DataMember(Name="difficulty")]
        public Dictionary<string, double> Difficulty { get; set; }

        [DataMember(Name="testnet")]
        public bool Testnet { get; set; }

        [DataMember(Name="keypoololdest")]
        public long KeyPoolOldest { get; set; }

        [DataMember(Name="keypoolsize")]
        public int KeyPoolSize { get; set; }

        [DataMember(Name="paytxfee")]
        public decimal PayTransactionFee { get; set; }

        [DataMember(Name="mininput")]
        public decimal MinimumInput { get; set; }

        [DataMember(Name="unlocked_until")]
        public long UnlockedUntil { get; set; }

        [DataMember(Name="errors")]
        public string Errors { get; set; }
    }
}
