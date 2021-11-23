using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum hanabi1
    {  
        第一次对话后 = 0x1,
        泰迪对话后 = 0x2,
        第二次对话后 = 0x4,
        狐狸羽織入手后 = 0x8,
        狐和服入手后 = 0x10,
        给吃的 = 0x20,
        吃了 = 0x40,
        半纏 = 0x80,
    }
}
