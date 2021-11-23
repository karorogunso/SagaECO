
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using System.Linq;
namespace SagaScript.FF
{
    public class S50003009 : Event
    {
        public S50003009()
        {
            this.EventID = 50003009;
        }
        class detail
        {
           public string name;
           public int count;
        }
        public override void OnEvent(ActorPC pc)
        {
            检查月饼(pc);
            switch(Select(pc,"请问你要干什么呢？","","查看个人收集与全服排名","活动简介","离开"))
            {
                case 1:
                    Dictionary<string, int> d = new Dictionary<string, int>();
                    foreach (var item in SDict["中秋月饼_排行榜"])
                        d.Add(item.Key, item.Value);
                    List<detail> 排名 = DictonarySort(d);
                    string s = "第一名：" + SStr[排名[0].name] + " - " + 排名[0].count.ToString();
                    s += " $R第二名：" + SStr[排名[1].name] + " - " + 排名[1].count.ToString();
                    s += " $R第三名：" + SStr[排名[2].name] + " - " + 排名[2].count.ToString();
                    s += " $R第四名：" + SStr[排名[3].name] + " - " + 排名[3].count.ToString();
                    s += " $R第五名：" + SStr[排名[4].name] + " - " + 排名[4].count.ToString();
                    s += " $R第六名：" + SStr[排名[5].name] + " - " + 排名[5].count.ToString();
                    s += " $R第七名：" + SStr[排名[6].name] + " - " + 排名[6].count.ToString();
                    s += "$P第八名：" + SStr[排名[7].name] + " - " + 排名[7].count.ToString();
                    s += " $R第九名：" + SStr[排名[8].name] + " - " + 排名[8].count.ToString();
                    s += " $R第十名：" + SStr[排名[9].name] + " - " + 排名[9].count.ToString();

                    int count = 0;
                    string name = SStr["中秋月饼_" + pc.Account.AccountID.ToString()];
                    for (int i = 0; i < 排名.Count - 1; i++)
                    {
                        if (SStr[排名[i].name] == name)
                            count = i + 1;
                    }
                    s += "$R$R你当前的排名是：" + count.ToString();

                    Say(pc, 131, "你已经收集了" + pc.AInt["中秋月饼_个人收集"] + "个月饼了$R目前全服收集为：" + SInt["中秋月饼_全服收集"] + "$R$R" + s, "中秋节玉兔");
                    break;
                case 2:
                    Say(pc, 131, "中秋节收集活动！$R活动时间：9月7日-9月18日早上维护$R$R当你的月饼收集达到一定数额，$R可以获得中秋节特别称号！", "中秋节玉兔");
                    Say(pc, 131, "个人收集达到500，获得称号“小月兔”$R个人收集达到2000，获得称号“玉兔”$R个人收集达到15000，获得称号“兔神”$R$R当全服收集达到100000，$R个人收集达到500可获得称号“桂花”$R当全服收集达到200000，$R所有个人收集达到8000可获得称号“月神”", "中秋节玉兔");
                    Say(pc, 131, "个人收集将会在活动结束时结算排名!$R收集排名第一名，获得称号“玉轮”$R第二至第五名，获得称号“玉弓”$R第五至第十名，获得称号“初月”", "中秋节玉兔");
                    Say(pc, 131, "称号属性：$R$R小月兔$R——HP上限增加300，攻击力5点$R玉兔$R——HP、MP、SP上限增加300$R兔神$R——HP、MP、SP上限增加300，攻击力10点、暴击5点", "中秋节玉兔");
                    Say(pc, 131, "称号属性：$R$R桂花$R——使用“圣愈术”时，$R有20%的几率发动AOE恢复$R$R月神$R——自身暴击属性变为0，$R攻击时有1%的几率触发“月神惩戒”$R（造成巨额单体伤害，15秒内触发一次）", "中秋节玉兔");
                    Say(pc, 131, "排名称号属性：$R$R初月$R——HP、MP、SP增加500，攻击力15点$R玉弓$R——HP、MP、SP增加550，攻击力20点$R玉轮$R——HP、MP、SP增加600，攻击力25点$R", "中秋节玉兔");

                    break;
                case 3:
                    break;
            }
        }
        private List<detail> DictonarySort(Dictionary<string, int> dic)
        {
            List<detail> 排名 = new List<detail>();
            var dicSort = from objDic in dic orderby objDic.Value descending select objDic;
            foreach (KeyValuePair<string, int> kvp in dicSort)
            {
                detail d = new detail();
                d.name = kvp.Key;
                d.count = kvp.Value;
                排名.Add(d);
            }
            return 排名;
        }
        void 检查月饼(ActorPC pc)
        {
Say(pc, 131, "中秋活动结束啦，感谢参与！", "中秋节玉兔");
return;
            ushort count = (ushort)CountItem(pc, 950000006);
            if(count > 10000)
            {
                Say(pc, 131, "哎呀，你带的月饼太多了，我拿不动哦", "中秋节玉兔");
            }
            if (count > 0)
            {
                Say(pc, 159, "哇，你带月饼来了呢。", "中秋节玉兔");
                TakeItem(pc, 950000006, count);
                pc.AInt["中秋月饼_个人收集"] += count;
                SInt["中秋月饼_全服收集"] += count;
                SDict["中秋月饼_排行榜"]["中秋月饼_" + pc.Account.AccountID.ToString()] += count;
                SStr["中秋月饼_" + pc.Account.AccountID.ToString()] = pc.Name;
            }
            if(pc.AInt["中秋月饼_个人收集"] >= 500 && pc.AInt["中秋月饼小月兔称号"] != 1)
            {
                pc.AInt["中秋月饼小月兔称号"] = 1;
                GiveItem(pc, 130000002, 1);
            }
            if (pc.AInt["中秋月饼_个人收集"] >= 500 && pc.AInt["中秋月饼玉兔称号"] != 1)
            {
                pc.AInt["中秋月饼玉兔称号"] = 1;
                GiveItem(pc, 130000003, 1);
            }
            if (pc.AInt["中秋月饼_个人收集"] >= 15000 && pc.AInt["中秋月饼兔神称号"] != 1)
            {
                pc.AInt["中秋月饼兔神称号"] = 1;
                GiveItem(pc, 130000004, 1);
            }
            if (pc.AInt["中秋月饼_个人收集"] >= 500 && SInt["中秋月饼_全服收集"] >= 100000 && pc.AInt["中秋月饼桂花称号"] != 1)
            {
                pc.AInt["中秋月饼桂花称号"] = 1;
                GiveItem(pc, 130000005, 1);
            }
            if (pc.AInt["中秋月饼_个人收集"] >= 8000 && SInt["中秋月饼_全服收集"] >= 200000 && pc.AInt["中秋月饼月神称号"] != 1)
            {
                pc.AInt["中秋月饼月神称号"] = 1;
                GiveItem(pc, 130000006, 1);
            }
        }
    }
}

