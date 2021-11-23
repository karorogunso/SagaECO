using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum World_01
    {
        //世界記錄

        //垃圾桶
        第一次使用垃圾桶 = 0x1,

        //道具精製師
        護髮劑合成任務開始 = 0x2,
        護髮劑合成任務完成 = 0x4,
        已經與道具精製師進行第一次對話 = 0x8,

        //鑑定師
        已經與鑑定師進行第一次對話 = 0x10,

        //六姬
        已經與六姬進行第一次對話 = 0x20,

        //弗朗西斯小姐
        翅膀裝飾合成任務開始 = 0x40,
        翅膀裝飾合成任務完成 = 0x80,
    }
}
