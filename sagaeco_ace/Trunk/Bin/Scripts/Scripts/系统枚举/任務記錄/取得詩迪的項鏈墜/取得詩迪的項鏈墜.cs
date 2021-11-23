using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Sinker
    {
        //觸發任務
        看過告示牌 = 0x1,//_7a91
        //在帕斯特收取合成藥
        未收到合成藥 = 0x2,//_7a92
        收到合成藥 = 0x4,//_7a93
        //鋼鐵工廠老闆
        拒絕幫忙 = 0x8,//_7a94
        未收到不明的合金 = 0x10,//_7a95
        收到不明的合金 = 0x20,//_7a96
        //摩根,商人行會總部
        未獲得給社長的信 = 0x40,//_7a97
        //返回鋼鐵工廠
        未收到合成測試報告 = 0x80,//_7a98
        //帕斯特
        未收到別針 = 0x100,//_7a99
        獲得別針 = 0x200,


        //取得詩迪的項鏈墜
        詩迪的項鏈墜製作完成 = 0x400,
        寶石商人給予詩迪的項鏈墜 = 0x800,
    }
}
