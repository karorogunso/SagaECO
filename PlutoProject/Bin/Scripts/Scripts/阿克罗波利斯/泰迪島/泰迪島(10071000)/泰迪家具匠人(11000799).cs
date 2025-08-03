using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:泰迪家具匠人(11000799) X:8 Y:80
namespace SagaScript.M10071000
{
    public class S11000799 : Event
    {
        public S11000799()
        {
            this.EventID = 11000799;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 10022700) > 0)
            {
                Say(pc, 131, "……！$R;" +
                "$R「タイニーメダル」3個と「りんご」100個で$R;" +
                "「小さな家」を「タイニー様式」に改築してくれます。$R;" +
                "重くて持てない人は友達に協力してもらいましょう。$R;", "タイニー家具職人");
                if (CountItem(pc, 10050300) >= 3 && CountItem(pc, 10002800) >= 100 && CountItem(pc, 30001200) >= 1)
                {
                    TakeItem(pc, 10050300, 3);
                    TakeItem(pc, 10002800, 100);
                    TakeItem(pc, 30001200, 1);
                    PlaySound(pc, 3140, false, 100, 50);
                    Wait(pc, 3000);
                    GiveItem(pc, 30000600, 1);
                    return;
                    

                }
                else
                {
                    Say(pc, 190, "材料不够额！$R;");
                    return;
                }
               
            }
            Say(pc, 131, "……つんっ！$R;" +
            "$Rもっともっと、家に$R;" +
            "詳しくなってから来るんだな！$R;" +
            "ここの敷居は高いんだぜぃっ！$R;", "タイニー家具職人");
        }
    }
}