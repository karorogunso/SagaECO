using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30075000
{
    public class S11000286 : Event
    {
        public S11000286()
        {
            this.EventID = 11000286;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (a//ME.JOB > 110
             )
            {
                Call(EVT1100028607);
                return;
            }
            if (a//ME.PET_TRANSNUM > 0
            )
            {
                Call(EVT1100028607);
                return;
            }
            */
            if (pc.Level < 30)
            {
                Say(pc, 131, "回去吧$R;");
                return;
            }
            Say(pc, 131, "您看起来比较有钱啊$R;" +
                "我们是佣兵军团。$R;" +
                "只要给钱，我们什么都可以做呢$R;");
            switch (Select(pc, "雇佣佣兵吗？", "", "好", "不要"))
            {
                case 1:
                    Say(pc, 131, "说重点吧，给多少？$R;");
                    int count = int.Parse(InputBox(pc, "请输入金额", InputType.ItemCode));
                    int a = pc.Level * 3000;
                    if (count < a)
                    {
                        Say(pc, 131, "别开玩笑吧！$R;" +
                            "那点钱，谁干啊？$R;");
                        return;
                    }
                    if (pc.Gold < a)
                    {
                        Say(pc, 131, "钱不够啊$R;");
                        return;
                    }
                    if (CheckInventory(pc, 10009800, 1))
                    {
                        pc.Gold -= count;
                        GiveItem(pc, 10009800, 1);
                        Say(pc, 131, "好吧，给您介绍个有实力的佣兵吧$R;" +
                            "$R召唤佣兵的时候，使用这个道具吧。$R;");
                        return;
                    }
                    Say(pc, 131, "行李好像太多了，整理一下吧$R;");
                    break;
                case 2:
                    Say(pc, 131, "回去吧$R;");
                    break;
            }

            /*
            //EVT1100028607
            Say(pc, 131, "哦，老大$R;" +
                "$R好久沒見啊$R;");
            //PARAM STR1 = ME.PET_NAME

            Say(pc, 131, pc.Name +
                "不是嗎?$R;" +
                "$R好像過得不錯啊$R;" +
                "$P啊，等會兒，您的合約好像……$R;" +
                "$R我得確認一下文件$R;" +
                "$P對了！$R;" +
                "$R您的任期已經結束了$R;");
            //PARAM STR1 = ME.NAME
            //PARAM STR1 + ！
            Say(pc, 131, "STR1$R;" +
                "$R不好意思，他的傭兵合約已經期滿囉$R;" +
                "$R他已經是自由身了$R;");
            Say(pc, 131, "等等！$R;" +
                "$R已經約滿了？$R;" +
                "$R哎！都忘了$R;");
            Say(pc, 131, "您為了贖罪，選擇當傭兵。$R;" +
                "$R現在您已經平安無事約滿，$R回復自由身了。$R;" +
                "$P這段時間，真是辛苦了。$R;");
            //PARAM STR1 = ME.NAME
            //PARAM STR1 + ！
            Say(pc, 131, "STR1$R;" +
                "祝賀他有新的開始吧$R;" +
                "$R給您再介紹個新的傭兵吧，$R當然是免費的阿$R;");
            Say(pc, 131, "等會兒$R;" +
                "$R哪……哪有這樣子的？$R都在一起這麼長時間了，$R;" +
                "$R哪有就這麼分開的啊？$R開玩笑的吧？$R;");
            //PARAM STR1 = ME.PET_NAME
            //PARAM STR1 + ！?
            Say(pc, 131, "STR1$R;" +
                "$R你在說什麼？$R;" +
                "$R你已經成為自由身了，$R該高興啊$R;");
            Say(pc, 131, "算了吧，$R;" +
                "$R在這種黑黑沈沈地方的人，$R你有什麼資格插嘴呀?$R;" +
                "$P你這種人怎麼會理解我的心情啊？$R;");
            //PARAM STR1 = ME.NAME
            //PARAM STR1 + ！
            //PARAM STR2 = ME.PET_NAME
            //PARAM STR2 + …
            Say(pc, 131, "STR2$R;" +
                "$R…您好像是個好主人呢…$R;" +
                "STR1$R;" +
                "他雖然是這麼說的，$R但是他將來的人生還是很重要呀。$R;" +
                "$R您要怎麼辦？$R;");
            switch (Select(pc, "怎麼辦呢？", "", "換傭兵", "算了"))
            {
                case 1:
                    GOTO EVT1100028604;
                    break;
                case 2:
                    GOTO EVT1100028605;
                    break;
            }
            //EVENTEND
            //EVT1100028604
            Say(pc, 131, "……原來$R;" +
                "$R好吧，我們之間也就這點交情啊$R;");
            //PARAM STR1 = ME.PET_NAME
            //PARAM STR1 + !!
            Say(pc, 131, "STR1$R;" +
                "$R您在說什麼阿？$R您不知道他$R面臨這種選擇時，心情如何嗎？$R;");
            Say(pc, 131, "……$R;" +
                "$R我也知道，$R跟您一起的時候，真的很開心啊$R;" +
                "$P我會給繼任者傳授所有能力，$R讓他好好幫幫您。$R;" +
                "$P希望以後…$R還有機會和您並肩作戰。$R;" +
                "$R……$R;");
            PlaySound(pc, 2030, false, 100, 50);
            Say(pc, 131, "寵物隨著成長，外貌也會不同喔$R;" +
                "$R請注意阿$R;");
            switch (Select(pc, "真的要換傭兵嗎？", "", "換傭兵", "算了"))
            {
                case 1:
                    GOTO EVT1100028606;
                    break;
                case 2:
                    GOTO EVT1100028605;
                    break;
            }
            //EVENTEND
            //EVT1100028605
            Say(pc, 131, "啊，是嗎？$R;" +
                "$R呵呵，太好了，$R謝謝$R;" +
                "$P那麼以後，拜託您了。$R;");
            //EVENTEND
            //EVT1100028606
            PlaySound(pc, 4001, false, 100, 50);
            //FADE OUT BLACK
            Wait(pc, 5000);
            //FADE IN
            Wait(pc, 1000);
            //PETCHANGE TRANSFORM 10009801 ALL
            //GOTO EVT1100028608
            //EVENTEND
            //EVT1100028608
            Say(pc, 131, "他是新的傭兵，$R以後就拜託了$R;");
            Say(pc, 131, "拜託了$R;");
            //EVENTEND
            */
        }
    }
}