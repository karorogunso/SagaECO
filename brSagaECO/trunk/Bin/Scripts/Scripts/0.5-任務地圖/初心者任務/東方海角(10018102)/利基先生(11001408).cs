using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:東方海角(10018102) NPC基本信息:利基先生(11001408) X:220 Y:65
namespace SagaScript.M10018102
{
    public class S11001408 : Event
    {
        public S11001408()
        {
            this.EventID = 11001408;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與埃米爾進行第一次對話))
            {
                尚未與埃米爾對話(pc);
                return;
            }

            Say(pc, 11001408, 131, "哎，事情都順利嗎?$R;" +
                                   "$R這裡是我父親的店，$R;" +
                                   "下次到東方海角的話，一定要來呀!$R;" +
                                   "$P商店的招牌看見了吧?$R;" +
                                   "$R商人的商店大部份都有這種招牌。$R;" +
                                   "$P這樣到第一次去的城市，$R;" +
                                   "也可以馬上知道那些是什麼店。$R;" +
                                   "$P你看右邊的店也有招牌吧!$R;" +
                                   "$R那是「道具精製師」$R;" +
                                   "和「鑑定師」的招牌。$R;" +
                                   "$P「武器商店」和「古董商店」$R;" +
                                   "也有指定的招牌唷!$R;" +
                                   "$R到阿高普路斯確認一下吧，$R;" +
                                   "對您的旅程會有幫助的。$R;", "利基先生");
        }

        void 尚未與埃米爾對話(ActorPC pc)
        {
            Say(pc, 11001408, 131, "還沒有跟埃米爾打招呼吧?$R;" +
                                   "先去埃米爾那裡吧!$R;", "利基先生");
        }  
    }
}
