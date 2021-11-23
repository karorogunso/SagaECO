using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:歷奇號(11000929) X:222 Y:61
namespace SagaScript.M10018101
{
    public class S11000929 : Event
    {
        public S11000929()
        {
            this.EventID = 11000929;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11000929, 500, "唔…$R;", "利基号");

            Say(pc, 11001408, 131, "啊，对不起，可能是看到陌生的脸孔$R;" +
                                   "所以有点害怕$R;" +
                                   "$R这小子叫利基号，是我的搭档$R;" +
                                   "$P对商人来说这个小子最棒了，$R;" +
                                   "看到带着这个家伙的人，$R;" +
                                   "就跟他说话吧$R;" +
                                   "$R可能会给您提示的呀$R;", "利基先生");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11000929, 0, "唔…$R;", "利基号");
        }  
    }
}
