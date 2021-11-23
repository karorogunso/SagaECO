using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Beginner_02
    {
        //初心者任務

        //說明ECO的活動木偶系統
        向瑪莎第一次詢問瑪歐斯的情報 = 0x1,
        已經與騎士團長官們進行第一次對話 = 0x2,

        //轉交感謝信
        轉交感謝信任務開始 = 0x4,
        已經把信轉交給初心者嚮導 = 0x8,
        轉交感謝信任務完成 = 0x10,

        //說明ECO的憑依系統
        已經與要憑依的女孩進行第一次對話 = 0x20,

        //說明ECO的道具修復系統
        已經與冒險家前輩進行第一次對話 = 0x40,
        冒險家前輩給予各類藥水 = 0x80,

        //說明ECO的基本系統
        已經與老師進行第一次對話 = 0x100,
        已經與女初階冒險者進行第一次對話 = 0x200,
        已經與男初階冒險者進行第一次對話 = 0x400,

        //說明ECO的過重系統
        物品過重教學開始 = 0x800,
        物品過重教學完成 = 0x1000,

        //瑪莎熱線
        第一次使用瑪莎熱線 = 0x2000,

        //TT的追加标记
        得到巧克力碎餅乾和果汁 = 0x100000,
    }
}
