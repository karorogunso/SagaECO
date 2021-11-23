using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum DFHJFlags
    {
        大地精靈第一次對話 = 0x1,
        寻找撒帕涅开始 = 0x2,
        已找到撒帕涅 = 0x4,
        寻找撒帕涅结束 = 0x8,
        开启商店 = 0x10,
        给过青菜 = 0x20,
        甲壳收集开始 = 0x100,
        甲壳收集十万 = 0x200,
        甲壳收集二十万 = 0x400,
        甲壳收集五十万 = 0x800,
        甲壳收集五十五万 = 0x1000,
        甲壳收集六十万 = 0x2000,
        甲壳收集六十五万 = 0x4000,
        甲壳收集七十万 = 0x8000,
        甲壳收集七十五万 = 0x10000,
        甲壳收集八十万 = 0x20000,
        甲壳收集九十万 = 0x40000,
        甲壳收集一百万 = 0x80000,
    }
}