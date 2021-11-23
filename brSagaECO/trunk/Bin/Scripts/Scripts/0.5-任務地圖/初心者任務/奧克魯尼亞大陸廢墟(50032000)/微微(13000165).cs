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
                Say(pc, 131, "您怎麼還在這裡？？$R;");
                return;
            }
            if (Beginner_01_mask.Test(Beginner_01.離開阿高普路斯市))
            {
                Say(pc, 13000165, 132, "有沒有受傷?$R;" +
                                       "$R幸好…$R;" +
                                       "$R這裡是…$R;" +
                                       "幾百年前的奧克魯尼亞大陸，$R;" +
                                       "$R在破壞埃米爾世界的$R;" +
                                       "那最後一場戰役中…$R;", "微微");

                Say(pc, 13000165, 131, "戰爭後，$R;" +
                                       "埃米爾世界長時間$R;" +
                                       "處於混亂和停滯狀態。$R;" +
                                       "$P不用擔心，$R;" +
                                       "$R您將要迎接『我們的時代』的$R;" +
                                       "阿高普路斯市，$R;" +
                                       "會再一次和平和繁榮的。$R;", "微微");

                Say(pc, 13000165, 132, "但是…危險並沒有完全消失。$R;" +
                                       "$R『那些傢伙們』…$R;" +
                                       "侵略者的進攻還在進行呀!$R;" +
                                       "$P戰勝『那些傢伙們』的威脅吧…$R;" +
                                       "$R不用擔心，您一定會做到的!!$R;", "微微");

                Say(pc, 13000165, 132, "用不了多久…$R;" +
                                       "充滿危險的惡運會降臨這裡。$R;" +
                                       "$R來…我會把您送到$R;" +
                                       "我們原來的時代的。$R;" +
                                       "$R下次再見!!$R;" +
                                       "$P現在是活動期間，$R;" +
                                       "如果你選擇『繼續聽故事』$R;" +
                                       "$P我會給你一個意外的驚喜喲!$R;", "微微");

                switch (Select(pc, "怎麼辦呢?", "", "繼續聽故事", "直接去阿高普路斯"))
                {
                    case 1:
                        PlaySound(pc, 2040, false, 100, 50);
                        GiveItem(pc, 10066307, 5);

                        Say(pc, 13000165, 131, "現在給您的禮物，$R;" +
                                               "是讓您可以匹敵您接下來那個世界的$R;" +
                                               "侵略者，而特地送給您的小禮物。$R;" +
                                               "$P千萬不要把它掉在地上喔!$R;" +
                                               "$R如果覺得不再需要它的時候，$R;" +
                                               "請把它掉在附近的垃圾桶唷。$R;", "微微");

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
