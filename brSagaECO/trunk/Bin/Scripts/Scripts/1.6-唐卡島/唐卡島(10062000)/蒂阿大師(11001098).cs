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
    public class S11001098 : Event
    {
        public S11001098()
        {
            this.EventID = 11001098;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<FGarden> fgarden = pc.AMask["FGarden"];

            if (!fgarden.Test(FGarden.铁板收集完毕))
            {
                Say(pc, 131, "嗯!$R;" +
                    "$R我們是飛空艇的庭院專家$R;" +
                    "「庭院大師」$R;");
                return;
            }

            Say(pc, 131, "我就是木偶製作大師蒂阿$R;" +
                "現在開始簡單講解$R『奧克魯尼亞航空法』$R;" +
                "$R可以開始了嗎？$R;");
            switch (Select(pc, "準備……", "", "還沒好", "OK"))
            {
                case 1:
                    break;
                case 2:
                    int a = 0;
                    do
                    {
                        Say(pc, 131, "先談關於燃料的話題$R;" +
                            "飛空庭使用的燃料是『摩根炭』唷，$R;" +
                            "$R是從叫摩根的地方採掘的炭。$R;" +
                            "$R可以在摩根市的冶煉所或$R『家具匠人』那裡購買。$R;" +
                            "$P如果職業是礦工的話，可以直接在$R;" +
                            "摩根市的採掘場開採唷。$R;" +
                            "$R可以說是最經濟的方法。$R;" +
                            "下面是關於乘搭須知。$R;" +
                            "$R飛空庭飛行時，乘坐在飛空庭的人$R都會一起移動。$R;" +
                            "飛行前，最基本的就是$R通知目的地$R;" +
                            "$P因為如果突然移動，$R會引起乘客恐慌唷$R;" +
                            "$P最後談關於目的地。$R;" +
                            "$R只要有港口的地方都可以去。$R;" +
                            "$P不過不可以隨意進入別國的領土，$R不然會違法的喔。$R;" +
                            "$P想去自己所屬國家以外的領土時，$R;" +
                            "先從飛空庭下來，$R辦理正式手續後入境，就可以了。$R;" +
                            "$P講座到這裡結束。$R;" +
                            "有什麼要問的嗎？$R;");
                        a = Select(pc, "都清楚了嗎？", "", "還不太理解。", "OK");
                        switch (a)
                        {
                            case 1:
                                Say(pc, 131, "OK$R;" +
                                    "那麼再說明一次吧。$R;");
                                break;
                            case 2:
                                Say(pc, 131, "OK$R;" +
                                    "$R這個規則很重要$R;" +
                                    "$R隨時都可以為您說明，$R想問的時候，再過來吧。$R;");
                                if (fgarden.Test(FGarden.听完飞空庭飞行规则))
                                    return;

                                fgarden.SetValue(FGarden.听完飞空庭飞行规则, true);
                                Say(pc, 131, "加固工程已經結束了沒有啊？$R;" +
                                    "$R木偶製作大師雪列娜$R;" +
                                    "工程結束了嗎？$R;");
                                Say(pc, 11001026, 131, "已經結束了。$R;" +
                                    "給您講解下一步說明，$R跟我說話吧。$R;");
                                break;
                        }
                    } while (a == 1);
                    break;
            }
        }
    }
}