using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:火紅德拉古(11001420) X:202 Y:88
namespace SagaScript.M10018103
{
    public class S11001420 : Event
    {
        public S11001420()
        {
            this.EventID = 11001420;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001420, 0, "…$R;", "炽色步行龙");

            Say(pc, 11001417, 131, "这只宠物叫「炽色步行龙」，$R;" +
                                   "是「骑乘宠物」的一种!$R;" +
                                   "$R移动速度非常快，$R;" +
                                   "所以去远方时很方便呀!$R;" +
                                   "$P听说在阿克罗尼亚大陆北部的诺森岛，$R;" +
                                   "发现了它的蛋呢!!$R;", "宠物养殖研究员"); 
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001420, 0, "…$R;", "炽色步行龙");
        }  
    }
}
