using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Gridcoin.CommandObjets
{
    [DataContract]
    public class BalanceInfo
    {
        public string Balance { get; set; }
    }
}
