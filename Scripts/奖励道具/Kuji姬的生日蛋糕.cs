
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S950000056 : Event
    {
        public S950000056()
        {
            this.EventID = 950000056;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 950000056) >= 1)
            {
                TakeItem(pc, 950000056, 1);
                奖励(pc);
                Say(pc, 0, "尝一口……试试看。");
                ShowEffect(pc, 8019);
                Wait(pc, 800);
                ShowEffect(pc, 6001);
                Say(pc, 0, "你感到头昏目眩……？");
                Wait(pc, 2000);
                Say(pc, 0, "怎么样，蛋糕好吃吗？", "Kuji姬");
                Say(pc, 0, "咦？你的脸色怎么看上去不太好？","Kuji姬");
                Wait(pc, 500);
                ShowEffect(pc, 6041);
                Wait(pc, 1000);
                Say(pc, 0, "这是番茄酱特意为大家做的蛋糕哦！$R她好不容易才做出来的呢！", "Kuji姬");
                Say(pc, 0, "呃……这个蛋糕……$R味道相当独特……$R嗯，还不错……", pc.Name);
                Say(pc, 0, "真的吗，太好了，那么再请你吃一块吧！！", "Kuji姬");
                ShowEffect(pc, 5456);
                Wait(pc, 3000);
                ShowEffect(pc, 5463);
                Say(pc, 0, "你感到彷佛有什么东西碎掉了……。");
            }
        }
        void 奖励(ActorPC pc)
        {
            GiveItem(pc, 951000000, (ushort)Global.Random.Next(5, 10));//地雷币
            GiveItem(pc, 950000025, (ushort)Global.Random.Next(10, 20));//kuji币
            GiveItem(pc, 950000043, 1);
            GiveItem(pc, 910000116, 1);
            GiveItem(pc, 950000057, 1);
        }
    }
}

