using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50080000
{
    public class S11002163 : Event
    {
        public S11002163()
        {
            this.EventID = 11002163;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<DEMNewbie> newbie = new BitMask<DEMNewbie>(pc.CMask["DEMNewbie"]);
            if (!newbie.Test(DEMNewbie.领取EP))
            {
                do
                {
                    Say(pc, 131, "起来了吗？、$R;" +
                    "MS-67$R;" +
                    "$P为了使你更进一步认识自己$R;" +
                    "你需要教育。$R;" +
                    "$P我被把你的教育委托了$R;" +
                    "型号为「ＤＥＭ－ＮＳ４４１０」的同伴。$R;" +
                    "$R首先先介绍我们的种族$R;" +
                    "留心听吧。$R;", "ＤＥＭ－ＮＳ４４１０");

                    Say(pc, 131, "我们这个地方的居民$R;" +
                    "是被称为「ＤＥＭ」的人型武器。$R;" +
                    "$P被「首脑」统一起来把我们的意志、$R;" +
                    "命令攻击这个地方。$R;" +
                    "$P多米尼翁跟有生命的种族、$R;" +
                    "对我们而言都是敌人。$R;" +
                    "$P除此之外、$R;" +
                    "还有埃米尔族和泰达尼亚族、$R;" +
                    "这些认识到多米尼翁的种族。$R;" +
                    "$R到这里能理解吗？$R;", "ＤＥＭ－ＮＳ４４１０");
                }
                while (Select(pc, "怎么办？", "", "再听一次", "听下一句") != 2);

                pc.EP++;
                newbie.SetValue(DEMNewbie.领取EP, true);
            }
            if (pc.CL < 10 && pc.DominionCL < 10)
            {
                Say(pc, 131, "好。$R;" +
                "$P那么你就立刻接受简单的训练$R;" +
                "并且马上到战场来。$R;" +
                "$R那就是制造你的理由、$R;" +
                "生存的理由。$R;" +
                "$P…在这之前、$R;" +
                "先告诉你成长状态的教学$R;" +
                "$P现在打开的视窗、$R;" +
                "是「成本限制视窗」。$R;" +
                "$P我们不是活动体。$R;" +
                "$R因此，就算会成长却依然会有限制。$R;" +
                "我们是用著一种叫「EP」$R;" +
                "$P的东西来让我们成长。$R;" +
                "消费「EP」。$R;" +
                "$P就能让自己的能力上升、$R;" +
                "$R当「成本限制」达到一定程度时$R;" +
                "$R你自己就会升级。$R;" +
                "$P还有的是「DEMIC」栏$R;" +
                "以后再说明吧。$R;" +
                "$P使用了的「EP」$R;" +
                "会提高「成长界限」。$R;" +
                "$P按「EP消费」按键、$R;" +
                "输入想消耗的EP数量。$R;" +
                "$R确认好后，按确认$R;" +
                "能力就会上升。$R;", "ＤＥＭ－ＮＳ４４１０");

                DEMCL(pc);
                return;
            }
            else
            {
                Say(pc, 131, "成本限制$R;" +
                "好象上升了。$R;" +
                "$P那就準备下一个解说吧。$R;" +
                "$R前往下一个地方吧。$R;", "ＤＥＭ－ＮＳ４４１０");
                int oldMap = pc.CInt["Beginner_Map"];
                pc.CInt["Beginner_Map"] = CreateMapInstance(50081000, 10023100, 250, 132);
                LoadSpawnFile(pc.CInt["Beginner_Map"], "DB/Spawns/50081000.xml");
                Warp(pc, (uint)pc.CInt["Beginner_Map"], 10, 14);

                DeleteMapInstance(oldMap);
            }
        }
    }
}