using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10062000
{
    public class S11001071 : Event
    {
        public S11001071()
        {
            this.EventID = 11001071;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Fame < 10)
            {
                Say(pc, 131, "对对，真不错呀$R;");
                Say(pc, 11001072, 364, "汪汪$R;" +
                    "汪汪！！$R;");
                return;
            }
            Say(pc, 131, "对对，真不错呀$R;");
            Say(pc, 11001072, 364, "汪汪$R;" +
                "汪汪！！$R;");
            Say(pc, 131, "啊，您也想治疗宠物吗？$R;");

            switch (Select(pc, "想怎么做呢？", "", "治疗", "什么也不做"))
            {
                case 1:
                    /*
                    if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    {
                        if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048900 ||
                            pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048901 ||
                            pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048902 ||
                            pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048950 ||
                            pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048951 ||
                            pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10048952 ||
                            pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10006650 ||
                            pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10006651)
                        {
                            if (!_4a73)
                            {
                                _4a73 = true;
                                Say(pc, 131, "第一次接受寵物治療嗎？$R;" +
                                    "$R在這裡可以恢復$R;" +
                                    "寵物的「親密度」$R;" +
                                    "$P但是要恢復降低的親密度，$R;" +
                                    "是需要時間的，還要用愛心$R;" +
                                    "$R要常常來看呀$R;");
                                //GOTO EVT1100107100
                                return;
                            }
                            if (a//ME.EVSTAT00 = TIME.EDAY
                            )
                            {
                                Say(pc, 131, "嗯，看了一下記錄$R;" +
                                    "今天好像有接受治療的傢伙$R;" +
                                    "$R雖然表面看起來很健康$R;" +
                                    "病情也有可能突然惡化呀$R;" +
                                    "$P今天要好好觀察一整天啊$R;" +
                                    "$R那麼，就拜託您了$R;");
                                return;
                            }
                            //PETREPAIR EQUIP 3
                            //SWITCH START
                            //ME.WORK0 = 0 EVT1100107105
                            //ME.WORK0 = -1 EVT1100107106
                            //ME.WORK0 = -2 EVT1100107107
                            //ME.WORK0 = -3 EVT1100040099
                            //ME.WORK0 = -4 EVT1100107108
                            //SWITCH END
                            //EVENTEND
                            //EVT1100107103
                            //EVENTEND
                            //EVT1100107104
                            //EVENTEND
                            //EVT1100107105
                            //PARAM ME.EVSTAT00 = TIME.EDAY
                            Wait(pc, 0);
                            ShowEffect(pc, 4154);
                            Wait(pc, 0);
                            ShowEffect(pc, 4154);
                            Wait(pc, 0);
                            ShowEffect(pc, 4154);
                            Wait(pc, 1000);
                            if (//EX.COUNTRY_CODE = 0
                            a && !_Xa52 && CheckInventory(pc, 10009111, 1))
                            {
                                Call(EVT1100107109);
                                return;
                            }
                            Say(pc, 131, "親密度恢復「3」了$R;" +
                                "真是太好了$R;" +
                                "今天就到這裡啦$R;" +
                                "$R明天再來吧$R;");
                            //EVENTEND
                            //EVT1100107106
                            Say(pc, 131, "啊，怎麼會$R;" +
                                "再看一次嗎？$R;");
                            //EVENTEND
                            //EVT1100107107
                            Say(pc, 131, "先裝備寵物吧$R;");
                            //EVENTEND
                            //EVT1100107108
                            Say(pc, 131, "没有疼的症狀嗎？$R;");
                            //EVENTEND
                            //EVT1100107109
                            _Xa52 = true;
                            GiveItem(pc, 10009111, 1);
                            Say(pc, 131, "親密度恢復「3」了$R;" +
                                "還算不錯$R;" +
                                "$R來，這是特別的禮物$R;" +
                                "是我老師做的糖果唷$R;" +
                                "使用這個親密度可以恢復「15」$R;" +
                                "$P老師真了不起呀$R;" +
                                "想完全治好寵物的話$R;" +
                                "讓老師診斷一下吧$R;" +
                                "$R老師就住在帕斯特啊$R;");
                            //EVENTEND
                        }
                    }
                    */
                    Say(pc, 131, "对不起，$R我是小狗专家$R;" +
                        "不能治疗别的宠物啊$R;");
                    break;
                case 2:
                    break;
            }
        }
    }
}