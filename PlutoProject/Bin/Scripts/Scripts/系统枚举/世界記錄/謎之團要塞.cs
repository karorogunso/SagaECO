﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum MZTYSFlags
    {
        //海盜會館,泰迪皇冠的提示
        獲得鵝卵石 = 0x1,
        獲得鐵塊 = 0x2,
        獲得銀塊 = 0x4,
        獲得金塊 = 0x8,
        提示泰迪皇冠 = 0x10,
        獲得泰迪皇冠 = 0x20,
        //武器屋
        武器製作所店員第一次對話 = 0x40,
        //咖啡館
        第一次任務 = 0x80,
        //幹部佈魯
        詢問謎團1 = 0x100,
        詢問謎團2 = 0x200,
        //外觀合成
        幫忙送藥 = 0x400,
        幫忙送謎語團面具 = 0x800,
        交給藥 = 0x1000,
        詢問謎語團面具 = 0x2000,
        收到謎語團面具 = 0x4000,
        交給謎語團面具 = 0x8000,
    }
}