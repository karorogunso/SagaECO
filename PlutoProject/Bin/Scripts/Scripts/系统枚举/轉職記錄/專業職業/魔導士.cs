using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Job2X_05
    {
        //魔導士
        轉職開始 = 0x1,//_3A89
        要求5樣物品 = 0x2,//_3A90
        防禦過高 = 0x4,//_4A12
        拒絕轉職 = 0x8,//_4A11
        轉職完成 = 0x10,//_3A92
    }
}