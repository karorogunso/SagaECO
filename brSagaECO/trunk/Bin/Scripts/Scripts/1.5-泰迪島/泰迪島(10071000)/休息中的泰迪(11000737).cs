using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:泰迪島(10071000) NPC基本信息:休息中的泰迪(11000737) X:68 Y:160
namespace SagaScript.M10071000
{
    public class S11000737 : Event
    {
        public S11000737()
        {
            this.EventID = 11000737;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            if (!Tinyis_Land_01_mask.Test(Tinyis_Land_01.已經與湖邊的泰迪進行第一次對話))
            {
                初次與湖邊的泰迪進行對話(pc);
                return;
            }

            Say(pc, 11000737, 131, "為什麼微微是望著我……$R;" +
                                   "$R卜卜……$R;", "休息中的泰迪");
        }

        void 初次與湖邊的泰迪進行對話(ActorPC pc)
        {
            BitMask<Tinyis_Land_01> Tinyis_Land_01_mask = new BitMask<Tinyis_Land_01>(pc.CMask["Tinyis_Land_01"]);

            Tinyis_Land_01_mask.SetValue(Tinyis_Land_01.已經與湖邊的泰迪進行第一次對話, true);

            Say(pc, 11000737, 131, "哇!? 害我嚇一跳。$R;", "休息中的泰迪");

            Say(pc, 11000737, 131, "……$R她叫微微$R;" +
                                   "$R不知道從什麼時候到這座島的，$R;" +
                                   "一直都在湖邊。$R;" +
                                   "$P卜卜……$R;" +
                                   "$P哦? 不是…$R;" +
                                   "$R才沒有什麼特別的感情!!$R;", "休息中的泰迪");
        }
    }
}




