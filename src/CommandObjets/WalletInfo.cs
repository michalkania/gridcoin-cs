using System.Runtime.Serialization;

namespace Gridcoin.CommandObjets
{
    [DataContract]
    public class WalletInfo
    {
        [DataMember(Name = "walletversion")]
        public int WalletVersion { get; set; }

        [DataMember(Name = "balance")]
        public decimal Balance { get; set; }

        [DataMember(Name = "newmint")]
        public decimal NewMint { get; set; }

        [DataMember(Name = "stake")]
        public decimal Stake { get; set; }

        [DataMember(Name = "keypoololdest")]
        public long KeyPoolOldest { get; set; }

        [DataMember(Name = "keypoolsize")]
        public int KeyPoolSize { get; set; }

        [DataMember(Name = "unlockeduntil")]
        public long UnclockedUntil { get; set; }
    }
}
