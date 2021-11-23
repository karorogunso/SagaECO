
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
    public class S99660000 : Event
    {
        public S99660000()
        {
            this.EventID = 99660000;
        }

        public override void OnEvent(ActorPC pc)
        {
            uint SlotID = 0;
            if (pc.TInt["套装Change的ID"] != 0)
                SlotID = (uint)pc.TInt["套装Change的ID"];
            pc.TInt["套装Change的ID"] = 0;

            SagaDB.Item.Item item = pc.Inventory.GetItem(SlotID);
            string text = "『" + item.BaseData.name + "』融合信息：$R$R";

            List<string> EnEName = new List<string>();
            foreach (var it in item.EnChangeList)
            {
                SagaDB.Item.Item equip = ItemFactory.Instance.GetItem(it.Key);
                EnEName.Add(equip.BaseData.name);
            }
            for (int i = 0; i < 5; i++)
            {
                if (EnEName.Count > i)
                    text += "装备" + (i + 1) + "：" + EnEName[i] + "$R";
                else
                    text += "装备" + (i + 1) + "：无$R";
            }
            if (EnEName.Count > 0)
                text += "$R现在可以切换装备形态哦";
            else
                text += "$R目前无法切换装备形态";
            Say(pc, 0, text, "套装装备信息");

            switch(Select(pc,"要怎么办呢？","","查看融合装备信息","切换装备形态","融合其他套装装备","消散已融合的装备","什么也不做"))
            {

            }
        }
    }
}

