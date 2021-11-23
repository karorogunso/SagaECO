using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Crystal
    {
        //名占卜師雷米阿的水晶
        開始收集 = 0x1,//_4A29
        光之精靈 = 0x2,//_4A30
        暗之精靈 = 0x4,//_4A31
        炎之精靈 = 0x8,//_4A32
        水之精靈 = 0x10,//_4A33
        土之精靈 = 0x20,//_4A34
        風之精靈 = 0x40,//_4A35
        第一個水晶 = 0x80,//_4A36
        索取魔杖 = 0x100,//_4A37
        第二個水晶 = 0x200,//_4A38
        繼續收集水晶 = 0x400,//_4A39
    }
}
