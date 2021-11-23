using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;

namespace SagaDB.Iris
{
    /// <summary>
    /// 伊利斯扭蛋
    /// </summary>
    [Serializable]
    public class IrisGacha
    {
        public uint ItemID;
        public uint PayFlag;
        public uint SessionID;
        public string SessionName;
        public uint PageID;
        public uint Count;
    }
}
