
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S60000128 : Event
    {
        public S60000128()
        {
            this.EventID = 60000128;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "亲爱的，我就在你身边呀。$R$R你都看不见我吗？摸不着我吗？听不见我的声音吗……？", "依兰");
            Wait(pc, 300);
            Select(pc, " ", "", "那个，你……");
            Say(pc, 0, "……", "依兰");
            Wait(pc, 600);
            Say(pc, 0, "那个……$R$R你..能看见我？", "依兰");
            Wait(pc, 300);
            Select(pc, " ", "", "……好吓人哦");
            Say(pc, 0, "是的。。$R我已经死了，在『$CW东部地牢$CD』儌乕僗僌奟壥暔偺怷$R$R僂僥僫屛墘廗応搶傾僋儘僯傾奀娸墘廗応。", "依兰");
            Say(pc, 0, "可是为￥僠儏乕僩儕傾儖儅僢僾$r僟僂儞僞僂儞,", "依兰");
            Select(pc, " ", "", "好的……");
            Say(pc, 0, "孯娡搰塱墦傊偺杒尷", "依兰");
            Wait(pc, 2000);
            Say(pc, 0, "对不起！乱码是我故意放的！$R$R因为我实在不知道凄惨的爱情故事该怎么写！！", "羽川柠");
            Say(pc, 0, "如果您有好的灵感，亦或是好的剧本！想要续写这个故事的话！$R请务必联系番茄！！$R$R番茄会为了报答，送你好礼物的！！QAQ", "羽川柠");
        }
    }
}