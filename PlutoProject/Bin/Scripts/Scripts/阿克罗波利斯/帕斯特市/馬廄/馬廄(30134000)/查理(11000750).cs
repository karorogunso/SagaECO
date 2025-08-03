using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30134000
{
    public class S11000750 : Event
    {
        public S11000750()
        {
            this.EventID = 11000750;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (mask.Test(PSTFlags.尋找小雞結束))//_5a16)
            {
                Say(pc, 131, "那么开始清洁…$R;");
                return;
            }
            if (mask.Test(PSTFlags.開始尋找小雞) &&
                mask.Test(PSTFlags.找到1號小雞) &&
                mask.Test(PSTFlags.找到2號小雞) &&
                mask.Test(PSTFlags.找到3號小雞) &&
                mask.Test(PSTFlags.找到4號小雞) &&
                mask.Test(PSTFlags.找到5號小雞) &&
                mask.Test(PSTFlags.找到6號小雞) &&
                mask.Test(PSTFlags.找到7號小雞))//_5a08 && _5a09 && _5a10 && _5a11 && _5a12 && _5a13 && _5a14 && _5a15)
            {
                mask.SetValue(PSTFlags.尋找小雞結束, true);
                //_5a16 = true;
                Say(pc, 131, "找到小鸡了吗？$R;" +
                    "$P嗯?它们不愿回来？$R;" +
                    "$R那没办法啊!$R;" +
                    "只能等到它们都睡着了$R;" +
                    "$P这是我的小小意思$R;" +
                    "能收下吗？$R;");
                Wait(pc, 2000);
                PlaySound(pc, 3087, false, 100, 50);
                ShowEffect(pc, 4131);
                Wait(pc, 2000);
                SkillPointBonus(pc, 1);
                Say(pc, 131, "技能点数增加了1$R;");
                return;
            }
            mask.SetValue(PSTFlags.開始尋找小雞, true);
            //_5a08 = true;
            Say(pc, 131, "啊！小鸡？$R;" +
                "小鸡在哪里？$R;" +
                "$R喂!知不知道小鸡在哪里？$R;" +
                "是个刚出生的咕咕鸡！$R;" +
                "$P还很小的$R;" +
                "发现的话能不能带回来呢？$R;" +
                "$R不见的小鸡一共有7只!$R;");
        }
    }
}