﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum MorgFlags
    {
        //摩根市
        //曉特(犬)技能點
        獲得基本職業 = 0x1,//_6a63
        獲得專門職業 = 0x2,//_6a64
        獲得技術職業 = 0x4,//_6a64
        //商人行會總部(30097000) : 引座員
        給予護髮劑 = 0x8,//_6a80
        接受基本職業 = 0x10,//_6a77
        接受專門職業 = 0x20,//_6a78
        接受技術職業 = 0x40,//_6a79

    }
}