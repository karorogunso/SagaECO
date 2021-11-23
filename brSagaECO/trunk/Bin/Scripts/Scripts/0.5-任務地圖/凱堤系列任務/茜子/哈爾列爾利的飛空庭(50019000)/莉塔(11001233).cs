using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50019000
{
    public class S11001233 : Event
    {
        public S11001233()
        {
            this.EventID = 11001233;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_05> Neko_05_amask = pc.AMask["Neko_05"];
            BitMask<Neko_05> Neko_05_cmask = pc.CMask["Neko_05"];

            //MUSIC 1081 0 0 100
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.飛空庭完成) &&
                !Neko_05_cmask.Test(Neko_05.得到茜子))
            {
                pc.CInt["Neko_05_Map_06"] = CreateMapInstance(50016000, 10062000, 110, 87);
                Warp(pc, (uint)pc.CInt["Neko_05_Map_06"], 8, 4);
                return;
            }
            if (Neko_05_amask.Test(Neko_05.茜子任务开始) &&
                Neko_05_cmask.Test(Neko_05.把電腦唯讀記憶體給哈爾列爾利) &&
                !Neko_05_cmask.Test(Neko_05.飛空庭完成))
            {
                Neko_05_cmask.SetValue(Neko_05.飛空庭完成, true);
                Say(pc, 11001229, 131, "用「客人」給我的『電腦唯讀記憶體』$R終於把我的飛空庭完成了♪$R;" +
                    "$R真是全都多虧「客人」啊！$R;");
                Say(pc, 11001233, 131, "哈爾列爾利……$R;");
                Say(pc, 11001229, 131, "嗯?媽媽?怎麼了?$R;" +
                    "$R不是說有什麼重要的話嗎！？$R;");
                Say(pc, 11001233, 131, "……$R;" +
                    "$P哈爾列爾利…$R;" +
                    "$R你的生命…快要…$R;");
                Say(pc, 11001233, 131, "人工製造的石像…生命非常短暫……$R;" +
                    "$R像我這樣的木偶制作大師$R雖然付出了無數的努力。$R但關於石像壽命，還是沒法克服啊…$R;" +
                    "$P所以不要再製造飛空庭了多陪媽媽吧$R;" +
                    "$R媽媽我會努力的$R讓我們哈爾列爾利多活一天$R;");
                Say(pc, 0, 131, "……！！$R;", "“凱堤(茜)”");
                Say(pc, 11001229, 131, "……$R;" +
                    "$R其實我也感覺到了$R;" +
                    "$R最近媽媽特別擔心我的身體狀況…$R;" +
                    "$R所以我想快點完成我的飛空庭$R;" +
                    "$P媽媽！我的生命還有多久?$R;");
                Say(pc, 11001233, 131, "不知道！我真的不知道！泣……$R;" +
                    "$R沒想到會這麼快…$R只知剩下的時間沒多久…$R;" +
                    "$P真的對不起！哈爾列爾利！$R媽媽對不起你！！$R;");
                Say(pc, 11001229, 131, "媽媽不是的…$R我可以來到這個世界已經覺得很幸福了$R;" +
                    "$R真的謝謝您把我造出來$R媽媽，謝謝！$R;");
                Say(pc, 11001233, 131, "哈爾列爾利…$R;" +
                    "$R我覺得…哈爾列爾利…$R還有茜也很可憐…$R;");
                Say(pc, 11001229, 131, "……$R;" +
                    "$P現在開始，我可以坐著飛空庭$R到世界每個角落去冒險$R這將會是漫長的旅程吧♪$R;");
                Say(pc, 0, 131, "什麼…！?$R;", "凱堤(全體)");
                Say(pc, 0, 131, "等…等一下！！$R;" +
                    "$R不要任意妄為！！$R;" +
                    "$P可是…真的沒辦法啊$R;" +
                    "$R如果哈爾列爾利一定要去的話$R那只能一起去啊…$R;", "“凱堤(茜)”");
                Say(pc, 11001229, 131, "不…我會自己去的$R;");
                Say(pc, 0, 131, "什麼…?$R;", "“凱堤(茜)”");
                Say(pc, 11001229, 131, "可能會是……很長…很長的旅行喔$R;" +
                    "$R我想去某個地方的$R「通往天空的塔」那裡$R;" +
                    "$P想去與僅存的機械時代的科學和塔$R連接在一起的塔妮亞世界那裡$R;" +
                    "$R也許會有…延長我們活動木偶石像$R短暫生命的方法啊$R;" +
                    "$P那茜冰呢…$R;" +
                    "$R跟「客人」在一起…$R還有跟其他姊妹在一起比較好$R;");
                Say(pc, 0, 131, "……$R;" +
                    "$R為什麼…?$R哈爾列爾利…到底……為什麼?$R;", "“凱堤(茜)”");
                Say(pc, 11001229, 131, "啊啊！茜冰！茜冰！$R;" +
                    "$R別哭…雖然我…不能流淚$R;" +
                    "$R可是要和茜冰分開，也很傷心的$R;" +
                    "$P「客人」…$R;" +
                    "$R要好好照顧茜啊！拜託了！$R;" +
                    "$P媽媽！還有「客人」！$R;" +
                    "$R真的！很感謝！$R;" +
                    "$R茜冰…要健康啊！$R;");
                Say(pc, 0, 131, "嗚嗚！哈爾列爾利！我會等你的…$R;" +
                    "$R哈爾列爾利…！！！$R;", "“凱堤(茜)”");
                Wait(pc, 1000);
                ShowEffect(pc, 0, 4023);
                ShowEffect(pc, 11001233, 4023);
                Wait(pc, 1666);
                pc.CInt["Neko_05_Map_06"] = CreateMapInstance(50016000, 10062000, 110, 87);
                Warp(pc, (uint)pc.CInt["Neko_05_Map_06"], 8, 4);
                //EVENTMAP_IN 16 1 8 4 2
                //EVENTEND
                return;
            }
            Say(pc, 11001229, 131, "我的飛空庭完成了♪$R;");
            //EVENTEND
        }
    }
}