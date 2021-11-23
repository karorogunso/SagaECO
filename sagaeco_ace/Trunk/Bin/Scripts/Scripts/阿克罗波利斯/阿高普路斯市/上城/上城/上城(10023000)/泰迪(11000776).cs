using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:泰迪(11000776) X:127 Y:153
namespace SagaScript.M10023000
{
    public class S11000776 : Event
    {
        public S11000776()
        {
            this.EventID = 11000776;
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]); 

            if (Acropolisut_01_mask.Test(Acropolisut_01.第一次跟上城泰迪对话))
            {
                第一次对话完毕(pc);
                return;
            }
            Acropolisut_01_mask.SetValue(Acropolisut_01.第一次跟上城泰迪对话, true);
            Say(pc, 0, 0, "…!!$R;" +
                          "$P活动木偶泰迪?$R;" +
                          "$R不是，不是活动木偶泰迪，$R;" +
                          "也不是石像呀!$R;" +
                          "$R但是泰迪怎么在这里站着啊…?$R;", " ");

            Say(pc, 11000776, 131, "喜欢神奇的东西吗?$R;", "泰迪");

            Say(pc, 0, 0, "……!!$R;" +
                          "泰迪说话了吗?$R;", " ");
        }

            void 第一次对话完毕(ActorPC pc)
        {
            byte x, y;
            BitMask<Acropolisut_01> Acropolisut_01_mask = new BitMask<Acropolisut_01>(pc.CMask["Acropolisut_01"]); 
            Acropolisut_01_mask.SetValue(Acropolisut_01.上城泰迪那里传送到泰迪岛, true);
            Say(pc, 11000776, 131, "想不想去神奇的地方呢?$R;", "泰迪");

            switch (Select(pc, "想去吗?", "", "想去", "不想去"))
            {
                case 1:
                    Say(pc, 11000776, 131, "$R那么闭上眼睛吧。$R;" +
                                           "$P我喊三声!$R;" +
                                           "$P一、二、三!!$R;", "泰迪");

                    Say(pc, 0, 0, "……??$R;" +
                                  "哎呀，怎么这么困呢?$R;" +
                                  "$P……$R;", " ");

                    Wait(pc, 990);

                    x = (byte)Global.Random.Next(243, 250);
                    y = (byte)Global.Random.Next(80, 86);

                    Warp(pc, 10071000, x, y);
                    break;

                case 2:
                    Say(pc, 11000776, 131, "嗯? 不去吗?$R;" +
                                           "真是爱管闲事的傢伙呀!$R;", "泰迪");
                    break;
            }
        }
    }
}
