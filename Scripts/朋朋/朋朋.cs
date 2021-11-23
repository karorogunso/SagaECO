
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using System.Linq;
namespace SagaScript.朋朋
{
    public partial class S50003013 : Event
    {
        public S50003013()
        {
            this.EventID = 50003013;
        }
        class detail
        {
            public string name;
            public int count;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "嘶嘶！(完全听不懂)$R", "朋朋");
            switch (Select(pc, "要做什么呢？", "", "我要挑战你！！(最多5人)", "查看通关时间排名", "离开"))
            {
                case 1:
                    Say(pc, 131, "嘶……$R$R(意思就是：居然想要欺负朋朋！）", "朋朋");
                    挑战朋朋(pc);
                    break;
                case 2:
                    Dictionary<string, int> d = new Dictionary<string, int>();
                    foreach (var item in SDict["朋朋通关_排行榜"])
                        d.Add(item.Key, item.Value);
                    List<detail> 排名 = DictonarySort(d);
                    SStr["未产出"] = "未产出";
                    string s = "————嘶————";
                    s += "$R第一名：" + SStr[排名[0].name] + " - " + 排名[0].count.ToString();
                    s += " $R第二名：" + SStr[排名[1].name] + " - " + 排名[1].count.ToString();
                    s += " $R第三名：" + SStr[排名[2].name] + " - " + 排名[2].count.ToString();
                    s += " $R第四名：" + SStr[排名[3].name] + " - " + 排名[3].count.ToString();
                    s += " $R第五名：" + SStr[排名[4].name] + " - " + 排名[4].count.ToString();
                    s += "$P————嘶————";
                    s += " $R第六名：" + SStr[排名[5].name] + " - " + 排名[5].count.ToString();
                    s += " $R第七名：" + SStr[排名[6].name] + " - " + 排名[6].count.ToString();
                    s += "$R第八名：" + SStr[排名[7].name] + " - " + 排名[7].count.ToString();
                    s += " $R第九名：" + SStr[排名[8].name] + " - " + 排名[8].count.ToString();
                    s += " $R第十名：" + SStr[排名[9].name] + " - " + 排名[9].count.ToString();
                    s += "$P————嘶————";
                    s += " $R第十一名：" + SStr[排名[10].name] + " - " + 排名[10].count.ToString();
                    s += " $R第十二名：" + SStr[排名[11].name] + " - " + 排名[11].count.ToString();
                    s += " $R第十三名：" + SStr[排名[12].name] + " - " + 排名[12].count.ToString();
                    s += " $R第十四名：" + SStr[排名[13].name] + " - " + 排名[13].count.ToString();
                    s += " $R第十五名：" + SStr[排名[14].name] + " - " + 排名[14].count.ToString();
                    Say(pc, 131, "嘶……$R$R" + s, "朋朋");
                    break;
                case 3: break;
            }
        }
        void 挑战朋朋(ActorPC pc)
        {
            if (pc.Party == null)
            {
                Say(pc, 131, "嘶……$R$R(意思就是：一个人还想挑战我!??)", "朋朋");
                return;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 131, "嘶……$R$R(意思就是：叫你们队长来！！)", "朋朋");
                return;
            }
            if (pc.Party.MemberCount > 6)
            {
                Say(pc, 131, "嘶……$R$R(意思就是：你们人这么多，不公平！！)", "朋朋");
                return;
            }
            /*foreach (var item in pc.Party.Members.Values)
            {
                if (item.CStr["朋朋限制"] == DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    Say(pc, 131, item.Name + "$R$R嘶……", "朋朋");
                    return;
                }
            }*/
            foreach (var item in pc.Party.Members.Values)
            {
                /*if (item.Gold < 2000000)
                {
                    Say(pc, 131, item.Name + "钱不够！！$R$R嘶……", "朋朋");
                    return;
                }*/
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (!item.Online && item.MapID != 10054000)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("有玩家不在这里，进入取消");
                    return;
                }
                foreach (var item2 in pc.Party.Members.Values)
                    SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("等待玩家" + item.Name + " 接受中..");
                if (Select(item, "你的队长申请了挑战朋朋，是否接受？", "", "接受", "不接受！！") == 1)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "接受了挑战朋朋");
                }
                else
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "拒绝了挑战朋朋，进入取消。");
                    return;
                }
            }

            pc.Party.Leader.TInt["S60913000"] = CreateMapInstance(60913000, 91000999, 21, 21, true, 0, true);//战斗点
            刷朋朋(pc.Party.Leader, (uint)pc.Party.Leader.TInt["S60913000"]);
            pc.Party.MaxMember = (uint)pc.Party.MemberCount;
            if (pc.Party.MaxMember > 6) return;
            foreach (var item in pc.Party.Members.Values)
            {
                item.CStr["朋朋限制"] = DateTime.Now.ToString("yyyy-MM-dd");
                item.TInt["副本复活标记"] = 1;
                item.Party.Leader.TInt["复活次数"] = 12;
                item.Party.Leader.TInt["设定复活次数"] = 12;
                item.Party.TTime["朋朋时间"] = DateTime.Now;
                Warp(item, (uint)pc.Party.Leader.TInt["S60913000"], 41, 84);
            }
        }
        private List<detail> DictonarySort(Dictionary<string, int> dic)
        {
            List<detail> 排名 = new List<detail>();
            var dicSort = from objDic in dic orderby objDic.Value ascending select objDic;
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                detail d = new detail();
                d.name = kvp.Key;
                d.count = kvp.Value;
                排名.Add(d);
            }
            if (排名.Count < 15)
            {
                for (int i = 排名.Count; i < 15; i++)
                {
                    detail d = new detail();
                    d.name = "未产出";
                    d.count = 99999999;
                    排名.Add(d);
                }
            }
            return 排名;
        }
    }
}

