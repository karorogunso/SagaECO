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
                              "$R前面有火焰…這樣下去很危險的…$R;" +
                              "$P這是『技能石』，$R;" +
                              "利用它的力量，$R;" +
                              "可以用火焰保護您的身體。$R;", "微微");

                Say(pc, 0, 0, "微微把奇形怪狀的石頭扔到空中，$R;" +
                              "然後唸起咒語來了。$R;" +
                              "$R……??$R;" +
                              "閃爍的光……?!$R;", " ");

                ActivateMarionette(pc, 20040301);
                Heal(pc);
                ShowEffect(pc, 8015);
                Say(pc, 0, 0, "變身成為『微微的塞爾曼德』了!$R;", " ");

                Say(pc, 0, 0, "活動木偶塞爾曼德的身體，$R;" +
                              "可以抵受熱力和火焰。$R;" +
                              "$R快! 趕緊通過走道吧!$R;", "微微");
            }
        }
    }
}
