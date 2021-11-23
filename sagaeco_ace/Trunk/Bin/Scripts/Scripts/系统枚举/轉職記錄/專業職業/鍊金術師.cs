using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum Job2X_10
    {
        //鍊金術師
        轉職開始 = 0x1,//_3a49
        提問開始 = 0x2,//_3a50
        題型1 = 0x4,//_3a51
        題型2 = 0x8,//_3a52
        題型3 = 0x10,//_3a53
        提問第一題 = 0x20,//_4a14
        提問第二題 = 0x40,//_4a15
        提問第三題 = 0x80,//_4a16
        提問第四題 = 0x100,//_4a17
        提問第五題 = 0x200,//_4a18
        提問第六題 = 0x400,//_4a19
        提問第七題 = 0x800,//_4a20
        提問第八題 = 0x1000,//_4a21
        提問第九題 = 0x2000,//_4a22
        提問第十題 = 0x4000,//_4a23
        所有問題回答正確 = 0x8000,//_3a54

        //無關部份
        第一場對話 = 0x1000000,//_3A16
    }
}