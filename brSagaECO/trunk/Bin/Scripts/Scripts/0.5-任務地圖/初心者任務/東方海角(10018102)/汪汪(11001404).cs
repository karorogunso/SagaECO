using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:汪汪(11001404) X:201 Y:89
namespace SagaScript.M10018102
{
    public class S11001404 : Event
    {
        public S11001404()
        {
            this.EventID = 11001404;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001404, 0, "汪汪!$R;", "汪汪");

            Say(pc, 11001403, 131, "這隻像狗的寵物叫「汪汪」，$R;" +
                                   "除了牠還有別的種類。$R;" +
                                   "$R根據職業，$R;" +
                                   "寵物在作戰時，也可以幫忙喔!!$R;", "寵物養殖研究員");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001404, 0, "汪汪!$R;", "汪汪");
        }  
    }
}
