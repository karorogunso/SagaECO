using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10001000
{
    public class S11000057 : Event
    {
        public S11000057()
        {
            this.EventID = 11000057;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "哦哦!$R;" +
                    "这不是" + pc.Name + "吗!$R;" +
                    "$R快请进$R;");
                return;
            }
            if (Knights_mask.Test(Knights.北國過境檢查完成))
            {
                Say(pc, 131, "辛苦了!您可以通过了$R;");
                return;
            }
            if (Knights_mask.Test(Knights.加入北軍騎士團))
            {
                
                Say(pc, 131, "您是隶属阿克罗尼亚混成骑士团$R;" +
                    "的『北军』吧？$R;" +
                    "$R请出示您的『诺森骑士团证』$R;");
                switch (Select(pc, "是否出示骑士团证?", "", "出示", "不出示"))
                {
                    case 1:
                        if (CountItem(pc, 10041600) >= 1)
                        {
                            Knights_mask.SetValue(Knights.北國過境檢查完成, true);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "好的，没问题了！！$R;" +
                                "$R以后不需要出示证件就可以通过了$R;");
                            //Warp(pc, 10049000, 147, 252);
                            return;
                        }
                        Say(pc, 131, "您好像没有骑士团证啊$R;");
                        break;
                    case 2:
                        break;
                }
            }
            if (CountItem(pc, 10042100) >= 1)
            {
                Say(pc, 131, "请出示你的『诺森入国许可证』$R;");
                switch (Select(pc, "出示入国许可证?", "", "出示", "不出示"))
                {
                    case 1:
                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10042100, 1);
                        Knights_mask.SetValue(Knights.北國過境檢查完成, true);
                        Say(pc, 131, "好的，没问题了！！$R;" +
                            "$R我们将回收你的入国许可证$R;" +
                            "以后不需要出示证件就可以通过了$R;");
                        //Warp(pc, 10049000, 147, 252);
                        break;
                    case 2:
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "不出示的话无法让你通过$R;" +
                            "$R所以请出示一下许可证吧$R;");
                        break;
                }
                return;
            }
            //if (CountItem(pc, 10042101) >= 1)
            //{
            //    Say(pc, 131, "请出示你的『诺森入国许可证』$R;");
            //    switch (Select(pc, "出示伪造入国许可证?", "", "出示", "不出示"))
            //    {
            //        case 1:
            //            Say(pc, 131, "这个证件是假的??$R;" +
            //                "颜色好像比较深！$R;" +
            //                "$R该不会是伪造的吧?$R;");
            //            switch (Select(pc, "是真的嗎?", "", "不是", "是的"))
            //            {
            //                case 1:
            //                    TakeItem(pc, 10042101, 1);
            //                    Say(pc, 131, "什么！？证件是假的！！$R;" +
            //                        "你想做非法的事情嗎！？$R;" +
            //                        "$P哼！这次我就放你一马！！$R;" +
            //                        "如果再有下次，我绝对不会放过你！$R;" +
            //                        "$R这张假的许可证我就没收了$R;" +
            //                        "有什么不满吗？去投诉我吧！$R;");
            //                    PlaySound(pc, 2041, false, 100, 50);
            //                    Say(pc, 0, 131, "失去了僞造諾頓入國許可證$R;");
            //                    break;
            //                case 2:
            //                    TakeItem(pc, 10042101, 1);
            //                    PlaySound(pc, 2040, false, 100, 50);
            //                    Say(pc, 131, "啊…我好像失礼了$R;" +
            //                        "我太过敏感了，真是抱歉啊$R;" +
            //                        "$R我们将回收你的入国许可证$R;" +
            //                        "请通过吧$R;");
            //                    Warp(pc, 10049000, 147, 252);
            //                    break;
            //            }
            //            break;
            //        case 2:
            //            PlaySound(pc, 2041, false, 100, 50);
            //            Say(pc, 131, "不出示许可证就无法入境$R;" +
            //                "$R所以请你出示一下许可证$R;");
            //            break;
            //    }
            //    return;
            //}
            Say(pc, 131, "如果不是阿克罗尼亚$R;" +
                "混成骑士团北军的成员$R;" +
                "或持有『诺森入国许可证』的冒险者$R;" +
                "是不能进入诺森王国的$R;" +
                "$P请回吧$R;");
        }
    }
}
