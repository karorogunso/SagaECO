using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:南部平原初心者學校(30091003) NPC基本信息:初階冒險者(11000994) X:4 Y:6
namespace SagaScript.M30091003
{
    public class S11000994 : Event
    {
        public S11000994()
        {
            this.EventID = 11000994;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            if (!Beginner_02_mask.Test(Beginner_02.已經與女初階冒險者進行第一次對話))
            {
                初次與初階冒險者進行對話(pc);
                return;
            }

            Say(pc, 11000994, 0, "哎呀，您好!$R;" +
                                 "$R现在对「阿克罗波利斯」熟悉了吗?$R;", "初阶冒险者");
        }

        void 初次與初階冒險者進行對話(ActorPC pc)
        {
            BitMask<Beginner_02> Beginner_02_mask = new BitMask<Beginner_02>(pc.CMask["Beginner_02"]);

            Beginner_02_mask.SetValue(Beginner_02.已經與女初階冒險者進行第一次對話, true);

            int selection;

            Say(pc, 11000994, 0, "哎呀，您好!$R;" +
                                 "$R您第一次来「阿高普路斯市」吗?$R;" +
                                 "$P我只比您稍微早一点$R;" +
                                 "来到「阿高普路斯市」而已。$R;" +
                                 "$R虽然可能不是遇到初心者。$R;" +
                                 "$P但在这裡，可以很容易找到$R;" +
                                 "跟自己等级差不多的人组成队五。$R;" +
                                 "所以我也不知不觉走到这来了。$R;", "初阶冒险者");

            selection = Select(pc, "要继续聊天吗?", "", "衣服很可爱啊!", "您的头发是怎么弄的?", "不用了");

            while (selection != 3)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 11000994, 0, "真的? 谢谢呀!$R;" +
                                             "$P这个T恤是在「阿克罗波利斯」的「下城」的$R;" +
                                             "「染色店阿姨」帮我染的。$R;" +
                                             "$R带『罩衫(男)』或『罩衫(女)』去的话，$R;" +
                                             "她会给您免费染色的。$R;" +
                                             "$P好像是…有点…多变的阿姨呀!$R;" +
                                             "要染什么颜色，看他那时的心情吧?$R;" +
                                             "$R到了「下城」有空的话，去看看也不错哦!$R;" +
                                             "$P要是不知道阿姨在哪里?$R;" +
                                             "看看小地图吧，红点就是NPC的位置，$R;" +
                                             "参考一下吧!$R;", "初阶冒险者");
                        break;

                    case 2:
                        Say(pc, 11000994, 0, "这个头发是在「阿克罗波利斯」的「上城」的$R;" +
                                             "「尼贝隆肯发型屋」得到的哦!$R;" +
                                             "$P那里有个语气奇怪的发型师，$R;" +
                                             "他会剪头发和设计造型喔!$R;" +
                                             "$P啊! 这个头发有店特别吧!$R;" +
                                             "是使用『发型屋介绍书』的呀!$R;" +
                                             "$R是只有拿着介绍书$R;" +
                                             "才帮您设计特别的发型。$R;" +
                                             "$P嗯? 『发型屋介绍书』是怎么得到的?$R;" +
                                             "$P我把平原的木箱打破得到『木箱』，$R;" +
                                             "然后请「道具精制师」帮我打开后。$R;" +
                                             "才发现里面有介绍书的。$R;" +
                                             "$R除此之外，还有其他的方法喔!$R;", "初阶冒险者");
                        break;
                }

                selection = Select(pc, "要继续聊天吗?", "", "衣服很可爱啊", "您的头发是怎么弄的?", "不用了");
            }
        }
    }
}
