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
        public uint itemid;
        public uint pageID;
        public uint count;
    }
}
