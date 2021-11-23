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
                    "完全没听过您的名字！$R;" +
                    "怎么都好，好像不能信任的感觉$R;" +
                    "您回去吧$R;");
                return;
            }
            if (pc.Fame > 99 && !mask.Test(AYEFlags.與老闆對話))//_2a50)
            {
                mask.SetValue(AYEFlags.與老闆對話, true);
                //_2a50 = true;
                Say(pc, 131, "啊，我说您阿！$R;" +
                    "您，真的是冒险者吗?$R;" +
                    "$R有没有想过帮我$R;" +
                    "击退『南部地牢』的魔物?$R;" +
                    "$P就是这里的南部地牢$R;" +
                    "矿产很丰富的$R;" +
                    "$R但是有很多厉害的魔物$R;" +
                    "很多人受了伤$R;" +
                    "$P公司没有矿物的话$R;" +
                    "可是不行的呢。$R;" +
                    "$R虽然不能给您很多报酬$R;" +
                    "但如果您帮忙，我会很感激您的$R;" +
                    "$P这里的向导会$R;" +
                    "收集击退的魔物$R;" +
                    "那么，拜托您了$R;");
                return;
            }
            Say(pc, 131, "什么？$R;" +
                "我的嗓子大，很烦人?$R;" +
                "这裡机械太多，才那么吵$R;" +
                "所以才得说大声一点啊$R;");
        }
    }
}