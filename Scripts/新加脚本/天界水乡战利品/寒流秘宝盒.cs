
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S953000021 : Event
    {
        public S953000021()
        {
            this.EventID = 953000021;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 953000021) >= 1)
            {
                TakeItem(pc, 953000021, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {

            SagaDB.Item.Item item = ItemFactory.Instance.GetItem(953100000);
            switch (Global.Random.Next(0, 3))
            {
                case 0://海洋之结晶
                    item = ItemFactory.Instance.GetItem(953100000);
                    item.Refine = 1;
                    item.HP = (short)Global.Random.Next(0, 320);//HP随机(0 ~ 500)
                    if (SagaLib.Global.Random.Next(0, 100) <= 20)
					{
						item.HP = (short)Global.Random.Next(0, 500);//HP随机(0 ~ 500)
						item.Name = "丰饶的海洋之结晶";
					}
                    item.Def = (short)Global.Random.Next(0, 1);
                    item.MDef = (short)Global.Random.Next(0, 1);
                    if (SagaLib.Global.Random.Next(0, 100) <= 50)
                    {
						item.Name = "坚固的海洋之结晶";
                        item.Def = (short)Global.Random.Next(0, 2);
                        item.MDef = (short)Global.Random.Next(0, 2);
                    }
                    GiveItem(pc, item);
                    break;
                case 1:
                    item = ItemFactory.Instance.GetItem(953100001);
                    item.Refine = 1;
                    item.WeightUp = (short)Global.Random.Next(0, 150);//加算物理攻击随机(0 ~ 20)
                    item.VolumeUp = (short)Global.Random.Next(0, 150);//加算魔法攻击随机(0 ~ 20)
                    if(SagaLib.Global.Random.Next(0,100) <= 20)
                    {
						item.Name = "大海贼的剑";
                        item.WeightUp = (short)Global.Random.Next(0, 200);//加算物理攻击随机(0 ~ 20)
                        item.VolumeUp = (short)Global.Random.Next(0, 200);//加算魔法攻击随机(0 ~ 20)
                    }
                    short atk = (short)Global.Random.Next(0, 1);
                    item.Atk1 = atk;//乘算物理攻击力固定提升：2
                    item.Atk2 = atk;//乘算物理攻击力固定提升：2
                    item.Atk3 = atk;//乘算物理攻击力固定提升：2
                    item.MAtk = (short)Global.Random.Next(0, 1);//乘算魔法攻击力固定提升：2
                    if (SagaLib.Global.Random.Next(0, 100) <= 50)
                    {
						item.Name = "伟大海贼的剑";
                        atk = (short)Global.Random.Next(0, 2);
                        item.Atk1 = atk;//乘算物理攻击力固定提升：2
                        item.Atk2 = atk;//乘算物理攻击力固定提升：2
                        item.Atk3 = atk;//乘算物理攻击力固定提升：2
                        item.MAtk = (short)Global.Random.Next(0, 2);//乘算魔法攻击力固定提升：2
                    }
                    GiveItem(pc, item);
                    break;
                case 2://海洋之心
                    item = ItemFactory.Instance.GetItem(953100002);
                    item.Refine = 1;
                    item.Str = (short)Global.Random.Next(0, 2);//STR随机(0 ~ 3)
                    item.Int = (short)Global.Random.Next(0, 2);//INT随机(0 ~ 3)
                    item.Dex = (short)Global.Random.Next(0, 2);//DEX随机(0 ~ 3)
                    item.Mag = (short)Global.Random.Next(0, 2);//MAG随机(0 ~ 3)
                    item.Vit = (short)Global.Random.Next(0, 2);//VIT随机(0 ~ 3)
                    item.Agi = (short)Global.Random.Next(0, 2);//AGI随机(0 ~ 3)
                    if (SagaLib.Global.Random.Next(0, 100) <= 30)
                    {
						item.Name = "怒卷的海洋之心";
                        item.Str = (short)Global.Random.Next(0, 3);//STR随机(0 ~ 3)
                        item.Int = (short)Global.Random.Next(0, 3);//INT随机(0 ~ 3)
                        item.Dex = (short)Global.Random.Next(0, 3);//DEX随机(0 ~ 3)
                        item.Mag = (short)Global.Random.Next(0, 3);//MAG随机(0 ~ 3)
                        item.Vit = (short)Global.Random.Next(0, 3);//VIT随机(0 ~ 3)
                        item.Agi = (short)Global.Random.Next(0, 3);//AGI随机(0 ~ 3)
                    }
                    GiveItem(pc, item);
                    break;
                case 3://寒流的月
                    item = ItemFactory.Instance.GetItem(953100003);
                    item.Refine = 1;
                    item.ASPD = (short)Global.Random.Next(0, 20);//攻击速度随机(0 ~ 20)
                    item.CSPD = (short)Global.Random.Next(0, 20);//吟唱速度随机(0 ~ 20)
                    item.Def = (short)Global.Random.Next(0, 2);
                    item.MDef = (short)Global.Random.Next(0, 2);
                    GiveItem(pc, item);
                    break;
            }
            SInt[pc.Name + "材料箱次数"]++;
        }
    }
}

