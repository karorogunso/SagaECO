using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30081000
{
    public class S11000557 : Event
    {
        public S11000557()
        {
            this.EventID = 11000557;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<AYEFlags> mask = pc.CMask["AYE"];
            if (pc.Fame < 100 && !mask.Test(AYEFlags.與老闆對話))//_2a50)
            {
                Say(pc, 131, "我不知道啊$R;");
                Say(pc, 131, pc.Name +
                    "?$R;" +
                    "完全沒聽過您的名字！$R;" +
                    "怎麼都好，好像不能信任的感覺$R;" +
                    "您回去吧$R;");
                return;
            }
            if (pc.Fame > 99 && !mask.Test(AYEFlags.與老闆對話))//_2a50)
            {
                mask.SetValue(AYEFlags.與老闆對話, true);
                //_2a50 = true;
                Say(pc, 131, "啊，我說您阿！$R;" +
                    "您，真的是冒險者嗎?$R;" +
                    "$R有沒有想過幫我$R;" +
                    "擊退『南部地牢』的魔物?$R;" +
                    "$P就是這裡的南部地牢$R;" +
                    "礦產很豐富的$R;" +
                    "$R但是有很多厲害的魔物$R;" +
                    "很多人受了傷$R;" +
                    "$P公司沒有礦物的話$R;" +
                    "可是不行的呢。$R;" +
                    "$R雖然不能給您很多報酬$R;" +
                    "但如果您幫忙，我會很感激您的$R;" +
                    "$P這裡的嚮導會$R;" +
                    "收集擊退的魔物$R;" +
                    "那麼，拜託您了$R;");
                return;
            }
            Say(pc, 131, "什麼？$R;" +
                "我的嗓子大，很煩人?$R;" +
                "這裡機械太多，才那麼吵$R;" +
                "所以才得說大聲一點阿$R;");
        }
    }
}