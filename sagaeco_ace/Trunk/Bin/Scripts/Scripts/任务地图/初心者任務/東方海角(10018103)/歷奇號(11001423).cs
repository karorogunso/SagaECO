using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018103) NPC基本信息:歷奇號(11001423) X:222 Y:61
namespace SagaScript.M10018103
{
    public class S11001423 : Event
    {
        public S11001423()
        {
            this.EventID = 11001423;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001423, 500, "唔…$R;", "利基号");

            Say(pc, 11001422, 131, "啊，对不起，可能是看到陌生的脸孔$R;" +
                                   "所以有点害怕$R;" +
                                   "$R这小子叫利基号，是我的搭档$R;" +
                                   "$P对商人来说这个小家伙最棒了，$R;" +
                                   "看到带着这个家伙的人，$R;" +
                                   "就跟他说话吧$R;" +
                                   "$R可能会给您提示的呀$R;", "利基先生");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001423, 0, "唔…$R;", "利基号");
        }  
    }
}
