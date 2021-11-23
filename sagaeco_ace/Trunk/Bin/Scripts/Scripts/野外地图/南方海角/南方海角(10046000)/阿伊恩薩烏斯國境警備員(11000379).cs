using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10046000
{
    public class S11000379 : Event
    {
        public S11000379()
        {
            this.EventID = 11000379;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Knights> Knights_mask = pc.CMask["Knights"];

            if (pc.Skills2.ContainsKey(706) || pc.SkillsReserve.ContainsKey(706))
            {
                Say(pc, 131, "哦哦!$R;" +
                    pc.Name + "不是吗!$R;" +
                    "$R快请进吧$R;");
                return;
            }
            if (Knights_mask.Test(Knights.南國過境檢查完成))
            {
                Say(pc, 131, "辛苦了!$R;" +
                    "可以通过了$R;");
                return;
            }
            if (Knights_mask.Test(Knights.加入南軍騎士團))
            {
                
                Say(pc, 131, "隶属于阿克罗尼亚混成骑士团$R;" +
                    "『南军』的人?$R;" +
                    "$R请出示『艾恩萨乌斯骑士团证』$R;");
                switch (Select(pc, "出示骑士团证吗?", "", "出示", "不出示"))
                {
                    case 1:
                        if (CountItem(pc, 10041500) >= 1)
                        {
                            Knights_mask.SetValue(Knights.南國過境檢查完成, true);
                            PlaySound(pc, 2040, false, 100, 50);
                            Say(pc, 131, "好的，没问题$R;" +
                                "$R以后不出示也可以通过了$R;");
                            //Warp(pc, 10061000, 145, 3);
                            return;
                        }
                        Say(pc, 131, "好像没有骑士团证阿$R;");
                        break;
                    case 2:
                        break;
                }
            }
            //EVT1100037903
            if (CountItem(pc, 10042000) >= 1)
            {
                Say(pc, 131, "请把『艾恩萨乌斯入国许可证』$R;" +
                    "$R出示一下吧$R;");
                switch (Select(pc, "出示入国许可证吗?", "", "出示", "不出示"))
                {
                    case 1:
                        PlaySound(pc, 2040, false, 100, 50);
                        TakeItem(pc, 10042000, 1);
                        Knights_mask.SetValue(Knights.南國過境檢查完成, true);
                        Say(pc, 131, "好的，没问题$R;" +
                            "$R那么许可证就在这里回收了$R;" +
                            "$R以后不出示也可以通过了$R;");
                        //Warp(pc, 10061000, 145, 3);
                        break;
                    case 2:
                        PlaySound(pc, 2041, false, 100, 50);
                        Say(pc, 131, "如果不出示入国许可证的话$R;" +
                            "就无法通过此地了$R;" +
                            "$R请出示入国许可证$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "欢迎来到艾恩萨乌斯联邦国!$R;" +
                "$R艾恩萨乌斯联邦国是$R;" +
                "只有属于阿克罗尼亚混成骑士团南军$R;" +
                "或持『艾恩萨乌斯入国许可证』的人$R;" +
                "$R才可以入境$R;");
        }
    }
}
