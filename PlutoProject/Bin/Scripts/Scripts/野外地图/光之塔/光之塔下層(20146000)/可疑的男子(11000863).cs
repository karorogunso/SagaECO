using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M20146000
{
    public class S11000863 : Event
    {
        public S11000863()
        {
            this.EventID = 11000863;
        }

        public override void OnEvent(ActorPC pc)
        {
            /*
            if (_2b36 && !_Xb42)
            {
                Say(pc, 131, "迪澳曼特說的沒錯，$R;" +
                    "唐卡的警備可差勁了$R;" +
                    "$R所以我也可以很輕鬆地把$R;" +
                    "『渦輪引擎』偷到手裡$R;" +
                    "$R靠這個我是不是也能出頭了…？$R;" +
                    "$P哎呀！$R;" +
                    "您…您是誰阿？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "只是路過的", "把『渦輪引擎』交出來吧？"))
                {
                    case 1:
                        Say(pc, 131, "到那兒去呀！$R;");
                        break;
                    case 2:
                        Say(pc, 131, "啊！這麼快就被發現了$R;" +
                            "$R對…對不起$R;" +
                            "我只是受人所託$R;" +
                            "不…不是我的錯呀$R;" +
                            "$R把東西還給您$R;" +
                            "饒我一次吧…$R;");
                        渦輪(pc);
                        break;
                }
                return;
            }

            if (_6a69)
            {
                Say(pc, 131, "唉…就這麼回去會被$R;" +
                    "迪澳曼特罵死的$R;" +
                    "$R這…這怎麼辦呢…$R;");
                return;
            }

            if (CountItem(pc, 10020002) > 0)
            {
                Say(pc, 131, "$R現在已經$R;" +
                    "跟我沒關係了…$R;");
                return;
            }

            if (_6a74)
            {
                Say(pc, 131, "證…證據飛到西邊了$R;" +
                    "$R之…之後我就不知道了$R;");
                return;
            }

            if (_6a73)
            {
                Say(pc, 131, "呵！$R;" +
                    "您…您是誰？$R;");
                switch (Select(pc, "怎麼辦呢？", "", "只是路過的？", "拿出證據來呀？"))
                {
                    case 1:
                        Say(pc, 131, "到那兒去呀！$R;");
                        break;

                    case 2:
                        _6a74 = true;
                        Say(pc, 131, "哈哈哈$R;" +
                            "您來遲一步了$R;" +
                            "$P證…證據已經扔掉了$R;" +
                            "$P已經沒有了$R;" +
                            "$P太可惜了$R;" +
                            "$R哈哈哈$R不要用那種嚇人的目光看著我呀$R;" +
                            "$P我…我的任務就是$R;" +
                            "在…在這裡扔掉文件$R;" +
                            "$R之…之後我就不知道了$R;" +
                            "$P如果想找就找吧$R;");
                        break;
                }
            }
            */
            Say(pc, 131, "看什么看呀?!$R;" +
                "去去去,一边去!$R;");
        }

        void 渦輪(ActorPC pc)
        {
            switch (Select(pc, "怎么办呢？", "", "饶你一次", "不行！你得有心理准备！"))
            {
                case 1:
                    Say(pc, 131, "哎呀…$R;" +
                        "太感谢了$R;" +
                        "$R现在就把东西还给您$R;");
                    if (CheckInventory(pc, 10027750, 1))
                    {
                        Say(pc, 131, "“涡轮引擎”很沉啊$R;" +
                            "你要拿走吗？$R;");
                        switch (Select(pc, "拿不拿？", "", "先不拿了", "空间足够！"))
                        {
                            case 1:
                                Say(pc, 131, "那..那么！不得不还给你了呀…$R;");
                                break;
                            case 2:
                                //_Xb42 = true;
                                GiveItem(pc, 10027750, 1);
                                PlaySound(pc, 2040, false, 100, 50);
                                Say(pc, 131, "得到“涡轮引擎”$R;");
                                break;
                        }
                        return;
                    }
                    PlaySound(pc, 2041, false, 100, 50);
                    Say(pc, 131, "东西太多了，不能接受呀$R;");
                    break;
                case 2:
                    Say(pc, 131, "饶了我吧…$R;" +
                        "$R不要好吗…$R;" +
                        "$R我把东西还给您，还不行吗$R;" +
                        "饶了我吧…啊？$R;");
                    渦輪(pc);
                    break;
            }
        }
    }
}
