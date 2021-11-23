using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:飛空庭的庭院(30201000) NPC基本信息:瑪莎(11000938) X:9 Y:10
namespace SagaScript.M30201000
{
    public class S11000938 : Event
    {
        public S11000938()
        {
            this.EventID = 11000938;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            if (!Beginner_01_mask.Test(Beginner_01.已經與瑪莎進行第一次對話))
            {
                與瑪莎進行第一次對話(pc);
                return;
            }
            else
            {
                與瑪莎進行第二次對話(pc);
                return;
            }
        }

        void 與瑪莎進行第一次對話(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);

            Beginner_01_mask.SetValue(Beginner_01.已經與瑪莎進行第一次對話, true);

            Say(pc, 11000938, 131, "欢迎来到飞空庭!$R;" +
                                   "$P飞空庭是飞在空中的家和庭园喔!$R;" +
                                   "$R在ECO世界，$R;" +
                                   "您可以拥有自己的房子和庭园。$R;" +
                                   "$P飞空庭是由「唐卡」的工匠制作的。$R;" +
                                   "$R收集部件有点困难呀…$R;" +
                                   "$P对了!$R;" +
                                   "$R有个村落里有个工匠，找找看吧，$R;" +
                                   "他人挺好的，$R;" +
                                   "每星期都会免费分发一些部件唷!$R;" +
                                   "$P听说好像在南部的什么地方…$R;" +
                                   "$P飞空庭啊!$R;" +
                                   "$R是在ECO世界里，$R;" +
                                   "非常重要的交通工具哦!$R;" +
                                   "$P要到「摩戈岛」$R;" +
                                   "一定要搭乘飞空庭呀!$R;" +
                                   "$R航道是由军队和行会管理的。$R;" +
                                   "$P一般人拥有的飞空庭$R;" +
                                   "只能当做房子和庭园呀!$R;" +
                                   "$R房子盖好了，可以照喜好随意装修喔!$R;" +
                                   "您可以让朋友进入，$R;" +
                                   "或指定一些人进入，$R;" +
                                   "也可以选择不开放。$R;" +
                                   "$P每个人随着自己的爱好。$R;" +
                                   "可以呈现出独一无二的风格唷!$R;" +
                                   "$R还有，$R;" +
                                   "飞空庭只能在指定地点降下呀!$R;" +
                                   "$R只能在有飞空庭专用的「机场」，$R;" +
                                   "才能降下呢!$R;" +
                                   "$P其实这里是不允许降下的。$R;" +
                                   "$R为了给您看看，才破例的喔!$R;" +
                                   "$P进房子里看一看吧?$R;" +
                                   "$R如果您想到下一阶段的话，$R;" +
                                   "跟我说一声就行了!$R;", "玛莎");
        }

        void 與瑪莎進行第二次對話(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);
            BitMask<Beginner_02> Beginner_02_mask =pc.CMask["Beginner_02"];

            byte x, y;
            if (!Beginner_02_mask.Test(Beginner_02.得到巧克力碎餅乾和果汁))
            {
                Beginner_02_mask.SetValue(Beginner_02.得到巧克力碎餅乾和果汁, true);
                Say(pc, 11000938, 131, "这里准备了饼干!$R;" +
                                       "一边吃，一边休息吧!$R;", "玛莎");

                PlaySound(pc, 2040, false, 100, 50);
                GiveItem(pc, 10009450, 1);
                GiveItem(pc, 10001600, 1);
                Say(pc, 0, 131, "得到『缺角的巧克力饼干』和『果汁』!$R;", " ");
            }
            if (Beginner_01_mask.Test(Beginner_01.發現床底下的東西))
            {
                ShowEffect(pc, 11000938, 4520);
                Wait(pc, 990);

                Say(pc, 11000938, 131, "啊啊啊啊!!$R;" +
                                       "$R这是「合成失败物」??$R;" +
                                       "这是哪来的呀?$R;" +
                                       "$R什么? 飞天鼠发现的?$R;" +
                                       "$P唉…$R;" +
                                       "$P被发现就没有办法了。$R;" +
                                       "$R烹调或合成，也有失败的时候吧!$R;" +
                                       "那时产生的…就是 『合成失败物』啦!$R;", "玛莎");

                ShowEffect(pc, 11000938, 4507);
                Wait(pc, 990);

                Say(pc, 11000938, 131, "不是经常失败的，$R;" +
                                       "只是偶~~~尔吧!!$R;", "玛莎");

                Say(pc, 11000938, 134, "呜呜…$R;" +
                                       "$R本来想以后自己拿到古董商店那里的。$R;" +
                                       "$P现在去不去下一阶段呢?$R;", "玛莎");
            }

            switch (Select(pc, "去下一阶段吗?", "", "去下一阶段", "再告诉我一次关于飞空庭的情报", "再休息一会儿"))
            {
                case 1:
                    Say(pc, 11000938, 131, "那么现在走吧!$R;" +
                                           "$P不过……$R;" +
                                           "$R从刚才那个地方到城市，$R;" +
                                           "需要一些时间喔!$R;" +
                                           "$P所以我用飞空庭$R;" +
                                           "送您到那里吧!$R;" +
                                           "$P其实是不准使用飞空庭的。$R;" +
                                           "$R但是帮助初心者，应该没问题啦!$R;" +
                                           "$P那么出发吧!$R;", "玛莎");

                    PlaySound(pc, 2438, false, 100, 50);
                    Wait(pc, 1980);

                    x = (byte)Global.Random.Next(171, 174);
                    y = (byte)Global.Random.Next(100, 103);

                    Warp(pc, 10025001, x, y);
                    break;

                case 2:
                    Say(pc, 11000938, 131, "飞空庭是由「唐卡」的工匠制作的哦!$R;" +
                                           "$R收集部件有点困难呀…$R;" +
                                           "$P对了!$R;" +
                                           "$R有个村落里有个工匠，找找看吧，$R;" +
                                           "他人挺好的，$R;" +
                                           "每星期都会免费分发一些部件唷!$R;" +
                                           "$P听说好像在南部的什么地方…$R;" +
                                           "$P飞空庭啊!$R;" +
                                           "$R是在ECO世界里，$R;" +
                                           "非常重要的交通工具唷!$R;" +
                                           "$P要到「摩戈岛」$R;" +
                                           "一定要搭乘飞空庭呀!$R;" +
                                           "$R航道是由军队和行会管理的。$R;" +
                                           "$P一般人拥有的飞空庭$R;" +
                                           "只能当做房子和庭园呀!$R;" +
                                           "$R房子盖好了，可以照喜好随意装修喔!$R;" +
                                           "您可以让朋友进入，$R;" +
                                           "或指定一些人进入，$R;" +
                                           "也可以选择不开放。$R;" +
                                           "$P每个人随着自己的爱好。$R;" +
                                           "可以呈现出独一无二的风格哦!$R;" +
                                           "$R还有，$R;" +
                                           "飞空庭只能在指定地点降下呀!$R;" +
                                           "$R只能在有飞空庭专用的「机场」，$R;" +
                                           "才能降下呢!$R;" +
                                           "$P其实这里是不允许降下的。$R;" +
                                           "$R为了给您看看，才破例的喔!$R;" +
                                           "$P进房子里看一看吧?$R;" +
                                           "$R如果您想到下一阶段的话，$R;" +
                                           "跟我说一声就行了!$R;", "玛莎");
                    break;

                case 3:
                    break;
            }
        }
    }
}
