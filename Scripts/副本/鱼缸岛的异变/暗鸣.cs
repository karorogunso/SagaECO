
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
    public partial class 暗鸣 : Event
    {
        public 暗鸣()
        {
            this.EventID = 60000007;
            this.leastQuestPoint = 1;

            this.alreadyHasQuest = "任務進行的還順利嗎?$R;";

            this.gotNormalQuest = "那就拜託了。$R;" +
                                  "$R等任務完成以後，再來找我吧。$R;";

            this.gotTransportQuest = "是阿，道具太重了吧?$R;" +
                                     "$R所以不能一次傳送的話，$R;" +
                                     "分成幾次給我也可以的。$R;";

            this.questCompleted = "真是辛苦了。$R;" +
                                  "$R恭喜你，任務完成了。$R;" +
                                  "$P來! 收下報酬吧!$R;";

            this.transport = "哦哦…全部都收集好了嗎?$R;";

            this.questCanceled = "嗯…如果是你，我相信你能做到的，$R;" +
                                 "很期待呢……$R;";

            this.questFailed = "……$R;" +
                               "$P失敗了?$R;" +
                               "$R真是鬧了大事，$R;" +
                               "不知道該說什麼好啊?$R;" +
                               "$P這次實在沒辦法了，$R;" +
                               "下次一定要成功啊!$R;" +
                               "$R知道了吧?$R;";

            this.notEnoughQuestPoint = "嗯…$R;" +
                                       "$R現在沒有要特別拜託的事情啊，$R;" +
                                       "再去冒險怎麼樣?$R;";

            this.questTooHard = "這對你來說有點困難啊?$R;" +
                                "$R這樣也沒關係嘛?$R;";
        }

        public override void OnEvent(ActorPC pc)
        {
            DateTime endDT = new DateTime(2017, 2, 12);//设置结束日期为2017年2月12日
            if (DateTime.Now < endDT)
            {
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 0)//此段代码为暗鸣所用
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "新年新气象！鱼缸团万岁！！", "暗鸣");
                    Say(pc, 0, "这位朋友你好，为我们的友谊干杯！", "暗鸣");
                    Select(pc, " ", "", "EXO ME？？？？？？", "一脸懵逼");
                    Say(pc, 0, "什么？$R你不是要当我们鱼缸团的干部吗？", "暗鸣");
                    Say(pc, 0, "既然如此！那我就来考考你好了！", "暗鸣");
                    Say(pc, 0, "到底你有没有资格成为我鱼缸团干部呢！", "暗鸣");
                    Say(pc, 0, "撒，让我们拭目以待吧哈哈哈哈哈！", "暗鸣");
                    Select(pc, " ", "", "EXO YOU？？？？？？", "二脸懵逼");
                    Say(pc, 0, "这位选手请听题：", "暗鸣");
                    Say(pc, 0, "下列哪只怪物是刷在$R鱼缸团城塞外围的小型boss的？", "暗鸣");
                    while (Select(pc, "你的回答是……", "", "你兔汉一", "他兔汉二", "我兔汉三", "谁兔汉四？") != 3)
                    {
                        Say(pc, 0, "这你都答错！？算了再给你个机会！", "暗鸣");
                    }
                    Say(pc, 0, "嚯嚯，看来你平时还是有在肝的！", "暗鸣");
                    pc.CInt["CC新春活动 暗鸣"] = 1;
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 1)
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "那么请听下一题！", "暗鸣");
                    Say(pc, 0, "下列哪只怪物不属于哞哞草原？", "暗鸣");
                    while (Select(pc, "你的回答是……", "", "梅花猪", "咕咕", "尼德鸟蛋", "星妈") != 4)
                    {
                        Say(pc, 0, "这你都答错！？算了再给你个机会！", "暗鸣");
                    }
                    Say(pc, 0, "星妈不想跟你说话$R并疯狂的对你嘎哦嘎哦嘎哦", "暗鸣");
                    pc.CInt["CC新春活动 暗鸣"] = 2;
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 2)
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "那么请听下一题！", "暗鸣");
                    Say(pc, 0, "歌词【在那遥远的地方，有位老流氓】$R其中老流氓指的是？", "暗鸣");
                    while (Select(pc, "你的回答是……", "", "阿凡提", "买买提", "阿凡达", "番茄") != 4)
                    {
                        Say(pc, 0, "这你都答错！？算了再给你个机会！", "暗鸣");
                    }
                    Say(pc, 0, "据说黑番茄是新年任务的保留节目。", "暗鸣");
                    pc.CInt["CC新春活动 暗鸣"] = 3;
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 3)
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "那么请听下一题！", "暗鸣");
                    Say(pc, 0, "学习移动施法的NPC名字叫做！？", "暗鸣");
                    while (Select(pc, "你的回答是……", "", "百铜·春影", "千金·夏影", "万银·秋影", "亿钻·冬影") != 2)
                    {
                        Say(pc, 0, "这你都答错！？算了再给你个机会！", "暗鸣");
                    }
                    Say(pc, 0, "嘻嘻，$R老夏忙着文明6一定不会看到这道题目。", "暗鸣");
                    pc.CInt["CC新春活动 暗鸣"] = 4;
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 4)
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "那么请听最终问题！", "暗鸣");
                    Say(pc, 0, "在当今社会上，$R随着越来越多的反物理现象的发生，$R牛顿已经按捺不住自己内心的躁动想撑开棺材板旋转跳跃我闭着眼地出现在大家面前。$R已知牛顿本人在棺材内运用洪荒之力让棺材板以45度角方向，36m/s的初速度腾空而起，$R碰巧正对面的爱因斯坦也以45度角，但是却是360m/s的初速度发射棺材，$R可能因为他会相对论，所以就很快。$R假设两人的棺材板质量，外形完全相同，$R问：", "暗鸣");
                    Say(pc, 0, "下列哪只搭档属于天宫希$R任务中可以获得的？", "暗鸣");
                    while (Select(pc, "你的回答是……", "", "皮噜噜·皇阿玛", "皮鲁鲁·阿露玛", "皮露露·阿鲁玛", "皮鹿鹿·沙琪玛") != 3)
                    {
                        Say(pc, 0, "这你都答错！？算了再给你个机会！", "暗鸣");
                    }
                    Say(pc, 0, "啊！！这都被你答对了！！", "暗鸣");
                    pc.CInt["CC新春活动 暗鸣"] = 5;
                }
                if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 5)
                {
                    ChangeMessageBox(pc);
                    Say(pc, 0, "这位年轻人，看来你很有成为鱼缸团干部的能力！", "暗鸣");
                    Say(pc, 0, "那么我就把象征鱼缸团干部的钛合金鱼缸送给你吧！", "暗鸣");
                    Select(pc, " ", "", "不要，快滚");
                    Say(pc, 0, "哦哦哦！？居然不要！有骨气！我欣赏你！", "暗鸣");
                    Say(pc, 0, "那我就什么都不给你了，嘻嘻。", "暗鸣");
                    Say(pc, 0, "……", "暗鸣");
                    Say(pc, 0, "你真的不要吗？", "暗鸣");
                    Select(pc, " ", "", "不要，快滚");
                    Say(pc, 0, "……", "暗鸣");
                    Say(pc, 0, "（暗呜强行给你塞了一个红包）");
                    GiveItem(pc, 910000115, 1);//红包
                    pc.CInt["CC新春活动 暗鸣"] = 6;//该part的最终标记
                    Say(pc, 0, "哈哈哈哈！那么我要去为了鱼缸团的明天锻炼身体了！橙！我们走！", "暗鸣");
                    Say(pc, 0, "（好像忘记问卡片的事了……）", pc.Name);
                    Say(pc, 0, "（……去认识认识其他参赛者吧）", pc.Name);
                    if (pc.CInt["CC新春活动"] == 1 && pc.CInt["CC新春活动 兔麻麻"] == 2 && pc.CInt["CC新春活动 番茄茄"] == 3 && pc.CInt["CC新春活动 天宫希"] == 3 && pc.CInt["CC新春活动 沙月"] == 1 && pc.CInt["CC新春活动 夏影"] == 1 && pc.CInt["CC新春活动 天天"] == 1 && pc.CInt["CC新春活动 暗鸣"] == 6)
                    {
                        ChangeMessageBox(pc);
                        Say(pc, 0, "似乎把参赛者都认识完了呢……", pc.Name);
                        Say(pc, 0, "去找那个奇怪的新年c看看好了", pc.Name);
                        pc.CInt["CCHelloComplete"] = 1;
                        return;
                    }
                    return;
                }
            }

            if (pc.CInt["第一次和暗鸣对话"] != 1)
            {
                Say(pc, 0, "在下就是鱼缸团团长，$R暗鸣是也！$R$R哦哦哦！！！$R你是加入鱼缸团的吗？$R$R（暗鸣向你丢来了一个鱼缸...）", "暗鸣");
                pc.CInt["第一次和暗鸣对话"] = 1;
                GiveItem(pc, 50029950, 1);
                Select(pc, "……", "", "卧槽...");
                Say(pc, 0, "这是我们身份的象征！$R$R怎么样，很酷炫对不对！！！！", "暗鸣");
                Say(pc, 0, "那么！！$R$R从今以后你就是鱼缸团团员了！！", "暗鸣");
                Select(pc, "……", "", "难怪这个奇怪的团这么缺人...");
                Select(pc, "……", "", "说起来我为什么要加入这个奇怪的团啊！？");
                Say(pc, 0, "嗯？$R$R你似乎被我看出你一脸疑惑！！", "暗鸣");
                Say(pc, 0, "大侠！！我看你一表人才！！$R$R像你这样的人我暗鸣绝对不会放过的！", "暗鸣");
                Select(pc, "……", "", "好吧……那么，需要我干什么嘛？");
                Say(pc, 0, "希望你能为鱼缸团的壮大、$R$R随时贡献出你的一份力量！！！！", "暗鸣");
                return;
            }
            if (pc.Level < 45)
            {
                Say(pc, 0, "嗯？$R我看你现在还不够强大！$R$R等你达到45级再来怎么样！！", "暗鸣");
                return;
            }
            switch (Select(pc, "请选择", "", "[副本]鱼缸后岛的异变", "[委托]副本委托清单", "中途进入副本[5点任务点]","离开"))
            {
                case 1:
                    Say(pc, 0, "啊！你们！$R你们现在看起来很闲啊", "暗鸣");
                    Say(pc, 0, "事实上，$R我这边有些麻烦事需要你们的帮助。", "暗鸣");
                    if (Select(pc, "??", "", "什么事呢", "没有，快滚") == 2) return;
                    Say(pc, 0, "帮大忙了，$R虽然说这本来应该是$R我们自行处理的事，$R$R但是最近岛上要忙的事太多，$R实在是人手不足。", "暗鸣");
                    Say(pc, 0, "我就长话短说了，$R鱼缸岛附近的2座离岛周围$R出现了奇怪的生物", "暗鸣");
                    Say(pc, 0, "放着不管的话$R可能会威胁到鱼缸岛上的居民，$R$R所以希望你们能$R跟我和疾风队长前去探查一下", "暗鸣");
                    switch (Select(pc, "请选择难度", "", "单人[最大1人，需要任务点20点]", "普通组队[最大4人，需要10任务点]", "离开"))
                    {
                        case 1:
                            鱼缸后岛的异变普单人(pc);
                            break;
                        case 2:
                            鱼缸后岛的异变普通(pc);
                            break;
                    }
                    break;
                case 2:
                        HandleQuest(pc, 3);
                    return;
                case 3:
                    if (pc.Party == null) return;
                    if (pc.Party.Leader == null) return;
                    if (pc.Party.Leader == pc)
                    {
                        Say(pc,0,"呃。。。$R如果是队长出来的话，$R就不能再进去了。", "暗鸣");
                        return;
                    }
                    else
                    {
                        uint mapid = pc.Party.Leader.MapID;
                        if(mapid == pc.Party.Leader.TInt["S10054100"] || mapid == pc.Party.Leader.TInt["S20004000"] || mapid == pc.Party.Leader.TInt["S20003000"]
                            || mapid == pc.Party.Leader.TInt["S20002000"] || mapid == pc.Party.Leader.TInt["S20001000"] || mapid == pc.Party.Leader.TInt["S20000000"]
                            　|| mapid == pc.Party.Leader.TInt["S30131002"])
                        {
                            if(Select(pc,"中途进入会重置当前BOSS血量，是否进入？","","是的，带我进去[消耗5任务点]","还是等一下吧..") == 1)
                            {
                                if (pc.QuestRemaining < 5)
                                {
                                    Say(pc, 0, "任务点不足");
                                    return;
                                }
                                if (pc.Party == null) return;
                                if (pc.Party.Leader == null) return;
                                if (mapid == pc.Party.Leader.TInt["S10054100"] || mapid == pc.Party.Leader.TInt["S20004000"] || mapid == pc.Party.Leader.TInt["S20003000"]
    || mapid == pc.Party.Leader.TInt["S20002000"] || mapid == pc.Party.Leader.TInt["S20001000"] || mapid == pc.Party.Leader.TInt["S20000000"]
    || mapid == pc.Party.Leader.TInt["S30131002"])
                                {
                                    pc.QuestRemaining -= 5;
pc.TInt["副本复活标记"] = 1;
                                    mapid = pc.Party.Leader.MapID;
                                    SagaMap.Map map = SagaMap.Manager.MapManager.Instance.GetMap(mapid);
                                    Warp(pc, mapid, Global.PosX16to8(pc.Party.Leader.X, map.Width), Global.PosY16to8(pc.Party.Leader.Y, map.Height));

                                    List<Actor> actors = new List<Actor>();
                                    foreach (var a in map.Actors)
                                        actors.Add(a.Value);
                                    foreach (var item in actors)
                                    {
                                        if (item != null)
                                            if (item.type == ActorType.MOB)
                                                if (!item.Buff.Dead)
                                                    item.HP = item.MaxHP;
                                    }
                                }
                            }
                        }
                    }
                    return;
            }
        }
        void senddigtomember(ActorPC pc, ushort id)
        {
            if (pc.Party != null)
                if (pc.Party.Leader != null)
                    if (pc.MapID == pc.Party.Leader.MapID)
                        foreach (var item in pc.Party.Members.Values)
                            ShowDialog(item, id);
        }
    }
}