using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞東部平原(10025001) NPC基本信息:食品店老婆婆(11000961) X:38 Y:120
namespace SagaScript.M10025001
{
    public class S11000961 : Event
    {
        public S11000961()
        {
            this.EventID = 11000961;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.物品過重教學完成))
            {
                物品過重教學(pc);
                return;
            }

            Say(pc, 11000961, 131, "刚才给您的，是『沉重杰利科』，$R;" +
                                   "统称沉重的石头。$R;" +
                                   "$P拿着太多沉重或庞大的东西，$R;" +
                                   "就会动不了的呀!$R;" +
                                   "$R那个时候，$R;" +
                                   "把非必要的道具扔掉才行。$R;" +
                                   "$P把图示往视窗外拖动，就可以扔掉，$R;" +
                                   "那样的话，就可以重新走动了。$R;" +
                                   "$P冒险时要经常留意自己的行李啊!$R;" +
                                   "不然要是被敌人包围，$R;" +
                                   "就会被魔物打死的…$R;" +
                                   "$R这样的事，也是有可能发生的!$R;", "食品店老婆婆");
        }

        void 物品過重教學(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.物品過重教學開始))
            {
                Beginner_02_mask.SetValue(Beginner_02.物品過重教學開始, true);

                Say(pc, 11000961, 131, "这里是食品店。$R;" +
                                       "$R烹调的时候，$R;" +
                                       "需要的基本材料在这里买就有了。$R;" +
                                       "$P我…是吧!$R;" +
                                       "拿这个看看吧?$R;", "食品店老婆婆");

                PlaySound(pc, 2041, false, 100, 50);
                GiveItem(pc, 10032810, 1);
                Say(pc, 0, 0, "得到『沉重杰力科』!$R;", " ");
            }

            if (Beginner_02_mask.Test(Beginner_02.物品過重教學開始) && 
                CountItem(pc, 10032810) > 0)
            {
                Beginner_02_mask.SetValue(Beginner_02.物品過重教學完成, true);

                Say(pc, 11000961, 131, "走不动了吧?$R;" +
                                       "$R您的『重量』过重，所以才会那样。$R;" +
                                       "$R道具视窗里，$R;" +
                                       "会显示「重量」和「体积」$R;" +
                                       "看一下吧?$R;" +
                                       "$P这里显示为红色的话，就不能动了。$R;" +
                                       "$P那个时候要扔掉道具，$R;" +
                                       "把重量和体积减低才能移动呀!$R;" +
                                       "$P刚刚给您的『沉重杰利科』，$R;" +
                                       "一般叫沉重的石头。$R;" +
                                       "$R它又重又没用，所以赶紧扔掉吧!$R;" +
                                       "$P把图示向窗外拖，就可以扔掉了，$R;" +
                                       "那样的话，就可以重新走路了。$R;" +
                                       "$P冒险时要经常留意自己的行李啊!$R;" +
                                       "不然要是被敌人包围，$R;" +
                                       "就会被魔物打死的…$R;" +
                                       "$R这样的事，也有可能发生的!$R;", "食品店老婆婆");
            }
        }
    }
}
