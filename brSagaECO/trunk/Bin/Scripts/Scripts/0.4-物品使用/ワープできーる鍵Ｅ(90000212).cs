using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB;
using SagaDB.Actor;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
//アイテム名：ワープできーる鍵Ｅ　アイテムID:16000300
namespace SagaScript
{
    public class S90000212 : Event
    {
        public S90000212()
        {
            this.EventID = 90000212;
        }

        public override void OnEvent(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
            {
                Say(pc, 0, "在道米尼界無法使用");
                
                /*
                Say(pc, 0, "ドミニオン界では使用できません");
                */
            }
            else
            {
                switch (Select(pc, "請問要前往哪地圖呢？", "", "復活戰士的契約地點", "魔法之蝶的契約地點", "不前往"))
                {
                    case 1:
                        switch (Select(pc, "請問要前往哪地圖呢？", "",
                            "樂天娜湖泊", "東方海角", "鬼之寢岩", "舒諾武雪原", "北方海角",
                            "南方海角", "莫古城", "殺人蜂的山隘通路", "東阿古諾尼亞海岸", "北部地牢前",
                            "阿伊恩市上層", "上城區東可動橋", "諾頓散步長廊", "法依斯特市", "不前往"))
                        {
                            case 1:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10032000, 139, 113);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 2:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10018100, 230, 89);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 3:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10061000, 138, 46);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 4:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10002000, 45, 219);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 5:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10001000, 100, 24);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 6:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10046000, 153, 229);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 7:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10060000, 164, 147);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 8:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10020000, 107, 75);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 9:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10034000, 35, 52);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 10:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10050000, 226, 134);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 11:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10063100, 132, 156);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 12:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10023100, 250, 131);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 13:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10065000, 46, 121);
                                TakeItem(pc, 16000300, 1);
                                break;
                            case 14:
                                ShowEffect(pc, 4011);
                                Warp(pc, 10057000, 8, 123);
                                TakeItem(pc, 16000300, 1);
                                break;
                        }
                        break;
                    case 2:
                        BitMask<MagicButterfly> MagicButterfly_Mask = pc.CMask["MagicButterfly"];
                        switch (Select(pc, "請問要前往哪地圖呢？","",""))
                        {
                            case 1:
                                break;
                        }
                        break;
                }
                

                /*
                switch (Select(pc, "請問要前往哪地圖呢？", "",
                "樂天娜湖泊", "東方海角", "鬼之寢岩", "舒諾武雪原", "北方海角",
                "南方海角", "莫古城", "殺人蜂的山隘通路", "東阿古諾尼亞海岸", "北部地牢前",
                "阿伊恩市上層", "上城區東可動橋", "諾頓散步長廊", "法依斯特市", "不前往"))
                */

                /*
                switch (Select(pc, "どこへワープしますか？", "",
                    "ウテナ湖", "イストー岬", "鬼ノ寝床岩", "スノップ雪原", "ノーザリン岬",
                    "サウスリン岬", "モーグシティ", "キラービーの峠道", "東アクロニア海岸", "ノーザンダンジョン前",
                    "アイアンシティ上層階", "アップタウン東可動橋", "ノーザンプロムナード", "ファーイーストシティ", "しない"))
                */
            }
        }
    }
}
