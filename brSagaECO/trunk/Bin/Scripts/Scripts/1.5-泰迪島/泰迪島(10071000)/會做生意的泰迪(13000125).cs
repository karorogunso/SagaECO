using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:會做生意的泰迪(13000125) X:229 Y:167
namespace SagaScript.M10071000
{
    public class S13000125 : Event
    {
        public S13000125()
        {
            this.EventID = 13000125;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 65535, "いらしゃい、いらしゃーい！$R;" +
            "タイニーが作る$R;" +
            "フシギなお菓子おいしいよー！$R;" +
            "$R1個300ゴールドだよー！$R;", "商売上手なタイニー");
            ShowEffect(pc, 5145);
            if (Select(pc, "300ゴールドだよー！", "", "タイニー焼きを買う", "買わない") == 1)
            {
                PlaySound(pc, 2060, false, 100, 50);

                Say(pc, 65535, "タイニー焼きは何種類かあるぞー。$R;" +
                "何番のタイニー焼きがいい？$R;" +
                "1から100までの数字の中で$R;" +
                "好きな数字を選べー。$R;", "商売上手なタイニー");
                ShowEffect(pc, 5145);
                string a = string.Format(InputBox(pc, "1から10までの数字を入力（半角）", InputType.PetRename));
                    Wait(pc, 1000);
                    if (a == "1")
                    {
                        ActivateMarionette(pc, 10022055);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "2")
                    {
                        ActivateMarionette(pc, 10022000);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "3")
                    {
                        ActivateMarionette(pc, 10022050);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "4")
                    {
                        ActivateMarionette(pc, 10022051);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "5")
                    {
                        ActivateMarionette(pc, 10022052);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "6")
                    {
                        ActivateMarionette(pc, 10022053);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "7")
                    {
                        ActivateMarionette(pc, 10050400);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "8")
                    {
                        ActivateMarionette(pc, 10050600);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "9")
                    {
                        ActivateMarionette(pc, 10050602);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
                    if (a == "10")
                    {
                        ActivateMarionette(pc, 10050700);
                        ShowEffect(pc, 8015);
                        Heal(pc);
                    }
            }
        }
    }
}