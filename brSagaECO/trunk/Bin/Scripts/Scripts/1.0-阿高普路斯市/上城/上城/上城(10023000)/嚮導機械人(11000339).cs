using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaLib;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
//所在地圖:上城(10023000) NPC基本信息:嚮導機械人(11000339) X:132 Y:95
namespace SagaScript.M10023000
{
    public class S11000339 : Event
    {
        public S11000339()
        {
            this.EventID = 11000339;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Neko_04> Neko_04_amask = pc.AMask["Neko_04"];
            BitMask<Neko_04> Neko_04_cmask = pc.CMask["Neko_04"];
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被告知去找機器人) &&
                Neko_04_cmask.Test(Neko_04.被告知犯人是小孩) &&
                !Neko_04_cmask.Test(Neko_04.被告知未見過小孩) &&
                pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Neko_04_cmask.SetValue(Neko_04.被告知未見過小孩, true);
                    Say(pc, 11000339, 131, "小孩嗎?$R;" +
                        "$R…不!沒見過啊$R;" +
                        "$P在我記憶中，出入行會宮殿的人裡$R沒有小孩喔!$R;");
                    Say(pc, 0, 131, "…那麼，那孩子$R還在這個建築物裡嗎?$R;" +
                        "$R明明在這裡某處的!$R;", "\"凱堤（山吹）\"");
                    Say(pc, 0, 131, "好像是喔!$R;" +
                        "$R這建築物…行會…哎！是什麼呢？$R;" +
                        "除了主人的工作室還有很多房間呢！$R;" +
                        "$R有人要把那壞孩子藏起來嗎?$R;" +
                        "$R好！$R不管怎麼樣!$R仔細的去每個房間找找看吧$R;", "\"凱堤（桃）\"");
                    Say(pc, 0, 131, "桃子!幹麼那麼開心啊$R主人不是因為寄存的東西被偷了$R還在煩嘛!$R;" +
                        "$R認真行動吧!$R;", "\"凱堤（山吹）\"");
                    Say(pc, 0, 131, "啊!對不起!$R;" +
                        "$R好像很有趣…所以…$R;", "\"凱堤（桃）\"");
                    Say(pc, 0, 131, "有趣？？$R;", "\"凱堤（山吹）\"");
                    Say(pc, 0, 131, "嗯~$R我們不是在跟主人冒險嘛!$R;" +
                        "$R我一直都覺得很幸福^^$R好開心喔$R;", "\"凱堤（桃）\"");
                    Say(pc, 0, 131, "是嗎?$R;", "\"凱堤（山吹）\"");
                    Say(pc, 0, 131, "嗯…?$R緑怎麼了…?$R;", "\"凱堤（桃）\"");
                    return;
                }
            }
            if (Neko_04_amask.Test(Neko_04.任務開始) &&
                !Neko_04_amask.Test(Neko_04.任務結束) &&
                Neko_04_cmask.Test(Neko_04.被告知去找機器人) &&
                !Neko_04_cmask.Test(Neko_04.被告知犯人是小孩) &&
                pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
            {
                if (pc.Inventory.Equipments[EnumEquipSlot.PET].ItemID == 10017900)
                {
                    Say(pc, 11000339, 131, "嗯？$R有沒有見過可疑的人？$R;" +
                        "$P沒有…沒有可疑人!$R;" +
                        "$P我是要給大家帶路，$R;" +
                        "所以經常在這裡出入$R拍照後，會暫時儲存在記憶體內$R;" +
                        "$P最起碼昨天、今天這兩天內$R沒有見過可疑人物喔$R;" +
                        "$R沒錯的!!$R;");
                    Say(pc, 0, 131, "那個…$R是不是應該告訴主人啊?$R;" +
                        "$R我說的是犯人啊$R;", "\"凱堤（山吹）\"");
                    Say(pc, 0, 131, "雖然說是那樣$R可是我們說的…話…$R主人不是聽不懂嘛$R;", "\"凱堤（桃）\"");
                    Say(pc, 0, 131, "那個嘛，是因為桃子對主人的愛$R不夠才那樣的$R;", "\"凱堤（山吹）\"");
                    Say(pc, 0, 131, "是嗎…不是，不是的!$R;" +
                        "$R沒可能的$R;", "\"凱堤（桃）\"");
                    return;
                }
            }
            NavigateCancel(pc);
            /*
            if (!_1A15)
            {
                Say(pc, 11000339, 131, "歡迎來到阿高普路斯$R;" +
                    "我是嚮導機械人$R;");
            }//*/
            Say(pc, 11000339, 131, "這裡是『行會宮殿』$R;" +
                "$R在這裡，您可以轉職各式各樣的$R;" +
                "職業，也可以承接任務$R;");
            Say(pc, 11000338, 131, "要帶您去其他地方嗎?$R;");
            /*
            if (_1A15)
            {
                switch (Select(pc, "“要繼續委託我帶路嗎?", "", "請繼續帶我到其他地方", "放棄"))
                {
                    case 1:
                        break;
                    case 2:
                        Say(pc, 11000339, 131, "真可惜啊$R;");
                        _1A15 = false;
                        return;
                }
            }//*/
            switch (Select(pc, "請選擇想去的地方", "", "白聖堂", "黑聖堂", "裁縫阿姨的家", "寶石商", "放棄"))
            {
                case 1:
                    Say(pc, 11000339, 131, "跟著箭頭方向走$R;" +
                        "它會帶您去『白聖堂』的$R;");
                    Navigate(pc, 160, 130);
                    //_1A15 = true;
                    break;
                case 2:
                    Say(pc, 11000339, 131, "跟著箭頭方向走$R;" +
                        "它會帶您去『黑聖堂』的$R;");
                    Navigate(pc, 95, 130);
                    //_1A15 = true;
                    break;
                case 3:
                    Say(pc, 11000339, 131, "跟著箭頭方向走$R;" +
                        "它會帶您去『裁縫阿姨的家』的$R;");
                    Navigate(pc, 89, 97);
                    //_1A15 = true;
                    break;
                case 4:
                    Say(pc, 11000339, 131, "跟著箭頭方向走$R;" +
                        "它會帶您去『寶石商』的$R;");
                    Navigate(pc, 150, 97);
                    //_1A15 = true;
                    break;
                case 5:
                    Say(pc, 11000339, 131, "真可惜啊$R;");
                    //_1A15 = false;
                    break;
            }
        }
    }
}