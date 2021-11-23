using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Neko_01
    {
        //凱堤(桃子)
        桃子任務開始 = 0x1,//_4A27
        得到不明物體的鬍鬢 = 0x2,
        桃子任務完成 = 0x4,//_Xa14
        與瑪歐斯對話 = 0x8,//_2A79
        使用不明的鬍鬚 = 0x10,//_4A26
        與雷米阿對話 = 0x20,//_2A80
        與祭祀對話 = 0x40,//_2A81
        光之精靈對話 = 0x80,//_2A82
        再次與祭祀對話 = 0x100,//_2A83
        得到裁縫阿姨的三角巾 = 0x200,//_2A84
        獲得桃子 = 0x400,//_2A85
    }
}
