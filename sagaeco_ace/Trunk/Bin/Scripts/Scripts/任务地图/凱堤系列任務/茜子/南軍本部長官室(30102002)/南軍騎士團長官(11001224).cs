using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30102002
{
    public class S11001224 : Event
    {
        public S11001224()
        {
            this.EventID = 11001224;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.向瑪莎詢問飛空庭引擎的相關情報) &&
                !Neko_05_cmask.Test(Neko_05.理解记忆体的出处))
            {
                飛空庭(pc);
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.向下城的優秀阿姨詢問瑪莎的蹤跡) &&
                !Neko_05_cmask.Test(Neko_05.向下城的優秀阿姨詢問瑪莎的蹤跡))
            {
                Neko_05_cmask.SetValue(Neko_05.向下城的優秀阿姨詢問瑪莎的蹤跡, true);
                Say(pc, 11001222, 131, "(谁来…救救我…)$R;");
                Say(pc, 11001224, 131, "也就是说…巨型机械G3式之后$R把主要武器变换成滑翔炮形式吧！$R;" +
                    "$R所以巨型机械G3式之后使用的是$R炮弹的弹道$R而不是使用安全的有翼弹$R这就是问题所在！$R;" +
                    "$P根据最新的研究$R配置G3式的压制对象$R就是装甲$R;" +
                    "不是硬体目标的见解有优势吧$R所以最近按照强压制能力的需求$R;" +
                    "具有大量炸药的大型有翼弹$R比高速和贯通力强的炮弹更……$R;");
                Say(pc, 11001222, 131, "啊啊啊！！$R;" +
                    pc.Name + "♪$R;" +
                    "$R发生什么事啊！?跟我有关?$R;" +
                    "是吧?是那样的吧?$R;" +
                    "$P虽然不知道是什么事$R但是请相信我吧♪$R;" +
                    "$R快来♪趁现在出发吧！$R;");
                Say(pc, 11001224, 131, "什么?…$R好久没这么有趣了呢…$R;");
                Say(pc, 11001222, 131, "对不起♪那下次我再来！$R;" +
                    "$R您说的很有趣，谢谢您♪$R;" +
                    "$P来来！快走吧♪快！！$R;");
                Say(pc, 0, 131, "这个人好像跟我不太合啊！$R;", "货物中的哈利路亚");
                Say(pc, 11001224, 131, "噢呵?$R活动木偶石像??$R;" +
                    "$R原来你喜欢机械！$R一定是的！！不会有错！！$R;" +
                    "$P来跟我一起讨论关于机器狐狸系列$R的悬挂装置构造…大概3小时吧$R;");
                Say(pc, 0, 131, "到此为止吧！！$R;", "除长官和哈尔列尔利以外，全都");
                飛空庭(pc);
                return;
            }
            Say(pc, 11001224, 131, "也就是说…巨型机械G3式之后$R把主要武器变换成滑翔炮形式吧！$R;" +
                "$R所以巨型机械G3式之后使用的是$R炮弹的弹道$R而不是使用安全的有翼弹$R这就是问题所在！$R;" +
                "$P根据最新的研究$R配置G3式的压制对象$R就是装甲$R;" +
                "不是硬体目标的见解有优势吧$R所以最近按照强压制能力的需求$R;" +
                "具有大量炸药的大型有翼弹$R比高速和贯通力强的炮弹更……$R;");
            Say(pc, 11001222, 131, "(谁来…救救我…)$R;");
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.进入光塔) &&
                !Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利) &&
                CountItem(pc, 10057900) == 0)
            {
                Say(pc, 0, 131, "啊啊…玛莎又给长官抓住不放了啊$R;" +
                    "$R快来…$R赶紧从光之塔内找到『电脑只读记忆体』后$R就回唐卡吧～♪$R;", "货物中的哈利路亚");
                return;
            }
        }

        void 飛空庭(ActorPC pc)
        {
            switch (Select(pc, "去玛莎的飞空庭吗?", "", "去", "不去"))
            {
                case 1:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        Say(pc, 11001222, 131, "啊…谁在凭依啊?$R;" +
                            "$R淑女的庭院是不可以带陌生人来的$R;");
                        return;
                    }
                    pc.CInt["Neko_05_Map_03"] = CreateMapInstance(50014000, 30032000, 3, 3);
                    Warp(pc, (uint)pc.CInt["Neko_05_Map_03"], 7, 12);
                    //EVENTMAP_IN 14 1 7 10 0
                    /*
        if(//ME.WORK0 = -1
        )
        {
                    Say(pc, 11001222, 131, "啊……!!$R飛空庭好像被市裡的航空管制扣住了$R;" +
                        "$R對不起!!請稍後再跟我說吧$R;");
            return;
        }//*/
                    break;
                case 2:
                    Say(pc, 11001222, 131, "(啊啊…快…走吧！！)$R;");
                    break;
            }
        }
    }
}