using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:火紅德拉古(11001406) X:202 Y:88
namespace SagaScript.M10018102
{
    public class S11001406 : Event
    {
        public S11001406()
        {
            this.EventID = 11001406;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001406, 0, "…$R;", "火紅德拉古");

            Say(pc, 11001403, 131, "這隻寵物叫「火紅德拉古」，$R;" +
                                   "是「騎乘寵物」的一種唷!$R;" +
                                   "$R移動速度非常快，$R;" +
                                   "所以去遠方時很方便呀!$R;" +
                                   "$P聽說在奧魯尼亞大陸北部的諾頓島，$R;" +
                                   "發現了牠的蛋呢!!$R;", "寵物養殖研究員"); 
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001406, 0, "…$R;", "火紅德拉古");
        }  
    }
}
