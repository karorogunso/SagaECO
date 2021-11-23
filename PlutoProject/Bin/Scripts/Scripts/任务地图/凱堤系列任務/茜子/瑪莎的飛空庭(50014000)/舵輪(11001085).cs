using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50014000
{
    public class S11001085 : Event
    {
        public S11001085()
        {
            this.EventID = 11001085;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11001226, 131, "啊啊$R危险啊!不要碰舵盘！$R;");
            玛莎(pc);
        }


        void 玛莎(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            Say(pc, 11001226, 131, "刚才谢谢了！$R;" +
                "$R听不太懂机器的语言$R我想你累了吧…?$R;");
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.了解玛莎和埃米尔的关系) &&
                !Neko_05_cmask.Test(Neko_05.进入光塔))
            {
                Neko_05_cmask.SetValue(Neko_05.了解到需要记忆体, true);
                Say(pc, 11001221, 131, "我是莉塔的儿子！石像「哈利路亚」！$R;" +
                    "$R想问关于飞空庭引擎的问题！$R;");
                Say(pc, 11001226, 131, "想问什么！?$R;" +
                    "$R我可以回答的都会告诉你的$R;");
                Say(pc, 0, 131, "哈利路亚和玛莎开始了对话…$R;");
                Say(pc, 11001226, 131, "嗯哼$R;" +
                    "$R跟我的飞空庭的『推进器飞行帆』$R类型不同呢$R;" +
                    "$R其他使用中的部件好像也没问题啊$R使用那调整…飞行时可以很高速呢…$R;");
                Say(pc, 11001221, 131, "嗯！我也这么想！$R;");
                Say(pc, 11001226, 131, "啊！莫非…$R;" +
                    "$R说不定电脑的设定有问题…！！$R;");
                Say(pc, 11001221, 131, "设定??$R;" +
                    "你是说涡轮引擎的设定吗？$R;");
                Say(pc, 11001226, 131, "嗯！$R;" +
                    "$R听说如果涡轮的设定不对$R即使组装的引擎再好也不能发动！$R;" +
                    "$P把电脑只读记忆体换掉吧$R看看会不会好点啊…$R;" +
                    "$R可是把那个放在手里…太困难了$R;" +
                    "$R虽然我是拜托商人行会成员拿来的♪$R;");
                Say(pc, 11001221, 131, "那电脑之读记忆体去哪里可以拿到啊?$R;" +
                    "$R请你带我去吧！$R;");
                Say(pc, 11001226, 131, "虽然没关系，可是……$R;");
                Say(pc, 0, 131, "玛莎往这里看…$R;" +
                    "$R好像有什么不好的感觉…$R;");
                Say(pc, 11001226, 131, "在光之塔里！$R;");
                Say(pc, 11001221, 131, "出发到光之塔！！$R;" +
                    "$R玛莎冰♪$R;");
                Say(pc, 11001226, 131, "知道，长官♪$R;" +
                    "$R冰是…$R;");
                Say(pc, 0, 131, "稍…等！稍等！！等一下！！$R;" +
                    "$R啊啊啊，是真的！?$R;");
                PlaySound(pc, 2438, false, 100, 50);
                Wait(pc, 1000);
                Warp(pc, 20146001, 199, 144);
                return;
            }
        }
    }
}