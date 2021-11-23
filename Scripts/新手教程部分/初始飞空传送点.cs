
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S11001019 : Event
    {
        public S11001019()
        {
            this.EventID = 11001019;
        }

        public override void OnEvent(ActorPC pc)
        {
                Say(pc, 11001019, 0, "欢迎来到YggEco Online的世界！", "系统声音");
                Say(pc, 11001019, 0, "这个世界由各种各样危险的区域组成，$R而率先突破最终区域的玩家$R可以获得50亿（？）的现金奖励！", "系统声音");
                Say(pc, 11001019, 0, "然而因为现在是内测版，所以没有奖金的说$R但是等到公测时$R您的进度可是会比别人快很多的哟~", "系统声音");
                Say(pc, 11001019, 0, "那么在开始游戏之前，$R请先设定您在游戏里的职业。", "系统声音");
                switch (Select(pc, "请选择你的职业(进入游戏后可变更)", "", "勇者（擅长近距离作战）", "祭司（擅长光属性魔法和暗属性魔法）"))
                {
                    case 1:
                        ChangePlayerJob(pc, PC_JOB.GLADIATOR);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        Say(pc, 131, "恭喜你$R你已经成为『勇者』了。", "系统声音");
                        PlaySound(pc, 4012, false, 100, 50);
                        break;
                    case 2:
                        ChangePlayerJob(pc, PC_JOB.CARDINAL);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 1000);
                        Say(pc, 131, "恭喜你$R你已经成为『祭司』了。", "系统声音");
                        PlaySound(pc, 4012, false, 100, 50);
                        break;
                }
            Say(pc, 11001019, 0, "设定完毕。$R那么，准备好■■始崭新的■■吗■", "系统声音");
            ShowEffect(pc, 4381);
            Wait(pc, 500);
            Say(pc, 11001019, 0, "那么，$R让■■出发", "系统声音");
            ShowEffect(pc, 5020);
            Wait(pc, 500);
            Say(pc, 11001019, 0, "去『$CR■■■之城$CD』■■”", "系统声音");
            ShowEffect(pc, 5103);
            Wait(pc, 500);
            Say(pc, 11001019, 0, "System", "系统声音");
            ShowEffect(pc, 5441);
            Wait(pc, 800);
            ShowEffect(pc, 5444);
            Wait(pc, 2000);
            ShowEffect(pc, 4312);
            Say(pc, 11001019, 0, "Error", "系统声音");
            Wait(pc, 2000);
            Warp(pc, 21190000, 28, 28);
        }
    }
}