using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
using System.Linq;
using System.Collections;
//所在地圖:下城(10024000) NPC基本信息:銀(11001172) X:132 Y:97
namespace SagaScript.M80060050
{
    public class S20010020 : Event
    {
        public S20010020()
        {
            this.EventID = 20010020;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2030, false, 100, 50);
            Say(pc, 20010020, 131, "你有ECO之心吗？" +
                                    "$R收集了足够的这种东西我就可以买买买了！$R;", "遥远的绿巨人");
            Say(pc, 20010020, 111, "虽然…好像…" +
                                    "$R这个世界好像不太一样？…算了$R;", "遥远的绿巨人");
            if (CountItem(pc, 22000103) >= 5)
            {
                Say(pc, 20010020, 111, "你好像有兴趣跟我交易？" +
                                    "$R太好了！我终于不用去蹲大陆洞了！" +
                                     "这些是我的存货！$R;" +
                                     "不过有些东西暂时用不了,可别说我坑你哦~$R;", "？？？");

                KujiTrade trade = new KujiTrade();
                List<string> arrname = trade.tradelist.Keys.ToList();
                arrname.Add("没兴趣");
                int lastoption = arrname.Count;
                int option = Select(pc, "想要什么", "", arrname.ToArray());
                if (option == arrname.Count)
                    return;
                string changenum = InputBox(pc, "要换几个?", InputType.Bank);
                int num = 0;
                if (!int.TryParse(changenum, out num))
                {
                    Say(pc, 20010020, 111, "魂淡!我这是问你要换几个!;", "遥远的绿巨人");
                    return;
                }
                KujiTradeInfo info = trade.tradelist[arrname[option]];
                if (CountItem(pc, info.HeartID) < info.HeartNum * num)
                {
                    Say(pc, 20010020, 111, "魂淡!你丫的哪来那么多心!?!;", "遥远的绿巨人");
                    return;
                }
                PlaySound(pc, 2040, false, 100, 50);
                TakeItem(pc,info.HeartID,info.HeartNum);
                GiveItem(pc, info.KujiBoxID, info.KujiBoxNum);
                Say(pc, 20010020, 111, "$R拜拜~~~~~~~~~$R;", "遥远的绿巨人");
            }
            else
            {
                Say(pc, 20010020, 111, "$R如果有兴趣就来找我吧。$R;", "遥远的绿巨人");
            }
        }
    }
    public class KujiTradeInfo
    {
        private uint heartid;
        /// <summary>
        /// 心的ID
        /// </summary>
        public uint HeartID
        {
            get { return heartid; }
            set { heartid = value; }
        }
        private byte heartnum;

        /// <summary>
        /// 心的数量
        /// </summary>
        public byte HeartNum
        {
            get { return heartnum; }
            set { heartnum = value; }
        }

        private uint kujiboxid;
        /// <summary>
        /// kuji盒子的ID
        /// </summary>
        public uint KujiBoxID
        {
            get { return kujiboxid; }
            set { kujiboxid = value; }
        }

        private byte kuijiboxnum;
        /// <summary>
        /// kuji盒子的数量
        /// </summary>
        public byte KujiBoxNum
        {
            get { return kuijiboxnum; }
            set { kuijiboxnum = value; }
        }

        /// <summary>
        /// 初始化兑换对象
        /// </summary>
        /// <param name="heartid">兑换物的ID</param>
        /// <param name="heartnum">所需兑换物数量</param>
        /// <param name="kujiboxid">兑换kuji盒子的ID</param>
        /// <param name="kujiboxnum">兑换kuji盒子的数量</param>
        public KujiTradeInfo(uint heartid, byte heartnum, uint kujiboxid, byte kujiboxnum)
        {
            this.heartid = heartid;
            this.heartnum = heartnum;
            this.kujiboxid = kujiboxid;
            this.kuijiboxnum = kujiboxnum;
        }
    }

    public class KujiTrade
    {

        public Dictionary<string, KujiTradeInfo> tradelist = new Dictionary<string, KujiTradeInfo>();
        public KujiTrade()
        {
            tradelist.Add("草?", new KujiTradeInfo(123, 1, 321, 1));
            tradelist.Add("123", new KujiTradeInfo(100, 1, 1005, 1));
            tradelist.Add("234", new KujiTradeInfo(100, 1, 1005, 1));
            tradelist.Add("345", new KujiTradeInfo(100, 1, 1005, 1));
        }

    }
}
