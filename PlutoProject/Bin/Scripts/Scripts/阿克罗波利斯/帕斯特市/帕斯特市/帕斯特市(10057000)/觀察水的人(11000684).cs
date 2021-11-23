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
                Say(pc, 131, "啊！是老婆寄来的？$R;" +
                    "多谢您帮我拿过来$R;" +
                    "$R动不动就忘了带便当，是老毛病了$R;" +
                    "哈哈哈哈！$R;" +
                    "$P为了表达我的谢意$R;" +
                    "给您唱一首歌好，$R;" +
                    "还是给您技能点数好？$R;");
                switch (Select(pc, "哪一个好?", "", "技能点数", "唱歌"))
                {
                    case 1:
                        mask.SetValue(PSTFlags.交出便當, true);
                        //_5a18 = true;
                        Wait(pc, 2000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 2000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "技能点数提升1$R;");
                        Say(pc, 131, "那样意思是…$R;" +
                            "我的歌不如技能点数啊$R;");
                        break;
                    case 2:
                        mask.SetValue(PSTFlags.交出便當, true);
                        //_5a18 = true;
                        Say(pc, 131, "想听听我的歌？$R;" +
                            "啊那开始了！$R;" +
                            "$P法伊斯特的早晨很安静！$R;" +
                            "非常好啊$R;" +
                            "$R法伊斯特的晚上也是安静的！$R;" +
                            "非常好啊$R;" +
                            "$P暖暖和和的天气，一起去散步吧$R;" +
                            "$R孩子们开心的玩耍着$R;" +
                            "今天也是好天气$R;" +
                            "$P哈哈！$R;" +
                            "非常高兴您来聆听我的歌！$R;" +
                            "$R为了表示感谢$R;" +
                            "给您技能点数吧！$R;");
                        Wait(pc, 2000);
                        PlaySound(pc, 3087, false, 100, 50);
                        ShowEffect(pc, 4131);
                        Wait(pc, 2000);
                        SkillPointBonus(pc, 1);
                        Say(pc, 131, "技能点数提升1$R;");
                        break;
                }
                return;
            }
            Say(pc, 131, "嘿！小傢伙！$R;" +
                "要不要和我玩猜谜游戏？$R;");
            switch (Select(pc, "怎么办?", "", "挑战猜谜游戏！", " …幼稚，玩那种… "))
            {
                case 1:
                    string a;
                    int xz = Global.Random.Next(1, 3);
                    switch (xz)
                    {
                        case 1:
                            Say(pc, 131, "红色小狗的名字是什么？$R;");

                            a = string.Format(InputBox(pc, "请输入红色小狗的名字", InputType.PetRename));
                            //"INPUTSTRING STR1 ""请输入红色小狗的名字""              "
                            Wait(pc, 1000);
                            if (a == "钢洛")
                            {
                                Say(pc, 131, "噢噢！正确答案！$R;" +
                                    "马上就答对了！$R;" +
                                    "太简单了吗？$R;" +
                                    "$R为了答谢，我就告诉您暗号吧$R;" +
                                    "$P『谜之团和朋友』$R;" +
                                    "$P好…到止为止$R;" +
                                    "$R什么密码？$R;" +
                                    "那个…$R;" +
                                    "我也不能告诉您$R;");
                                return;
                            }
                            Say(pc, 131, "嗯！真是可惜！$R;" +
                                "$R小狗在这附近，找看看吧！$R;");
                            break;
                        case 2:
                            Say(pc, 131, "黑色小狗的名字是什么？$R;");
                            a = string.Format(InputBox(pc, "请输入黑色小狗的名字", InputType.PetRename));
                            //"INPUTSTRING STR1 ""请输入黑色小狗的名字""            "
                            Wait(pc, 1000);
                            if (a == "雷因宝乌")
                            {
                                Say(pc, 131, "噢噢！正确答案！$R;" +
                                    "马上就答对了！$R;" +
                                    "太简单了吗？$R;" +
                                    "$R为了答谢，我就告诉您暗号吧$R;" +
                                    "$P『谜之团和朋友』$R;" +
                                    "$P好…到止为止$R;" +
                                    "$R什么密码？$R;" +
                                    "那个…$R;" +
                                    "我也不能告诉您$R;");
                                return;
                            }
                            Say(pc, 131, "嗯！真是可惜！$R;" +
                                "$R小狗在这附近，找看看吧！$R;");
                            break;
                        case 3:
                            Say(pc, 131, "斑点小狗的名字是什么？$R;");
                            a = string.Format(InputBox(pc, "请输入斑点小狗的名字", InputType.PetRename));
                            //"INPUTSTRING STR1 ""请输入斑点小狗的名字""              "
                            Wait(pc, 1000);
                            if (a == "帕迪3世")
                            {
                                Say(pc, 131, "噢噢！正确答案！$R;" +
                                    "马上就答对了！$R;" +
                                    "太简单了吗？$R;" +
                                    "$R为了答谢，我就告诉您密码吧$R;" +
                                    "$P『谜之团和朋友』$R;" +
                                    "$P好…到止为止$R;" +
                                    "$R什么密码？$R;" +
                                    "那个…$R;" +
                                    "我也不能告诉您$R;");
                                return;
                            }
                            Say(pc, 131, "嗯！真是可惜！$R;" +
                                "$R小狗在这附近，找看看吧！$R;");
                            break;
                    }
                    break;
            }
        }
    }
}