using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:阿高普路斯市市走道(50031000) NPC基本信息:微微(13000168) X:11 Y:19
namespace SagaScript.M50031000
{
    public class S13000168 : Event
    {
        public S13000168()
        {
            this.EventID = 13000168;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.變身成為微微的塞爾曼德))
            {
                Beginner_01_mask.SetValue(Beginner_01.變身成為微微的塞爾曼德, true);

                Say(pc, 0, 0, "啊…稍等!!$R;" +
                              "$R前面有火焰…这样下去很危险的…$R;" +
                              "$P这是『技能石』，$R;" +
                              "利用它的力量，$R;" +
                              "可以用火焰保护您的身体。$R;", "蒂塔");

                Say(pc, 0, 0, "蒂塔把奇形怪状的石头扔到空中，$R;" +
                              "然后念起咒语来了。$R;" +
                              "这是……??$R;" +
                              "闪烁的光……?!$R;", " ");

                ActivateMarionette(pc, 20040301);
                Heal(pc);
                ShowEffect(pc, 8015);
                Say(pc, 0, 0, "变身成为『蒂塔的火焰蜥蜴』了!$R;", " ");

                Say(pc, 0, 0, "活动木偶火焰蜥蜴的身体，$R;" +
                              "可以抵受热力和火焰。$R;" +
                              "$R快! 赶紧通过走道吧!$R;", "蒂塔");
            }
        }
    }
}
