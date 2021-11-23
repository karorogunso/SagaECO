using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50084000
{
    public class S11002173 : Event
    {
        public S11002173()
        {
            this.EventID = 11002173;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);

            if (!newbie.Test(DEMNewbie.第一次跟迷之女性说话))
            {
                NPCMove(pc, 11002173, -10050, -10550, 315, MoveType.NONE);
                Say(pc, 131, "咦..？那家伙找到新的东西....$R;" +
                "$P虽然是DEM族却被DEM追捕着...$R;" +
                "$P最近....、$R;" +
                "听说了很多「例外」的存在。$R;" +
                "$R嘛...对我来说都是好事...$R;", "迷之少女");

                if (Select(pc, "……", "", "迷之少女", "多米尼翁少女") == 2)
                {
                    Say(pc, 131, "多米尼翁少女的事情？$R;" +
                    "$P如果是这样的话、$R;" +
                    "它的心应该是出故障了。$R;" +
                    "$P如果是你的话$R;" +
                    "作为多米尼翁的盟友$R;" +
                    "守护她吧...。$R;", "迷之少女");
                    NPCMove(pc, 11002173, -10050, -10550, 135, MoveType.NONE);
                    Say(pc, 111, "但是..这个次元断层...不太正常...$R;" +
                    "$P前方那个发光的区域，就是次元断层。$R;" +
                    "经常都会发生时间倾斜。$R;" +
                    "$P倾斜到什么地方都可以前往...$R;" +
                    "但是去到的地方是不知道的$R;" +
                    "$P在这个世界上...$R;" +
                    "$P你是很幸运.....$R;" +
                    "$P你想作为DEM而活的话、$R;" +
                    "就跳下去，$R;" +
                    "$P要么留在这等死。$R;" +
                    "如果你想通过自己的道路前进...$R;" +
                    "就跳进这个次元断层...$R;" +
                    "$R就算不知道是什么地方都好....。$R;" +
                    "$P选择权还是在你身上。$R;" +
                    "$R自由是很可贵的....。$R;", "迷之少女");
                }
            }
            else
            {
                Say(pc, 111, "……。$R;", "迷之少女");
                Say(pc, 0, 65535, "少女尖锐的目光...$R;" +
                "正注视着这个强大的旋祸…。$R;", " ");
            }
        }
    }
}