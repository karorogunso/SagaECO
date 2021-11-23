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
            Say(pc, 11001226, 131, "啊啊$R危險啊!不要碰舵盤！$R;");
            玛莎(pc);
        }


        void 玛莎(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            Say(pc, 11001226, 131, "剛才謝謝了！$R;" +
                "$R聽不太懂機器的語言$R我想你累了吧…?$R;");
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.了解玛莎和埃米尔的关系) &&
                !Neko_05_cmask.Test(Neko_05.进入光塔))
            {
                Neko_05_cmask.SetValue(Neko_05.了解到需要记忆体, true);
                Say(pc, 11001221, 131, "我是莉塔的兒子！石像「哈爾列爾利」！$R;" +
                    "$R想問關於飛空庭引擎的問題！$R;");
                Say(pc, 11001226, 131, "想問什麼！?$R;" +
                    "$R我可以回答的都會告訴你的$R;");
                Say(pc, 0, 131, "哈爾列爾利和瑪莎開始了對話…$R;");
                Say(pc, 11001226, 131, "嗯哼$R;" +
                    "$R跟我的飛空庭的『推進器飛行帆』$R類型不同呢$R;" +
                    "$R其他使用中的部件好像也沒問題啊$R使用那調整…飛行時可以很高速呢…$R;");
                Say(pc, 11001221, 131, "嗯！我也這麼想！$R;");
                Say(pc, 11001226, 131, "啊！莫非…$R;" +
                    "$R說不定電腦的設定有問題…！！$R;");
                Say(pc, 11001221, 131, "設定??$R;" +
                    "你是說渦輪引擎的設定嗎？$R;");
                Say(pc, 11001226, 131, "嗯！$R;" +
                    "$R聽說如果渦輪的設定不對$R即使組裝的引擎再好也不能發動！$R;" +
                    "$P把電腦唯讀記憶體換掉吧$R看看會不會好點啊…$R;" +
                    "$R可是把那個放在手裡…太困難了$R;" +
                    "$R雖然我是拜託商人行會成員拿來的♪$R;");
                Say(pc, 11001221, 131, "那電腦唯讀記憶體去哪裡可以拿到啊?$R;" +
                    "$R請你帶我去吧！$R;");
                Say(pc, 11001226, 131, "雖然沒關係，可是……$R;");
                Say(pc, 0, 131, "瑪莎往這裡看…$R;" +
                    "$R好像有什麼不好的感覺…$R;");
                Say(pc, 11001226, 131, "在光之塔裡！$R;");
                Say(pc, 11001221, 131, "出發到光之塔！！$R;" +
                    "$R瑪莎冰♪$R;");
                Say(pc, 11001226, 131, "知道，長官♪$R;" +
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