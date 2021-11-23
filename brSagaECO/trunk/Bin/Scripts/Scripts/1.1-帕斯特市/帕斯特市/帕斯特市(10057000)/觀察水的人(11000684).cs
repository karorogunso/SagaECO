using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000684 : Event
    {
        public S11000684()
        {
            this.EventID = 11000684;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            if (!mask.Test(PSTFlags.交出便當) && CountItem(pc, 10043304) >= 1)//_5a18
            {
                TakeItem(pc, 10043304, 1);
                Say(pc, 131, "啊！是老婆寄來的？$R;" +
                    "多謝您幫我拿過來$R;" +
                    "$R動不動就忘了帶便當，是老毛病了$R;" +
                    "哈哈哈哈！$R;" +
                    "$P為了表達我的謝意$R;" +
                    "給您唱一首歌好，$R;" +
                    "還是給您技能點數好？$R;");
                switch (Select(pc, "哪一個好?", "", "技能點數", "唱歌"))
                {
                    case 1:
                        mask.SetValue(PSTFlags.交出便當, true);
                        //_5a18 = true;
                        Wait(pc, 2000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 2000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "技能點數提升1$R;");
                        Say(pc, 131, "那樣意思是…$R;" +
                            "我的歌不如技能點數啊$R;");
                        break;
                    case 2:
                        mask.SetValue(PSTFlags.交出便當, true);
                        //_5a18 = true;
                        Say(pc, 131, "想聽聽我的歌？$R;" +
                            "啊那開始了！$R;" +
                            "$P帕斯特的早晨很安靜！$R;" +
                            "非常好啊$R;" +
                            "$R帕斯特的晚上也是安靜的！$R;" +
                            "非常好啊$R;" +
                            "$P暖暖和和的天氣，一起去散步吧$R;" +
                            "$R孩子們開心的玩耍著$R;" +
                            "今天也是好天氣$R;" +
                            "$P哈哈！$R;" +
                            "非常高興您來聆聽我的歌！$R;" +
                            "$R為了表示感謝$R;" +
                            "給您技能點數吧！$R;");
                        Wait(pc, 2000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 2000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "技能點數提升1$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "嘿！小傢伙！$R;" +
                "要不要和我玩猜謎遊戲？$R;");
            switch (Select(pc, "怎麽辦?", "", "挑戰猜謎遊戲！", " …幼稚，玩那種… "))
            {
                case 1:
                    string a;
                    int xz = Global.Random.Next(1, 3);
                    switch (xz)
                    {
                        case 1:
                            Say(pc, 131, "紅色小狗的名字是什麽？$R;");

                            a = string.Format(InputBox(pc, "請輸入紅色小狗的名字", InputType.PetRename));
                            //"INPUTSTRING STR1 ""請輸入紅色小狗的名字""              "
                            Wait(pc, 1000);
                            if (a == "鋼洛")
                            {
                                Say(pc, 131, "噢噢！正確答案！$R;" +
                                    "馬上就答對了！$R;" +
                                    "太簡單了嗎？$R;" +
                                    "$R為了答謝，我就告訴您密碼吧$R;" +
                                    "$P『謎語團和朋友』$R;" +
                                    "$P好…到止為止$R;" +
                                    "$R什麽密碼？$R;" +
                                    "那個…$R;" +
                                    "我也不能告訴您$R;");
                                return;
                            }
                            Say(pc, 131, "嗯！真是可惜！$R;" +
                                "$R小狗在這附近，找看看吧！$R;");
                            break;
                        case 2:
                            Say(pc, 131, "黑色小狗的名字是什麽？$R;");
                            a = string.Format(InputBox(pc, "請輸入黑色小狗的名字", InputType.PetRename));
                            //"INPUTSTRING STR1 ""請輸入黑色小狗的名字""            "
                            Wait(pc, 1000);
                            if (a == "雷因寶烏")
                            {
                                Say(pc, 131, "噢噢！正確答案！$R;" +
                                    "馬上就答對了！$R;" +
                                    "太簡單了嗎？$R;" +
                                    "$R為了答謝，我就告訴您密碼吧$R;" +
                                    "$P『謎語團和朋友』$R;" +
                                    "$P好…到止為止$R;" +
                                    "$R什麽密碼？$R;" +
                                    "那個…$R;" +
                                    "我也不能告訴您$R;");
                                return;
                            }
                            Say(pc, 131, "嗯！真是可惜！$R;" +
                                "$R小狗在這附近，找看看吧！$R;");
                            break;
                        case 3:
                            Say(pc, 131, "斑點小狗的名字是什麽？$R;");
                            a = string.Format(InputBox(pc, "請輸入斑點小狗的名字", InputType.PetRename));
                            //"INPUTSTRING STR1 ""請輸入斑點小狗的名字""              "
                            Wait(pc, 1000);
                            if (a == "帕迪3世")
                            {
                                Say(pc, 131, "噢噢！正確答案！$R;" +
                                    "馬上就答對了！$R;" +
                                    "太簡單了嗎？$R;" +
                                    "$R為了答謝，我就告訴您密碼吧$R;" +
                                    "$P『謎語團和朋友』$R;" +
                                    "$P好…到止為止$R;" +
                                    "$R什麽密碼？$R;" +
                                    "那個…$R;" +
                                    "我也不能告訴您$R;");
                                return;
                            }
                            Say(pc, 131, "嗯！真是可惜！$R;" +
                                "$R小狗在這附近，找看看吧！$R;");
                            break;
                    }
                    break;
            }
        }
    }
}