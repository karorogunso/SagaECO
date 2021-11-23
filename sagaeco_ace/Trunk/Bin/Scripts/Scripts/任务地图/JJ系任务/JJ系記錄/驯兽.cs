using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum jjxs
    {  
        开始 = 0x1,
        面试 = 0x2,
        失败 = 0x4,
        正确 = 0x8,
        面试通过 = 0x10,
        测试通过 = 0x20,
        入手徽章 = 0x40,

        开始第一次测试 = 0x80,
        开始第二次测试 = 0x100,

    }
}
