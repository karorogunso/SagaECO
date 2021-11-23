
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
    public class S910000122 : Event
    {
        public S910000122()
        {
            this.EventID = 910000122;
        }
        class Valentine
        {
            public uint itemID;
            public uint rate;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000122) > 0)
            {
                uint itemid = GetBoundId();
                if (itemid == 0) return;
                TakeItem(pc, 910000122, 1);
                GiveItem(pc, itemid, 1);
                SkillHandler.Instance.ShowEffectOnActor(pc, 8056);
                List<uint> ann = new List<uint>() { 110137500, 910000104, 960000012, 950000028, 950000027, 950000031, 950000032 };
                if (ann.Contains(itemid) && pc.Account.GMLevel < 100)
                {
                    SagaDB.Item.Item i = ItemFactory.Instance.GetItem(itemid);
                    Announce(pc.Name + " 从 白色情人节甜蜜盒子里 获得了 " + i.BaseData.name + " ！");
                }
            }
        }
        uint GetBoundId()
        {
            uint id = 10000000;
            List<Valentine> list = GetList();
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
        Valentine New(uint id, uint rate)
        {
            Valentine s = new Valentine();
            s.itemID = id;
            s.rate = rate;
            return s;
        }
        int GetTotalRate(List<Valentine> list)
        {
            int t = 0;
            foreach (var item in list)
            {
                t += (int)item.rate;
            }
            return t;
        }
        List<Valentine> GetList()
        {
            List<Valentine> items = new List<Valentine>();
            items.Add(New(110137500, 5));//天使之羽·阿鲁玛
            items.Add(New(10075293, 10));//天使之羽发型介绍信
            items.Add(New(50082200, 10));//天使之翼（白色·有缎带）
            items.Add(New(60149100, 20));//守护天使的连衣裙（白）
            items.Add(New(50011570, 25));//守护天使的短袜（白）
            items.Add(New(50233000, 25));//守护天使的鞋子（黑色）
            items.Add(New(31168200, 20));//天使蛋（白）
            items.Add(New(60149200, 20));//大天使的正装（白色·上衣）
            items.Add(New(50232900, 20));//大天使的正装（白色·裤子）
            items.Add(New(31168100, 15));//天使的秋千
            items.Add(New(61070000, 15));//神圣铃铛（金色）
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

