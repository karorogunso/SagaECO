using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Job2X_12
    {
        //貿易商
        聽取貿易商說明 = 0x1,//_3A62
        轉職開始 = 0x2,//_3A63
        搜集紋章紙 = 0x4,//_3A69
        給予紋章紙 = 0x8,//_3A74
        收集解毒果 = 0x10,//_3A70
        給予解毒果 = 0x20,//_3A75
        收集黃麥粉 = 0x40,//_3A71
        給予黃麥粉 = 0x80,//_3A76
        收集石油 = 0x100,//_3A72
        給予石油 = 0x200,//_3A77
        收集花束 = 0x400,//_3A73
        給予花束 = 0x800,//_3A78
        收集結束 = 0x1000,//_3A64
        索取拜金使的紋章 = 0x2000,//_3A74
        轉職成功 = 0x4000,//_3A65
        防禦過高 = 0x8000,//_3A66

        //額外
        第一次對話 = 0x100000,//_7a22
    }
}