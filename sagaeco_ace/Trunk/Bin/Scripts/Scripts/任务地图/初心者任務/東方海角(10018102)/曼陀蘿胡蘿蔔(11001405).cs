using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:曼陀蘿胡蘿蔔(11001405) X:204 Y:90
namespace SagaScript.M10018102
{
    public class S11001405 : Event
    {
        public S11001405()
        {
            this.EventID = 11001405;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001405, 131, "…♪$R;", "曼陀罗胡萝卜");

            Say(pc, 11001403, 131, "这只宠物叫「曼陀罗胡萝卜」。$R;" +
                                   "$R养起来十分费劲，$R;" +
                                   "但对跟植物有关的职业来说，$R;" +
                                   "是很有帮助哦!$R;", "宠物养殖研究员");         
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001405, 131, "…♪$R;", "曼陀罗胡萝卜");
        } 
    }
}
