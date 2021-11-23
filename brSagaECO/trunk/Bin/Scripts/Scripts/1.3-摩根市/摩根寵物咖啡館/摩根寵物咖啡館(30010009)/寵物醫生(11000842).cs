using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010009
{
    public class S11000842 : Event
    {
        public S11000842()
        {
            this.EventID = 11000842;
        }

        public override void OnEvent(ActorPC pc)
        {

            if (pc.Fame < 10)
            {
                Say(pc, 190, "您好$R;");
                return;
            }
            Say(pc, 190, "這，這是怎麼回事阿？$R;");
            //EVT1100084207
            switch (Select(pc, "想怎麼做呢？", "", "治療寵物", "什麼也不做"))
            {
                case 1:
                    PetRecover(pc, 1);
                    break;
                case 2:
                    break;
            }

        }

        void 治療寵物(ActorPC pc)
        {
            /*
            //EVT1100084200
            if (pc.Account.GMLevel == 1)
            {
                Call(EVT1100084298);
                return;
            }
            if (!_4a73)
            {
                _4a73 = true;
                Say(pc, 190, "第一次接受寵物治療嗎？$R;" +
                    "$R在這裡可以恢復$R;" +
                    "寵物的「親密度」$R;" +
                    "$P但是要恢復降低的親密度，$R;" +
                    "是需要時間的，還要用愛心$R;" +
                    "$R要常常來看呀$R;");
                //GOTO EVT1100084207
                return;
            }
            //PARAM ME.WORK0 = TIME.EDAY
            if (a//ME.EVSTAT00 = ME.WORK0
            )
            {
                Say(pc, 190, "嗯，看了一下記錄$R;" +
                    "今天好像有接受治療的傢伙$R;" +
                    "$R雖然表面看起來很健康$R;" +
                    "病情也有可能突然惡化呀$R;" +
                    "$P今天要好好觀察一整天啊$R;" +
                    "$R那麼，就拜託您了$R;");
                //EVENTEND
                return;
            }
            //EVT1100084298
            //PETREPAIR 1
            //SWITCH START
            //ME.WORK0 = 0 EVT1100084204
            //ME.WORK0 = -1 EVT1100084205
            //ME.WORK0 = -2 EVT1100084206

            //SWITCH END
            //EVENTEND

            //EVT1100084204
            //PARAM ME.EVSTAT00 = TIME.EDAY
            Wait(pc, 0);
            ShowEffect(pc, 4154);
            Wait(pc, 2000);
            if (a//EX.COUNTRY_CODE = 0
            && !_Xa52 && CheckInventory(pc, 10009111, 1))
            {
                _Xa52 = true;
                GiveItem(pc, 10009111, 1);
                Say(pc, 190, "親密度恢復「1」了$R;" +
                    "還算不錯$R;" +
                    "$R來，這是特別的禮物$R;" +
                    "是我老師做的糖果唷$R;" +
                    "使用這個親密度可以恢復「15」唷$R;" +
                    "$P老師真了不起呀$R;" +
                    "想完全治好寵物的話$R;" +
                    "讓老師診斷一下吧$R;" +
                    "老師就住在帕斯特啊$R;");
                return;
            }
            Say(pc, 190, "親密度恢復「1」了$R;" +
                "還算不錯$R;" +
                "今天的治療到這裡結束吧$R;" +
                "明天再來呀$R;");
            //EVENTEND
            //EVT1100084205
            Say(pc, 190, "哦，奇怪$R;" +
                "再試一次吧$R;");
            //EVENTEND
            //EVT1100084206
            Say(pc, 190, "看樣子没有必要治療了$R;");
            //EVENTEND
            //EVT1100084208
            //EVENTEND
            */
        }
    }
}