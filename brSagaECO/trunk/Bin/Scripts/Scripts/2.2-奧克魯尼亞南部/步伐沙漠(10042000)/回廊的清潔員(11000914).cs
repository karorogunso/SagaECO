using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
namespace SagaScript.M10042000
{
    public class S11000914 : Event
    {
        public S11000914()
        {
            this.EventID = 11000914;
        }

        public override void OnEvent(ActorPC pc)
        {
            int selection;
            BitMask<JLFlags> mask = new BitMask<JLFlags>(pc.CMask["JL"]);
            if (!mask.Test(JLFlags.回廊清洁员第一次對話))
            {
                Say(pc, 255, "呼～休息一會兒吧…$R;" +
                    "$R正在打掃寵物養殖場$R灰塵很多，真煩人…$R;" +
                    "$R對了！這個紙信封是為了灰塵準備的唷$R;" +
                    "$P想不想去呢？$R想去我就給您帶路…$R;");
                mask.SetValue(JLFlags.回廊清洁员第一次對話, true);
            }
            else
            {
                Say(pc, 255, "那麼！要去回廊寵物養殖場?$R;");
            }
            selection = Select(pc, "去回廊寵物養殖場嗎？", "", "去", "不去", "什麼叫回廊寵物養殖場？");
            while (selection != 2)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 255, "順便把裡邊也打掃一下吧~$R;");
                        Say(pc, 255, "未實裝~$R;");
                        return;
                    case 2:
                        return;
                    case 3:
                        Say(pc, 255, "是無限回廊的附加設施$R是為了不想影響其他人$R但又想養寵物的人，而增設的$R;" +
                            "$R一共3層$R在哪養什麼寵物$R隨您心意就可以了$R;");
                        break;
                }
                selection = Select(pc, "去回廊寵物養殖場嗎？", "", "去", "不去", "什麼叫回廊寵物養殖場？");
            }

        }
    }
}