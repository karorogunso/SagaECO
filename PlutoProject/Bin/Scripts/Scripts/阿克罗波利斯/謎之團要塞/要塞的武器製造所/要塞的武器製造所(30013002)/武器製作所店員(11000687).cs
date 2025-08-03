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
                Say(pc, 131, "哟!$R;" +
                    "快来$R;" +
                    "$R这力是法伊斯特$R唯一的铁匠铺$R;" +
                    "是用您们收集的材料$R;" +
                    "炼铁和制作武器的$R;" +
                    "$P如果有想做的东西，就快点说吧$R;" +
                    "现在告诉我的话，可以免费给您做的$R;");
            }
            switch (Select(pc, "要做什么呢？", "", "制造武器", "制造防具", "炼制金属", "制造箭", "什么也不做"))
            {
                case 1:
                    switch (Select(pc, "想制作什么?", "", "制作武器", "制作魔杖", "制作弓", "放弃"))
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