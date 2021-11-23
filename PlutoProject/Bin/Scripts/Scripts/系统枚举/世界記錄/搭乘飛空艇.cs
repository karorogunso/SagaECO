using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum TravelFGarden
    {
        //摩根到軍艦島
        已經辦理手續 = 0x1,//_6a57
        //摩根到光塔
        已经买票 = 0x2, //_6a54
        //马克码头到唐卡
        已办理去唐卡手续 = 0x4,
        持有南军团证 = 0x8,
    }
}