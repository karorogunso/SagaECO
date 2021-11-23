using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaLib;
//using MySql.Data.MySqlClient;
using System.Data;
using SagaScript.Chinese;
//所在地圖:上城東邊吊橋(10023100) NPC基本信息:初心者嚮導(11000532) X:247 Y:124
namespace SagaScript.M10023100
{
    public class S11000532 : Event
    {
        public S11000532()
        {
            this.EventID = 11000532;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要拿取今天的每日獎勵嗎？", "", "要", "不要"))
            {
                case 1:
                    Verification(pc);
                    break;
                case 2:
                    return;
            }
            //20050073 温泉の素
            //20050056 フシギなマッチ
            //10022900 時空の鍵
            //ワープできる鍵E
            //タイニーの秘伝収納術
        }
        void Verification(ActorPC pc)
        {
            //檢查同一天重複領取
            if (pc.CInt["LastDailyItemCountYear"] == DateTime.Now.Year &&
                pc.CInt["LastDailyItemCountMonth"] == DateTime.Now.Month &&
                pc.CInt["LastDailyItemCountDay"] == DateTime.Now.Day)
            {
                Say(pc, 0, "今天你已經領取過了哦", "招待泰迪");
                return;
            }
            //更新領取日期
            {
                pc.CInt["LastDailyItemCountYear"] = DateTime.Now.Year;
                pc.CInt["LastDailyItemCountMonth"] = DateTime.Now.Month;
                pc.CInt["LastDailyItemCountDay"] = DateTime.Now.Day;
                //轉移到一下程序:發送物品
                DetermineItemToGive(pc);
            }
        }
        void DetermineItemToGive(ActorPC pc)
        {
            BitMask<DailyItem> DailyItem_mask = pc.CMask["DailyItem"];
            //第一天獎勵
            if (!(DailyItem_mask.Test(DailyItem.第一天)))
            {
                DailyItem_mask.SetValue(DailyItem.第一天, true);
                Say(pc, 0, "這是第一天的每日獎勵。$R今後每天也要來喔。", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY1-3");
                return;
            }
            //第二天獎勵
            if ((DailyItem_mask.Test(DailyItem.第一天)) && !(DailyItem_mask.Test(DailyItem.第二天)))
            {
                DailyItem_mask.SetValue(DailyItem.第二天, true);
                Say(pc, 0, "這是第二天的每日獎勵。$R朝著第十天加油吧", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY1-3");
                return;
            }
            //第三天獎勵
            if ((DailyItem_mask.Test(DailyItem.第二天)) && !(DailyItem_mask.Test(DailyItem.第三天)))
            {
                DailyItem_mask.SetValue(DailyItem.第三天, true);
                Say(pc, 0, "這是第三天的每日獎勵。$R每天都拿取一次是好習慣喔喔", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY1-3");
                return;
            }
            //第四天獎勵
            if ((DailyItem_mask.Test(DailyItem.第三天)) && !(DailyItem_mask.Test(DailyItem.第四天)))
            {
                DailyItem_mask.SetValue(DailyItem.第四天, true);
                Say(pc, 0, "第四天了呢，$R" + "今天給你一份令你展顏的獎勵吧。", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY4-5");
                return;
            }
            //第五天獎勵
            if ((DailyItem_mask.Test(DailyItem.第四天)) && !(DailyItem_mask.Test(DailyItem.第五天)))
            {
                DailyItem_mask.SetValue(DailyItem.第五天, true);
                Say(pc, 0, "嗯嗯$R這是第五天的每日獎勵。", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY4-5");
                return;
            }
            //第六天獎勵
            if ((DailyItem_mask.Test(DailyItem.第五天)) && !(DailyItem_mask.Test(DailyItem.第六天)))
            {
                DailyItem_mask.SetValue(DailyItem.第六天, true);
                Say(pc, 0, "差點忘了$R這是第六天的每日獎勵。", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY6-7");
                return;
            }
            //第七天獎勵
            if ((DailyItem_mask.Test(DailyItem.第六天)) && !(DailyItem_mask.Test(DailyItem.第七天)))
            {
                DailyItem_mask.SetValue(DailyItem.第七天, true);
                Say(pc, 0, "來了來了$R這是第七天的每日獎勵。", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY6-7");
                return;
            }
            //第八天獎勵
            if ((DailyItem_mask.Test(DailyItem.第七天)) && !(DailyItem_mask.Test(DailyItem.第八天)))
            {
                DailyItem_mask.SetValue(DailyItem.第八天, true);
                Say(pc, 0, "第八天啦$R給你這個吧。", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY8-9");
                return;
            }
            //第九天獎勵
            if ((DailyItem_mask.Test(DailyItem.第八天)) && !(DailyItem_mask.Test(DailyItem.第九天)))
            {
                DailyItem_mask.SetValue(DailyItem.第九天, true);
                Say(pc, 0, "這是第九天的每日獎勵。$R明天有大份一點的獎勵呢。", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY8-9");
                return;
            }
            //第十天獎勵
            if ((DailyItem_mask.Test(DailyItem.第九天)) && !(DailyItem_mask.Test(DailyItem.第十天)))
            {
                DailyItem_mask.SetValue(DailyItem.第一天, false);
                DailyItem_mask.SetValue(DailyItem.第二天, false);
                DailyItem_mask.SetValue(DailyItem.第三天, false);
                DailyItem_mask.SetValue(DailyItem.第四天, false);
                DailyItem_mask.SetValue(DailyItem.第五天, false);
                DailyItem_mask.SetValue(DailyItem.第六天, false);
                DailyItem_mask.SetValue(DailyItem.第七天, false);
                DailyItem_mask.SetValue(DailyItem.第八天, false);
                DailyItem_mask.SetValue(DailyItem.第九天, false);
                Say(pc, 0, "恭喜你，這是第十天的每日獎勵。$R" +
                    "下次開始便會重新再算一次哦", "招待泰迪");
                PlaySound(pc, 2429, false, 100, 50);
                Wait(pc, 1000);
                GiveRandomTreasure(pc, "DAILYREWARDDAY10");
                return;
            }
        }
    }
}

