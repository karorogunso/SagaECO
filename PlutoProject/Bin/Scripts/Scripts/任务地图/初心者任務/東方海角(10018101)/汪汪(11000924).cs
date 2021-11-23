using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:汪汪(11000924) X:201 Y:89
namespace SagaScript.M10018101
{
    public class S11000924 : Event
    {
        public S11000924()
        {
            this.EventID = 11000924;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11000924, 0, "汪汪!$R;", "汪汪");

            Say(pc, 11000923, 131, "这只像狗的宠物叫「汪汪」，$R;" +
                                   "除了它还有别的种类。$R;" +
                                   "$R根据职业，$R;" +
                                   "宠物在作战时，也可以帮忙喔!!$R;", "宠物养殖研究员");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11000924, 0, "汪汪!$R;", "汪汪");
        }  
    }
}
