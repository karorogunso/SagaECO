using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript.Chinese.Enums
{
    public enum JobBasic_08
    {
        //魔攻師
        選擇轉職為魔攻師 = 0x1,
        已經從闇之精靈那裡把心染為黑暗 = 0x2,
        已經從黑佰特那裡聽取有關黑暗魔法的知識 = 0x4,
        魔攻師轉職任務完成 = 0x8,
        魔攻師轉職成功 = 0x10,
        已經轉職為魔攻師 = 0x20,
    }
}