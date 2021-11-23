using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Neko_02
    {
        //凱堤(藍)
        藍任務結束 = 0x1,//_Xa51

        藍任務開始 = 0x2,//_4A75
        與裁縫阿姨第一次對話 = 0x4,//_4A76
        得知維修方法 = 0x8,//_4A77
        開始維修 = 0x10,//_4A78
        聽取建議 = 0x20,//_4A79
        獲知原始的事情 = 0x40,//_4A80
        得到藍 = 0x80,//_4A81
        得到三角巾 = 0x100,//_4A83
        藍任務失敗 = 0x200,//_4A84
    }
}