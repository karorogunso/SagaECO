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
                Say(pc, 11001222, 131, "(誰來…救救我…)$R;");
                Say(pc, 11001224, 131, "也就是說…機幹特G3式之後$R把主要武器變換成滑翔炮形式吧！$R;" +
                    "$R所以機幹特G3式之後使用的是$R砲彈的彈道$R而不是使用安全的有翼彈$R這就是問題所在！$R;" +
                    "$P根據最新的研究$R配置G3式的壓制對象$R就是裝甲$R;" +
                    "不是硬體目標的見解有優勢吧$R所以最近按照強壓制能力的需求$R;" +
                    "具有大量炸藥的大型有翼彈$R比高速和貫通力强的炮彈更……$R;");
                Say(pc, 11001222, 131, "啊啊啊！！$R;" +
                    pc.Name + "♪$R;" +
                    "$R發生什麼事啊！?跟我有關?$R;" +
                    "是吧?是那樣的吧?$R;" +
                    "$P雖然不知道是什麼事$R但是請相信我吧♪$R;" +
                    "$R快來♪趁現在出發吧！$R;");
                Say(pc, 11001224, 131, "什麼?…$R好久沒這麼有趣了呢…$R;");
                Say(pc, 11001222, 131, "對不起♪那下次我再來！$R;" +
                    "$R您說的很有趣，謝謝您♪$R;" +
                    "$P來來！快走吧♪快！！$R;");
                Say(pc, 0, 131, "行李裡的哈爾列爾利$R;" +
                    "這個人好像跟我不太合阿！$R;", "\"貨物中的哈爾列爾利\"");
                Say(pc, 11001224, 131, "噢呵?$R活動木偶石像??$R;" +
                    "$R原來你喜歡機械！$R一定是的！！不會有錯！！$R;" +
                    "$P來跟我一起討論關於機器狐狸系列$R的懸掛裝置構造…大概3小時吧$R;");
                Say(pc, 0, 131, "到此為止吧！！$R;", "除長官和哈爾列爾利以外，全都");
                飛空庭(pc);
                return;
            }
            Say(pc, 11001224, 131, "也就是說…機幹特G3式之後$R把主要武器變換成滑翔炮形式吧！$R;" +
                "$R所以機幹特G3式之後使用的是$R砲彈的彈道$R而不是使用安全的有翼彈$R這就是問題所在！$R;" +
                "$P根據最新的研究$R配置G3式的壓制對象$R就是裝甲$R;" +
                "不是硬體目標的見解有優勢吧$R所以最近按照強壓制能力的需求$R;" +
                "具有大量炸藥的大型有翼彈$R比高速和貫通力强的炮彈更……$R;");
            Say(pc, 11001222, 131, "(誰來…救救我…)$R;");
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.进入光塔) &&
                !Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利) &&
                CountItem(pc, 10057900) == 0)
            {
                Say(pc, 0, 131, "啊啊…瑪莎又給長官抓住不放了啊$R;" +
                    "$R快來…$R趕緊從光之塔內找到『電腦唯讀記憶體』後$R就回唐卡吧～♪$R;", "\"貨物中的哈爾列爾利\"");
                return;
            }
        }

        void 飛空庭(ActorPC pc)
        {
            switch (Select(pc, "去瑪莎的飛空庭嗎?", "", "去", "不去"))
            {
                case 1:
                    if (pc.PossesionedActors.Count != 0)
                    {
                        Say(pc, 11001222, 131, "啊…誰在憑依啊?$R;" +
                            "$R淑女的庭院是不可以帶陌生人來的$R;");
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