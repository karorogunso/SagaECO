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
                Say(pc, 255, "呼～休息一会儿吧…$R;" +
                    "$R正在打扫宠物养殖场$R灰尘很多，真烦人…$R;" +
                    "$R对了！这个纸信封是为了灰尘准备的$R;" +
                    "$P想不想去呢？$R想去我就给您带路…$R;");
                mask.SetValue(JLFlags.回廊清洁员第一次對話, true);
            }
            else
            {
                Say(pc, 255, "那么！要去回廊宠物养殖场?$R;");
            }
            selection = Select(pc, "去回廊宠物养殖场吗？", "", "去", "不去", "什么叫回廊宠物养殖场？");
            while (selection != 2)
            {
                switch (selection)
                {
                    case 1:
                        Say(pc, 255, "顺便把里边也打扫一下吧~$R;");
                        Say(pc, 255, "未实装~$R;");
                        return;
                    case 2:
                        return;
                    case 3:
                        Say(pc, 255, "是无限回廊的附加设施$R是为了不想影响其他人$R但又想养宠物的人，而增设的$R;" +
                            "$R一共3层$R在哪养什么宠物$R随您心意就可以了$R;");
                        break;
                }
                selection = Select(pc, "去回廊宠物养殖场吗？", "", "去", "不去", "什么叫回廊宠物养殖场？");
            }

        }
    }
}