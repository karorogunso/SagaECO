using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018101) NPC基本信息:利基先生(11000928) X:220 Y:65
namespace SagaScript.M10018101
{
    public class S11000928 : Event
    {
        public S11000928()
        {
            this.EventID = 11000928;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11000928, 131, "哎，事情都顺利吗?$R;" +
                                   "$R这里是我父亲的店，$R;" +
                                   "下次到东方海角的话，一定要来呀!$R;" +
                                   "$P商店的招牌看见了吧?$R;" +
                                   "$R商人的商店大部份都有这种招牌。$R;" +
                                   "$P这样到第一次去的城市，$R;" +
                                   "也可以马上知道那些是什么店。$R;" +
                                   "$P你看右边的店也有招牌吧!$R;" +
                                   "$R那是「道具精制师」$R;" +
                                   "和「鉴定师」的招牌。$R;" +
                                   "$P「武器商店」和「古董商店」$R;" +
                                   "也有指定的招牌!$R;" +
                                   "$R到阿克罗波利斯确认一下吧，$R;" +
                                   "对您的旅程会有帮助的。$R;", "利基先生");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11000928, 131, "还没有跟埃米尔打招呼吧?$R;" +
                                   "先去埃米尔那里吧!$R;", "利基先生");
        }  
    }
}
