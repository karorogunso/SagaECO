
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.FF
{
    public class S80000013 : Event
    {
        public S80000013()
        {
            this.EventID = 80000013;
        }

        class IrisTake
        {
            public uint score;
        }
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "您好，我是书页管理员·伊利斯。$R您可以将手上多余的书页换成积分，$R使用积分兑换其他书页哦。", "伊利斯");
            
            byte type = 1;
            switch(Select(pc,"选择书页种类","","伊利斯Another书页","阿鲁卡多Another书页", "芭丝特Another书页", "离开"))
            {
                case 1:
                    switch(Select(pc,"请选择【当前书页积分："+ pc.AInt["书页积分"].ToString() + "】","","换成积分","使用积分兑换", "离开"))
                    {
                        case 1:
                            换成积分(pc);
                            break;
                        case 2:
                            使用积分兑换(pc,1);
                            break;
                    }
                    break;
                case 2:
                    type = 2;
                    break;
                case 3:
                    type = 3;
                    break;
            }
            if (type != 1)
            {
                switch (Select(pc, "请选择【当前书页积分：" + pc.AInt["书页积分"].ToString() + "】", "", "使用积分兑换", "离开"))
                {
                    case 1:
                        使用积分兑换(pc, type);
                        break;
                }
            }
        }
        void 使用积分兑换(ActorPC pc,byte type)
        {
            Dictionary<uint, IrisTake> IrisTakeParas = new Dictionary<uint, IrisTake>();
            if (type == 1)
                IrisTakeParas = GetGiveIrisParas();
            else if(type == 2)
                IrisTakeParas = GetGive阿鲁卡多Paras();
            else if (type == 3)
                IrisTakeParas = GetGive芭丝特Paras();
            List<string> paras = new List<string>();
            List<uint> ids = new List<uint>();
            foreach (var item in IrisTakeParas)
            {
                SagaDB.Item.Item i = ItemFactory.Instance.GetItem(item.Key);
                paras.Add(i.BaseData.name + "【需要：" + item.Value.score.ToString() + "点】");
                ids.Add(item.Key);
            }
            paras.Add("离开");
            int set = Select(pc, "请选择兑换道具", "", paras.ToArray());
            if (set == paras.Count) return;
            uint itemid = ids[set - 1];
            uint score = IrisTakeParas[itemid].score;
            if(pc.AInt["书页积分"] < score)
            {
                Say(pc, 131, "积分不够哦");
                return;
            }
            else
            {
                GiveItem(pc, itemid, 1);
                pc.AInt["书页积分"] -= (int)score;
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("使用了 " + score.ToString() + " 点书页积分，当前总共拥有：" + pc.AInt["书页积分"].ToString());

            }
        }
        void 换成积分(ActorPC pc)
        {
            Dictionary<uint, IrisTake> IrisTakeParas = new Dictionary<uint, IrisTake>();
            IrisTakeParas = GetTakeIrisParas();
            List<string> paras = new List<string>();
            List<uint> ids = new List<uint>();
            foreach (var item in IrisTakeParas)
            {
                SagaDB.Item.Item i = ItemFactory.Instance.GetItem(item.Key);
                paras.Add(i.BaseData.name + "【" + item.Value.score.ToString() + "点】");
                ids.Add(item.Key);
            }
            paras.Add("离开");
            int set = Select(pc, "请选择要兑换成积分的道具", "", paras.ToArray());
            if (set == paras.Count) return;
            ushort count = ushort.Parse(InputBox(pc, "请输入要兑换个数", InputType.ItemCode));
            uint itemid = ids[set - 1];
            uint score = IrisTakeParas[itemid].score;
            if (CountItem(pc, itemid) < count)
            {
                Say(pc, 131, "道具不够哦");
                return;
            }
            else
            {
                TakeItem(pc, itemid, count);
                int s = (int)(score * count);
                pc.AInt["书页积分"] += s;
                SagaMap.Network.Client.MapClient.FromActorPC(pc).SendSystemMessage("得到了 " + s.ToString() + " 点书页积分，当前总共拥有：" + pc.AInt["书页积分"].ToString());
            }
        }

        Dictionary<uint, IrisTake> GetTakeIrisParas()
        {
            Dictionary<uint, IrisTake> items = new Dictionary<uint, IrisTake>();
            IrisTake iris = new IrisTake();
            iris.score = 1;
            items.Add(10162400, iris);
            iris = new IrisTake();
            iris.score = 2;
            items.Add(10162401, iris);
            iris = new IrisTake();
            iris.score = 7;
            items.Add(10162402, iris);
            iris = new IrisTake();
            iris.score = 3;
            items.Add(10162403, iris);
            items.Add(10162404, iris);
            items.Add(10162405, iris);
            items.Add(10162407, iris);
            iris = new IrisTake();
            iris.score = 4;
            items.Add(10162406, iris);
            iris = new IrisTake(); 
            iris.score = 25;
            items.Add(10163800, iris);
            return items;
        }

        Dictionary<uint, IrisTake> GetGiveIrisParas()
        {
            Dictionary<uint, IrisTake> items = new Dictionary<uint, IrisTake>();
            IrisTake iris = new IrisTake();
            iris.score = 2;
            items.Add(10162400, iris);
            iris = new IrisTake();
            iris.score = 3;
            items.Add(10162401, iris);
            iris = new IrisTake();
            iris.score = 12;
            items.Add(10162402, iris);
            iris = new IrisTake();
            iris.score = 5;
            items.Add(10162403, iris);
            items.Add(10162407, iris);
            iris = new IrisTake();
            iris.score = 6;
            items.Add(10162404, iris);
            items.Add(10162405, iris);
            iris = new IrisTake();
            iris.score = 7;
            items.Add(10162406, iris);
            iris = new IrisTake();
            iris.score = 25;
            items.Add(10163800, iris);
            iris = new IrisTake();
            iris.score = 25;
            items.Add(10163801, iris);
            iris = new IrisTake();
            iris.score = 25;
            items.Add(10163802, iris);
            iris = new IrisTake();
            iris.score = 25;
            items.Add(10163803, iris);
            return items;
        }
        Dictionary<uint, IrisTake> GetGive阿鲁卡多Paras()
        {
            Dictionary<uint, IrisTake> items = new Dictionary<uint, IrisTake>();
            IrisTake iris = new IrisTake();
            iris.score = 5;
            items.Add(10161200, iris);
            iris = new IrisTake();
            iris.score = 7;
            items.Add(10161201, iris);
            iris = new IrisTake();
            iris.score = 18;
            items.Add(10161202, iris);
            iris = new IrisTake();
            iris.score = 10;
            items.Add(10161203, iris);
            items.Add(10161207, iris);
            iris = new IrisTake();
            iris.score = 11;
            items.Add(10161204, iris);
            items.Add(10161205, iris);
            iris = new IrisTake();
            iris.score = 15;
            items.Add(10161206, iris);
            iris = new IrisTake();
            iris.score = 50;
            items.Add(10162600, iris);
            iris = new IrisTake();
            iris.score = 120;
            items.Add(10162601, iris);
            iris = new IrisTake();
            iris.score = 240;
            items.Add(10162602, iris);
            iris = new IrisTake();
            iris.score = 430;
            items.Add(10162603, iris);
            return items;
        }
        Dictionary<uint, IrisTake> GetGive芭丝特Paras()
        {
            Dictionary<uint, IrisTake> items = new Dictionary<uint, IrisTake>();
            IrisTake iris = new IrisTake();
            iris.score = 5;
            items.Add(10161500, iris);
            iris = new IrisTake();
            iris.score = 7;
            items.Add(10161501, iris);
            iris = new IrisTake();
            iris.score = 18;
            items.Add(10161502, iris);
            iris = new IrisTake();
            iris.score = 10;
            items.Add(10161503, iris);
            items.Add(10161507, iris);
            iris = new IrisTake();
            iris.score = 11;
            items.Add(10161504, iris);
            items.Add(10161505, iris);
            iris = new IrisTake();
            iris.score = 15;
            items.Add(10161506, iris);
            iris = new IrisTake();
            iris.score = 50;
            items.Add(10162900, iris);
            iris = new IrisTake();
            iris.score = 120;
            items.Add(10162901, iris);
            iris = new IrisTake();
            iris.score = 240;
            items.Add(10162902, iris);
            iris = new IrisTake();
            iris.score = 430;
            items.Add(10162903, iris);
            return items;
        }
    }
}