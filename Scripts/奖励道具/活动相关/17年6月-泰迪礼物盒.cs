
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap.Skill;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S952000003 : Event
    {
        public S952000003()
        {
            this.EventID = 952000003;
        }
        class Spring
        {
            public uint itemID;
            public uint rate;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 952000003) > 0)
            {
                uint itemid = GetBoundId();
                if (itemid == 0) return;
                TakeItem(pc, 952000003, 1);
                GiveItem(pc, itemid, 1);
                SkillHandler.Instance.ShowEffectOnActor(pc, 8056);
                List<uint> ann = new List<uint>() { 110137600, 910000104, 960000012, 950000028, 950000027, 950000031, 950000032 };
                if (ann.Contains(itemid) && pc.Account.GMLevel < 100)
                {
                    SagaDB.Item.Item i = ItemFactory.Instance.GetItem(itemid);
                    Announce(pc.Name + " 从 泰迪礼物盒里 获得了 " + i.BaseData.name + " ！");
                }
            }
        }
        uint GetBoundId()
        {
            uint id = 10000000;
            List<Spring> list = GetList();
            int totalrate = GetTotalRate(list);
            int ran = Global.Random.Next(0, totalrate);
            int rate = 0;
            foreach (var item in list)
            {
                rate += (int)item.rate;
                if (ran <= rate)
                {
                    id = item.itemID;
                    break;
                }
            }
            return id;
        }
        Spring New(uint id, uint rate)
        {
            Spring s = new Spring();
            s.itemID = id;
            s.rate = rate;
            return s;
        }
        int GetTotalRate(List<Spring> list)
        {
            int t = 0;
            foreach (var item in list)
            {
                t += (int)item.rate;
            }
            return t;
        }
        List<Spring> GetList()
        {
            List<Spring> items = new List<Spring>();
            items.Add(New(110137600, 5));//泰迪·阿鲁玛
            items.Add(New(953300000, 10));//儿童节发型介绍信
            items.Add(New(60151807, 20));//贺喜连衣裙
            items.Add(New(50011640, 25));//庆祝的长筒袜
            items.Add(New(50219690, 25));//贺年凉鞋
            items.Add(New(61070100, 20));//新年祝贺铃铛
            items.Add(New(60087400, 15));//雪糕
            items.Add(New(60087401, 15));//雪糕（抹茶）
            items.Add(New(60087402, 15));//雪糕（巧克力）
            items.Add(New(60087403, 15));//雪糕（草莓）
            items.Add(New(61070700, 15));//多彩气球（红）
            items.Add(New(61071600, 10));//水果牛奶
            items.Add(New(61071601, 10));//咖啡牛奶
            items.Add(New(61010900, 15));//投掷派
            items.Add(New(61011501, 15));//烤好的披萨
            items.Add(New(61071200, 15));//缘日水气球（紫）
            items.Add(New(61071201, 15));//缘日水气球（红）
            items.Add(New(60089700, 10));//一袋鲷鱼烧
            items.Add(New(60087200, 10));//棒棒糖
            items.Add(New(60087201, 10));//棒棒糖（露莉耶）
            items.Add(New(910000101, 50));//任务点增加勋章[5点]
            items.Add(New(910000102, 10));//任务点增加勋章[10点]
            items.Add(New(910000103, 10));//任务点增加勋章[50点]
            items.Add(New(910000104, 5));//任务点增加勋章[300点]
            /*items.Add(New(910000075, 10));//经验值增加50%
            items.Add(New(910000076, 10));//经验值增加100%
            items.Add(New(910000077, 5));//经验值增加150%
            items.Add(New(910000078, 3));//经验值增加200%
            items.Add(New(910000079, 2));//经验值增加250%
            items.Add(New(910000080, 1));//经验值增加300%
            items.Add(New(910000086, 10));//搭档经验值增加50%
            items.Add(New(910000087, 10));//搭档经验值增加100%
            items.Add(New(910000088, 5));//搭档经验值增加150%
            items.Add(New(910000089, 3));//搭档经验值增加200%
            items.Add(New(910000090, 2));//搭档经验值增加250%
            items.Add(New(910000091, 1));//搭档经验值增加300%*/
            items.Add(New(910000092, 50));//搭档经验礼盒（2000）
            items.Add(New(910000093, 20));//搭档经验礼盒（4000）
            items.Add(New(910000094, 20));//搭档经验礼盒（6000）
            items.Add(New(910000095, 15));//搭档经验礼盒（8000）
            items.Add(New(910000096, 10));//搭档经验礼盒（10000）
            items.Add(New(910000097, 10));//搭档盒子
            //items.Add(New(910000106, 10));//搭档变身卡
            items.Add(New(950000027, 5));//S级搭档突破石
            items.Add(New(950000028, 1));//SS级搭档突破石
            items.Add(New(950000000, 5));//发型代币
            items.Add(New(950000001, 5));//抽脸币
            items.Add(New(951000000, 50));//KUJI扭蛋机代币
            items.Add(New(950000025, 15));//限定KUJI代币
            items.Add(New(960000000, 10));//项链强化石
            items.Add(New(960000001, 10));//武器强化石
            items.Add(New(960000002, 10));//衣服强化石
            items.Add(New(960000010, 10));//强化11-20祝福水
            items.Add(New(960000011, 5));//强化21-30祝福水
            items.Add(New(960000012, 1));//强化31-40祝福水
            //items.Add(New(950000031, 5));//新增一个打开限定kuji盒子的钥匙(rank0-3等奖品机率x10)
            //items.Add(New(950000032, 5));//新增一个打开限定kuji盒子的钥匙(必开出盒内rank5以上的物品)
            return items;
        }
    }
}

