using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞大陸廢墟(50032000) NPC基本信息:微微(13000165) X:100 Y:25
namespace SagaScript.M50032000
{
    public class S13000165 : Event
    {
        public S13000165()
        {
            this.EventID = 13000165;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (pc.CInt["Country"])
            {
                case 0:
                    台灣地區初心者任務(pc);
                    break;

                case 1:
                    日本地區初心者任務(pc);
                    break;
            }
        }

        void 台灣地區初心者任務(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = pc.CMask["Beginner_01"];

            byte x, y;

            if (pc.CInt["NewQuestHack"] == 1)
            {
                Say(pc, 131, "您怎么还在这里？？$R;");
                return;
            }
            if (Beginner_01_mask.Test(Beginner_01.離開阿高普路斯市))
            {
                Say(pc, 13000165, 132, "有没有受伤?$R;" +
                                       "$R幸好…$R;" +
                                       "$R这里是…$R;" +
                                       "几百年前的阿克罗尼亚大陆，$R;" +
                                       "$R在破坏埃米尔世界的$R;" +
                                       "那最后一场战役中…$R;", "蒂塔");

                Say(pc, 13000165, 131, "战争后，$R;" +
                                       "埃米尔世界长时间$R;" +
                                       "处于混乱和停滞状态。$R;" +
                                       "$P不用担心，$R;" +
                                       "$R您将要迎接『我们的时代』的$R;" +
                                       "阿克罗波利斯，$R;" +
                                       "会再一次和平和繁荣的。$R;", "蒂塔");

                Say(pc, 13000165, 132, "但是…危险并没有完全消失。$R;" +
                                       "$R『他们』…$R;" +
                                       "侵略者的进攻还在进行呀!$R;" +
                                       "$P战胜『他们』的威胁吧…$R;" +
                                       "$R不用担心，您一定会做到的!!$R;", "蒂塔");

                Say(pc, 13000165, 132, "用不了多久…$R;" +
                                       "充满危险的厄运会降临这里。$R;" +
                                       "$R来…我会把您送到$R;" +
                                       "我们原来的时代的。$R;" +
                                       "$R下次再见!!$R;" +
                                       "$P现在是活动期间，$R;" +
                                       "如果你选择『继续听故事』$R;" +
                                       "$P我会给你一个意外的惊喜哟!$R;", "蒂塔");

                switch (Select(pc, "怎么办呢?", "", "继续听故事", "直接去阿克罗波利斯"))
                {
                    case 1:
                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10066307, 1);

                        Say(pc, 13000165, 131, "现在给您的礼物，$R;" +
                                               "是让您可以匹敌您接下来那个世界的$R;" +
                                               "侵略者，而特地送给您的小礼物。$R;" +
                                               "$P千万不要把它掉在地上喔!$R;" +
                                               "$R如果觉得不再需要它的时候，$R;" +
                                               "请把它扔在附近的垃圾桶哦。$R;", "蒂塔");

                        pc.CInt["NewQuestHack"] = 1;
                        pc.QuestRemaining += 5;
                        x = (byte)Global.Random.Next(199, 204);
                        y = (byte)Global.Random.Next(64, 68);
                        Warp(pc, 10018102, x, y);
                        break;

                    case 2:
                        pc.CInt["NewQuestHack"] = 1;
                        pc.QuestRemaining += 5;
                        x = (byte)Global.Random.Next(245, 250);
                        y = (byte)Global.Random.Next(126, 131);
                        Warp(pc, 10023100, x, y);
                        break;
                }
            }
        }

        void 日本地區初心者任務(ActorPC pc)
        {
            Say(pc, 132, "どこもお怪我はありませんか？$R;" +
            "$Rそう、よかった…。$R;" +
            "$Pここは…$R数百年前のアクロニア大陸。$R;" +
            "$Rエミルの世界を破壊しつくした$Rあの戦争の…最後の戦いの時……。$R;", "ティタ");

            Say(pc, 131, "戦争のあとエミルの世界は$R長い混乱と停滞の時代が続きました。$R;" +
            "$P心配しないで、$R;" +
            "$Rあなたがこれから向かう$R「あたしたちの時代」のアクロポリスは$R平和と繁栄を取り戻していますわ。$R;", "ティタ");

            Say(pc, 132, "ただ…脅威が$R完全に去ったわけではありません。$R;" +
            "$R「彼ら」…次元侵略者の侵攻は$R今でも深く静かに進行しています。$R;" +
            "$P本当の脅威を聡明な心で照らし出して。$R;" +
            "$R大丈夫、$Rあなたならきっと出来ますわ。$R;", "ティタ");

            Say(pc, 132, "もうすぐここにも$R恐ろしい瘴気が降ってきます。$R;" +
            "$Rさあ…もとの、あたしたちの時代に$Rティタがお送りしますわ。$R;" +
            "$R遠からずまたお目にかかりましょうね。$R;", "ティタ");

            ShowEffect(pc, 4023);
            Wait(pc, 1980);

            if (pc.Race == PC_RACE.EMIL)
            {
                Warp(pc, 30090002, 2, 2);
                pc.QuestRemaining += 5;
            }

            if (pc.Race == PC_RACE.TITANIA)
            {
                Warp(pc, 30140000, 12, 15);
                pc.QuestRemaining += 5;
            }

            if (pc.Race == PC_RACE.DOMINION)
            {
                Warp(pc, 30141000, 11, 17);
                pc.QuestRemaining += 5;
            }
        }
    }
}
