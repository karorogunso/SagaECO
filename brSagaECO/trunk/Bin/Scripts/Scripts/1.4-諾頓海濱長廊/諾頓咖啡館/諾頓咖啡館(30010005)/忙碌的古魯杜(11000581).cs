using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010005
{
    public class S11000581 : Event
    {
        public S11000581()
        {
            this.EventID = 11000581;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.搜集紋章紙) && !Job2X_12_mask.Test(Job2X_12.給予紋章紙))//_3A69 && !_3A74)
            {
                Say(pc, 11000581, 131, "看一下…$R煉油10桶…$R;" +
                    "$R麵包5個$R械糖汁3個$R還有…還有…$R;");
                Say(pc, 11000586, 131, "那…古魯杜先生?$R後面好像來了客人$R;");
                Say(pc, 11000581, 131, "哦…來了…$R你要訂購什麽?$R不好意思能等一下嗎?$R因爲還在整理中所以無法離手$R;");

                if (CountItem(pc, 10024900) >= 10)
                {
                    TakeItem(pc, 10024900, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予紋章紙, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集解毒果, true);
                    Job2X_12_mask.SetValue(Job2X_12.搜集紋章紙, false);
                    //_3A74 = true;
                    //_3A70 = true;
                    //_3A69 = false;
                    Say(pc, 11000586, 131, "所以古魯杜先生$R不用那樣從計量到調查$R一一都仔細地做…$R那些我也可以自己做啊$R;");
                    Say(pc, 11000581, 131, "呵!交給你的話，只是浪費時間…$R;" +
                        "$R…什麽?不是客人?$R是我們行會的成員吧$R;" +
                        "$P什麽?大商人的磨練?$R;" +
                        "$R行會大師那家伙又把我攪和在一起$R;");
                    Say(pc, 11000581, 131, "知道了!知道了啊!$R「紋章紙」10張確實收到了$R;" +
                        "$R是諾頓騎士團一直想要的東西$R我會幫你轉交的$R;" +
                        "$R反正都要做事$R要不也來幫我的忙吧?$R;" +
                        "$P幫我給伊戈路的伊頑轉交$R10個「解毒果實」吧$R;" +
                        "$P最近小惡魔在搗亂好像很頭痛啊$R;" +
                        "$P不是的…嘮叨就到此爲止!$R我也會過去的$R;" +
                        "$R那就拜託了!$R;");
                    return;
                }
                return;
            }
            Say(pc, 11000581, 131, "給伊戈路的伊頑轉交$R10個「解毒果實」吧$R;" +
                "$R那拜託您了$R;");
        }
    }
}
