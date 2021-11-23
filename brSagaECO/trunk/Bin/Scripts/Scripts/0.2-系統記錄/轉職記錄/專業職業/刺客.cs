using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Job2X_03
    {
        //刺客
        刺客轉職開始 = 0x1,//_4A00
        //軍艦島
        第一個問題回答正確 = 0x2,//_4A01
        第一個問題回答錯誤 = 0x4,//_4A04
        未獲得暗殺者的內服藥1 = 0x8,//_4A69
        //奧克魯尼亞東海岸
        第二個問題回答正確 = 0x10,//_4A02
        第二個問題回答錯誤 = 0x20,//_4A05
        未獲得暗殺者的內服藥2 = 0x40,//_4A70
        //北方海角
        第三個問題回答正確 = 0x80,//_4A03
        第三個問題回答錯誤 = 0x100,//_4A06
        未獲得暗殺者的內服藥3 = 0x200,//_4A71
        //鐵火山
        第四個問題回答正確 = 0x400,///_4A08
        第四個問題回答錯誤 = 0x800,//_4A07
        未獲得暗殺者的內服藥4 = 0x1000,//_4A72
        //中央
        交還暗殺者的內服藥 = 0x2000,//_4A25
        防禦過高 = 0x4000,
        轉職結束 = 0x8000,
    }
}
