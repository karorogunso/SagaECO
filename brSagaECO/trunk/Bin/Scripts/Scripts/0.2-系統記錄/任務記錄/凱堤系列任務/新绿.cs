using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Neko_09
    {
        新绿任务开始 = 0x1,
        綠色三角巾入手 = 0x2,
        黑暗圣杯入手 = 0x4,

        获得灵魂碎片_01 = 0x8,//道米尼界通天塔
        获得灵魂碎片_02 = 0x10,//上城
        获得灵魂碎片_03 = 0x20,//随机1
        获得灵魂碎片_04 = 0x40,//随机2

        黑暗圣杯满了 = 0x80,
        DEM消失 = 0x100,
        去军舰岛 = 0x200,
        完成 = 0x400,
    }
}