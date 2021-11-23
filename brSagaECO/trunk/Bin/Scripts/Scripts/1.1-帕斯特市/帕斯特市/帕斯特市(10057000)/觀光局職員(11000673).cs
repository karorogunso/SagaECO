using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10057000
{
    public class S11000673 : Event
    {
        public S11000673()
        {
            this.EventID = 11000673;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<PSTFlags> mask = pc.CMask["PST"];
            Say(pc, 131, "歡迎光臨帕斯特市!$R;");
            if (!mask.Test(PSTFlags.觀光局職員第一次對話))//_4a86)
            {
                mask.SetValue(PSTFlags.觀光局職員第一次對話, true);
                //_4a86 = true;
                Say(pc, 131, "簡單介紹一下這個地方！$R;" +
                    "$P這裡是帕斯特島正中央的地方$R;" +
                    "$R無論是去哪個地方都很方便$R;" +
                    "以這裡作為基地，進行冒險的話$R;" +
                    "會更方便的$R;" +
                    "$P南邊有住宅和學校的$R;" +
                    "是帕斯特國民居住地$R;" +
                    "$R北邊有綠色防備軍的本部和倉庫$R;" +
                    "還有可以通往商業區的路$R;" +
                    "$P此外還有牧場以及果園等$R;" +
                    "所以是個散步的好地方啊！$R;");
                return;
            }
            switch (Select(pc, "要給您帶路嗎？", "", "不用了", "管理局", "農夫行會總部", "綠色防備軍本部", "商店區", "學校", "城市整體説明"))
            {
                case 2:
                    Say(pc, 131, "帕斯特管理局是$R;" +
                        "辦理各種文書手續的地方$R;" +
                        "$R並給年輕的冒險家$R;" +
                        "委託簡單的事情$R;");
                    Navigate(pc, 174, 84);
                    break;
                case 3:
                    Say(pc, 131, "這裡是農夫行會總部吧$R;" +
                        "不僅是農夫行會$R;" +
                        "$R也有生產系行會分會$R;" +
                        "好好利用吧！$R;");
                    Navigate(pc, 174, 100);
                    break;
                case 4:
                    Say(pc, 131, "綠色防備軍的本部就是那個建築$R;" +
                        "$R綠色防備軍如其名$R;" +
                        "是負責這個國家防禦和$R;" +
                        "城市安全的國家！$R;" +
                        "$P就連喜歡小狗的人聚在一起$R;" +
                        "他們也管的！$R;");
                    Navigate(pc, 28, 100);
                    break;
                case 5:
                    Say(pc, 131, "這裡是咖啡館和武器商店的集中地$R;" +
                        "非常繁華$R;" +
                        "帶您到商店區的入口吧$R;" +
                        "請隨著箭頭方向走$R;");
                    Navigate(pc, 114, 103);
                    break;
                case 6:
                    Say(pc, 131, "掛著大鐘的建築物就是學校$R;" +
                        "誰都可以上學的$R;" +
                        "去看看吧！$R;");
                    Navigate(pc, 147, 119);
                    break;
                case 7:
                    Say(pc, 131, "簡單介紹一下這個地方！$R;" +
                        "$P這裡是帕斯特島正中央的地方$R;" +
                        "$R無論是去哪個地方都很方便$R;" +
                        "以這裡作為基地，進行冒險的話$R;" +
                        "會更方便的$R;" +
                        "$P南邊有住宅和學校的$R;" +
                        "是帕斯特國民居住地$R;" +
                        "$R北邊有綠色防備軍的本部和倉庫$R;" +
                        "還有可以通往商業區的路$R;" +
                        "$P此外還有牧場以及果園等$R;" +
                        "所以是個散步的好地方啊！$R;");
                    break;
            }
        }
    }
}