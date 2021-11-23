using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum DEMNewbie
    {
        领取EP = 0x1,
        介绍造型变换 = 0x2,
        给予改造部件 = 0x4,
        已经DEMIC改造完毕 = 0x8,
        要求去攻击靶子 = 0x10,
        第一次跟迷之女性说话 = 0x20,
        第一次找实验室向导 = 0x40,
    }
}
