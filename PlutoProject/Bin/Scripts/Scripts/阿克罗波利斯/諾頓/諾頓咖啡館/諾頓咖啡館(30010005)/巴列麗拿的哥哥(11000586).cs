using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30010005
{
    public class S11000586 : Event
    {
        public S11000586()
        {
            this.EventID = 11000586;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (Job2X_12_mask.Test(Job2X_12.收集解毒果))//_3A70)
            {
                Say(pc, 131, "对古鲁杜先生一直都有感谢之心$R;");
                return;
            }

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.搜集紋章紙) && !Job2X_12_mask.Test(Job2X_12.給予紋章紙))//_3A69 && !_3A74)
            {
                Say(pc, 11000581, 131, "看一下…$R炼油10桶…$R;" +
                    "$R面包5个$R械糖汁3个$R还有…还有…$R;");
                Say(pc, 11000586, 131, "那…古鲁杜先生?$R后面好像来了客人$R;");
                Say(pc, 11000581, 131, "哦…来了…$R你要订购什么?$R不好意思能等一下吗?$R因为还在整理中所以无法离手$R;");

                if (CountItem(pc, 10024900) >= 10)
                {
                    TakeItem(pc, 10024900, 10);
                    Job2X_12_mask.SetValue(Job2X_12.給予紋章紙, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集解毒果, true);
                    Job2X_12_mask.SetValue(Job2X_12.搜集紋章紙, false);
                    //_3A74 = true;
                    //_3A70 = true;
                    //_3A69 = false;
                    Say(pc, 11000586, 131, "所以古鲁杜先生$R不用那样从计量到调查$R一一都仔细地做…$R那些我也可以自己做啊$R;");
                    Say(pc, 11000581, 131, "呵!交给你的话，只是浪费时间…$R;" +
                        "$R…什么?不是客人?$R是我们行会的成员吧$R;" +
                        "$P什么?大商人的磨练?$R;" +
                        "$R行会大师那家伙又把我搅和在一起$R;");
                    Say(pc, 11000581, 131, "知道了!知道了啊!$R「纹章纸」10张确实收到了$R;" +
                        "$R是诺森骑士团一直想要的东西$R我会帮你转交的$R;" +
                        "$R反正都要做事$R要不也来帮我的忙吧?$R;" +
                        "$P帮我给冰屋的伊顽转交$R10个「解毒果实」吧$R;" +
                        "$P最近小恶魔在捣乱好像很头痛啊$R;" +
                        "$P不是的…唠叨就到此为止!$R我也会过去的$R;" +
                        "$R那就拜托了!$R;");
                    return;
                }
                return;
            }
            Say(pc, 11000581, 131, "给冰屋的伊顽转交$R10个「解毒果实」吧$R;" +
                "$R那拜托您了$R;");
        }
    }
}
