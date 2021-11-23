using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10018104
{
    public class S18000180 : Event
    {
        public S18000180()
        {
            this.EventID = 18000180;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Beginner_01> Beginner_01_mask = new BitMask<Beginner_01>(pc.CMask["Beginner_01"]);
            //int selection;
            if (Beginner_01_mask.Test(Beginner_01.埃米爾給予埃米爾徽章))
            {

                ShowEffect(pc, 18000179, 4501);



                if (pc.Pet != null)
                {
                    Say(pc, 18000179, 361, "あぁーー！！$R;" +
                "ね、ネコマタとマリオネットは$R;" +
                "返してもらうよ！$R;", "エミル");
                    ShowEffect(pc, 18000179, 5146);
                }
                else
                {
                    TakeItem(pc, 10013852, 1);
                    TakeItem(pc, 10017920, 1);
                    Say(pc, 18000179, 131, "やっぱり、君がいないと$R;" +
                    "肩が落ち着かないね。$R;", "エミル");
                    Warp(pc, 30201003, 7, 12);
                }
            }
            else
            {
               话没说完(pc);
                return;
            }
        }
        void 话没说完(ActorPC pc)
        {
            Say(pc, 18000179, 131, "あ！$R;" +
            "まだ僕の説明が終わってないよ！$R;", "エミル");

        }
        }
    }

