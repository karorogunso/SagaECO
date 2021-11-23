
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
using SagaMap.ActorEventHandlers;
namespace 东部地牢副本
{
    public partial class 东部地牢 : Event
    {
        public 东部地牢()
        {
            this.EventID = 80000702;
        }
        public override void OnEvent(ActorPC pc)
        {
            if(pc.Account.GMLevel > 20)
            pc.CInt["东牢进入任务"] = 3;
            SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(pc.MapID);
            /*switch (Select(pc,"GM测试控制台","","进入正常流程", "贪婪领主", "暗黑鬼偶", "腐毒丧尸", "冥王", "夺魂者"))
            {
                case 1:
                    break;
                case 2:
                    ActorMob mobS = map.SpawnCustomMob(10000000, map.ID, 14670200, 0, 10010100, 1, Global.PosX16to8(pc.X,map.Width), Global.PosY16to8(pc.Y, map.Height), 0, 1, 0, 贪婪领主Info(Difficulty.Normal), 贪婪领主AI(Difficulty.Normal), null, 0)[0];
                    mobS.TInt["playersize"] = 1500;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS, false);
                    return;
                case 3:
                    ActorMob mobS2 = map.SpawnCustomMob(10000000, map.ID, 15510200, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 0, 1, 0, 暗黑兔偶Info(Difficulty.Normal), 暗黑兔偶AI(Difficulty.Normal), null, 0)[0];
                    mobS2.TInt["playersize"] = 1500;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS2, false);
                    return;
                case 4:
                    ActorMob mobS3 = map.SpawnCustomMob(10000000, map.ID, 15380600, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 0, 1, 0, 腐毒丧尸Info(Difficulty.Normal), 腐毒丧尸AI(Difficulty.Normal), null, 0)[0];
                    mobS3.TInt["playersize"] = 5000;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS3, false);
                    return;
                case 5:
                    ActorMob mobS4 = map.SpawnCustomMob(10000000, map.ID, 10136900, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 0, 1, 0, S100000105.冥王Info(Difficulty.Normal), S100000105.冥王AI(Difficulty.Normal), null, 0)[0];
                    mobS4.TInt["playersize"] = 2500;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS4, false);
                    return;
                case 6:
                    ActorMob mobS5 = map.SpawnCustomMob(10000000, map.ID, 16010000, 0, 10010100, 1, Global.PosX16to8(pc.X, map.Width), Global.PosY16to8(pc.Y, map.Height), 0, 1, 0, 夺魂者Info(Difficulty.Normal), 夺魂者AI(Difficulty.Normal), null, 0)[0];
                    mobS5.TInt["playersize"] = 1500;
                    map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, mobS5, false);
                    return;
            }*/
            switch (pc.CInt["东牢进入任务"])
            {
                case 0:
                    Say(pc, 0, "……", "小莫");
                    break;
                case 1:
                    Say(pc, 0, "……", "小莫");
                    break;
                case 2:
                    ChangeMessageBox(pc);
                    Say(pc, 0, "你们终于来了，准备好要进入东方地牢了吗？", "小莫");
                    Say(pc, 0, "每耽搁一分钟，病人们的病情都可能会继续恶化，我们得抓紧时间。", "小莫");
                    if (Select(pc, "怎么回答？", "", "准备好了", "再等等") == 1)
                    {
                        Say(pc, 0, "地牢的入口就在后面，我来带路。$R$R准备好了之后，就让队长和我说话吧。", "小莫");
                        pc.CInt["东牢进入任务"] = 3;
                        //副本对话(pc);
                        return;
                    }
                    break;
                case 3:
                    Say(pc, 0, "要前往去东部地牢吗？", "莫库草·阿鲁玛");
                    switch (Select(pc, "……", "", "单人前往（单人模式）", "请带我们去吧（多人模式）", "任务控制台", "不去了"))
                    {
                        case 1:
                            副本对话单人(pc);
                            break;
                        case 2:
                            //Say(pc, 0, "好的", "莫库草·阿鲁玛");
                            副本对话多人(pc);
                            return;
                        case 3:
                            HandleQuest(pc, 6);
                            break;
                    }
                    break;
            }
        }
    }
}