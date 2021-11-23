using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum FGarden
    {
        得到飛空庭鑰匙 = 0x1,
        第一次和飛空庭匠人說話 = 0x2,
        得知飛空庭材料 = 0x4,
        還飛空庭旋轉帆超重 = 0x8,
        唐卡注册飞空庭 = 0x10,//xa31
        委托飞空庭甲板 = 0x20, //xb32
        委托涡轮引擎 = 0x40, //xb33
        委托汽笛 = 0x80,//xb34
        委托飞行用帆 = 0x100,
        完全委托飞行用帆 = 0x200,//xb36
        委托飞行用大帆 = 0x400,//xb37
        完全委托飞行用大帆 = 0x800,//xb38
        接受改造飞空庭订单 = 0x1000,//xb30
        铁板收集完毕 = 0x2000,//xb31
        听完飞空庭飞行规则 = 0x4000,//2b29
        开始收集改造部件 = 0x8000,//2b30
        飞空庭改造完成 = 0x10000,//xb39
        给予飞空翅膀 = 0x20000,//xb40
        凯提推进器 = 0x40000,//2b32
        凯提推进器_光 = 0x80000,//2b33
        凯提推进器_暗 = 0x100000,//2b34
        超重用仓库 =0x200000,//2b31
    }
}

