using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:奧克魯尼亞南部平原(10031000) NPC基本信息:尼庫爾勒(11000097) X:120 Y:30
namespace SagaScript.M10031000
{
    public class S11000097 : Event
    {
        public S11000097()
        {
            this.EventID = 11000097;

            this.questTransportSource = "請您幫我轉交給對方,$R;" +
                "那就拜託了!;";
            this.questTransportDest = "收好了，真的謝謝您阿;";
            this.questTransportCompleteSrc = "這麼快就將物品轉交給對方了?$R;" +
                "非常謝謝阿!$R;" +
                "$R請去任務服務台領取報酬吧！;";
            this.questTransportCompleteDest = "辛苦了;";
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_02> Neko_02_amask = pc.AMask["Neko_02"];
            BitMask<Neko_02> Neko_02_cmask = pc.CMask["Neko_02"];

            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.聽取建議) &&
                !Neko_02_cmask.Test(Neko_02.獲知原始的事情))
            {
                Say(pc, 131, "$R$P塔依開始自然自語了?$P憑依者也沒有呢?$R;" +
                    "$P這樣不可能的事情…$R;" +
                    "$R也許問一下除了活動木偶以外$R擁有其他力量的人可能會比較好$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.開始維修) &&
                !Neko_02_cmask.Test(Neko_02.聽取建議))
            {
                Neko_02_cmask.SetValue(Neko_02.聽取建議, true);
                Say(pc, 131, "喂，塔依的維修進行的好嗎?$R;" +
                    "$R什麽?$P塔依開始自然自語了?$P憑依者也沒有呢?$R;" +
                    "$R不是跟石像活動木偶做成錯覺吧?$R;" +
                    "$P嗯…難道那是真的?$R;" +
                    "$R從來沒聽説過那樣的事啊…$R;" +
                    "$R嗯…$R我不是活動木偶的專家$R所以不是全部都知道$R但是…$R;" +
                    "$P嗯…$R想聽聽看活動木偶專家的意見阿$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
                Neko_02_cmask.Test(Neko_02.得知維修方法) && 
                !Neko_02_cmask.Test(Neko_02.開始維修))
            {
                Say(pc, 131, "準備一下塔依的材料吧$R還有四種類的強化結晶也要…$R;" +
                    "$R強化結晶會引起神秘的作用$R安裝部件會自己開始恢復$R;");
                return;
            }
            if (!Neko_02_amask.Test(Neko_02.藍任務結束) &&
            Neko_02_cmask.Test(Neko_02.與裁縫阿姨第一次對話) && 
            !Neko_02_cmask.Test(Neko_02.得知維修方法))
            {
                Neko_02_cmask.SetValue(Neko_02.得知維修方法, true);
                Say(pc, 131, "活動木偶需要維修$R;" +
                    "$P想要維修壞掉的塔依?$R;" +
                    "$R維修活動木偶很簡單的$R但是你能做到嗎?$R;" +
                    "$P嗯，好的!$R;" +
                    "$P因爲你是我的朋友$R所以會特別教你的$R;" +
                    "$R不可以教給別人阿!$R;" +
                    "$P首先要準備活動木偶塔依的材料$R;" +
                    "$P『洋鐵的主幹』和『洋鐵的心』$R;" +
                    "$P用那個替換碎部件$R;" +
                    "$R雖然安裝部件是困難的$R但是不要太在意$R;" +
                    "$P然後是唐卡秘傳的維修方法!$R;" +
                    "$P4種類的強化結晶全部需要$R;" +
                    "$R『生命的結晶』$R『力量的結晶』$R『魔力的結晶』還有最後是$R『會心一擊的結晶』$R;" +
                    "$P使用強化結晶的話$R結晶4個會引起神秘的作用$R安裝部件會自己開始恢復$R;");
                return;
            }
            int a = Global.Random.Next(1, 2);
            if (a == 1)
            {
                Say(pc, 131, "媽媽在故鄉擔心著呢…$R;" +
                    "好久沒去故鄉了，要不要回去呢?$R;");
                return;
            }
            Say(pc, 131, "聽説火焰精靈就藏在這附近$R;" +
                "找一找看怎麽樣?$R;");
        }
    }
}
