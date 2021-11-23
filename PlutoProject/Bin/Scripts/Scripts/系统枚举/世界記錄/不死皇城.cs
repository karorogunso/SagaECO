﻿using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum BSHCFlags
    {
        開始尋找耳環 = 0x1,
        找到左耳環 = 0x2,
        找到右耳環 = 0x4,
        左耳環位置1 = 0x8,
        左耳環位置2 = 0x10,
        左耳環位置3 = 0x20,
        右耳環位置1 = 0x40,
        右耳環位置2 = 0x80,
        右耳環位置3 = 0x100,
    }
}