using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10060000
{
    public class S11000844 : Event
    {
        public S11000844()
        {
            this.EventID = 11000844;

        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<LightTower> LightTower_mask = pc.CMask["LightTower"];
            
            if (LightTower_mask.Test(LightTower.給予懷錶))
            {
                Say(pc, 131, "聽著懷錶的聲音$R;" +
                    "就會覺得他在我的身邊呢$R;" +
                    "$R太感謝您了…$R;");
                return;
            }

            if (CountItem(pc, 10024680) > 0)
            {
                LightTower_mask.SetValue(LightTower.給予懷錶, true);
                //_6a59 = true;
                TakeItem(pc, 10024680, 1);
                Say(pc, 131, "啊！這是？他的…$R;" +
                    "您在哪裡得到這個的？$R;" +
                    "$R原來如此…您去過那座塔呀$R;" +
                    "$P謝謝...太感謝您了$R;" +
                    "$R沒錯，他肯定就是我死去的愛人。$R;" +
                    "原來很久以前就出事了…$R;" +
                    "$P這個懷錶沒有指針對吧?$R;" +
                    "那個指針在我這兒呢$R;" +
                    "$P是他去塔之前，在這棵樹下$R;" +
                    "托我保管的…$R;" +
                    "$R每當傷心落淚時，我拿著指針，$R;" +
                    "時間就像是停留一般$R;" +
                    "“沒關係…很快會回來的…”$R;" +
                    "說完這句話就走了…$R;" +
                    "$P現在這個錶也可以動了$R;" +
                    "謝謝...太感謝您了$R;");
                return;
            }
            
            Say(pc, 131, "您好！小不點$R;" +
                "$R問我在幹什麼是嗎？$R;" +
                "$P正在等人呢$R;" +
                "$R很久以前就約好了$R;" +
                "在這個樹下等他$R;" +
                "$R您想聽聽有關我的故事嗎?$R;");
            switch (Select(pc, "聽不聽奶奶的故事呢？", "", "聽", "不聽"))
            {
                case 1:
                    Say(pc, 131, "那想聽一聽嗎?$R;" +
                        "$R從這裡往西走$R;" +
                        "有機械文明時代$R;" +
                        "未完成的建築物唷$R;" +
                        "$P現在什麼也沒有，成了廢墟！$R;" +
                        "$R以前摩根和阿伊恩薩烏斯$R;" +
                        "還是聯合城的時候，曾打算重建呀$R;" +
                        "$P阿伊恩薩烏斯的人，$R;" +
                        "希望把它建成後，$R;" +
                        "想向世界展示自己的技術能力阿$R;" +
                        "$P那時，摩根的所有居民，$R;" +
                        "為了重建這座塔而努力工作呀$R;" +
                        "$R我的愛人也一樣$R;" +
                        "$P他離開我的時候跟我約好$R;" +
                        "$R在這個地方…$R;" +
                        "在這棵樹下見面$R;" +
                        "$P但是他一直沒有回來$R;" +
                        "只是聽說塔上發生了很大的事故，$R;" +
                        "他也被卷進去了$R;" +
                        "$R我…我…傷心過度$R;" +
                        "怎麼也不肯相信阿$R;" +
                        "$P也想過去找他$R;" +
                        "不過事故發生後$R;" +
                        "塔內出現了魔物去不了…$R;" +
                        "$R現在只能在這裡等他回來$R;" +
                        "$P雖然現在我想他不會回來了$R;" +
                        "$R但是這個地方對我來說$R;" +
                        "非常重要的阿。$R;" +
                        "$P唉…謝謝您聽完我的故事$R;" +
                        "$R我一直會在這裡，$R;" +
                        "再來找我吧$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}