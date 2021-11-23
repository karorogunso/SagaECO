using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30013002
{
    public class S11000687 : Event
    {
        public S11000687()
        {
            this.EventID = 11000687;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<MZTYSFlags> mask = pc.CMask["MZTYS"];
            if (mask.Test(MZTYSFlags.武器製作所店員第一次對話))//_3A13)
            {
                mask.SetValue(MZTYSFlags.武器製作所店員第一次對話, true);
                //_3A13 = true;
                Say(pc, 131, "喲!$R;" +
                    "快來$R;" +
                    "$R這裡是帕斯特$R唯一的武器製造所$R;" +
                    "是用您們收集的材料$R;" +
                    "煉鐵和製作武器的$R;" +
                    "$P如果有想做的東西，就快點說吧$R;" +
                    "現在告訴我的話，可以免費給您做的$R;");
            }
            switch (Select(pc, "要做什麼呢？", "", "製造武器", "製造防具", "煉製金屬", "製造箭", "什麼也不做"))
            {
                case 1:
                    switch (Select(pc, "想製作什麼?", "", "製作武器", "製作魔杖", "製作弓", "放棄"))
                    {
                        case 1:
                            Synthese(pc, 2010, 10);
                            break;
                        case 2:
                            Synthese(pc, 2021, 5);
                            break;
                        case 3:
                            Synthese(pc, 2034, 5);
                            break;
                    }
                    break;
                case 2:
                    Synthese(pc, 2017, 5);
                    break;
                case 3:
                    Synthese(pc, 2051, 3);
                    break;
                case 4:
                    Synthese(pc, 2035, 5);
                    break;
            }
        }
    }
}