using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30113001
{
    public class S11000590 : Event
    {
        public S11000590()
        {
            this.EventID = 11000590;
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<Job2X_12> Job2X_12_mask = pc.CMask["Job2X_12"];

            if (Job2X_12_mask.Test(Job2X_12.索取拜金使的紋章))
            {
                Say(pc, 131, "收下『贸易家的纹章』了!$R;" +"$R祝贺啊!!$R;");
                return;
            }

            if (!Job2X_12_mask.Test(Job2X_12.轉職開始))//_3A63)
            {
                return;
            }

            if (Job2X_12_mask.Test(Job2X_12.收集花束) && !Job2X_12_mask.Test(Job2X_12.給予花束))//_3A73 && !_3A78)
            {
                Say(pc, 11000590, 131, "古鲁杜先生…这样的话就困难了!$R现在正是做事的时候啊…$R突然约吃饭的话…$R;");
                Say(pc, 11000590, 131, "啊!来客人了!$R;" +"$R欢…欢迎光临!$R欢迎来到行会宫殿~~!!$R;");
                Say(pc, 11000585, 131, "…?$R…这…真是!四个家伙啊!$R来…来的也真快…怎么搞的?!$R;" +"没办法$R;");

                if (CountItem(pc, 10005400) >= 10)
                {
                    TakeItem(pc, 10005400, 10);
                    Job2X_12_mask.SetValue(Job2X_12.收集結束, true);
                    Job2X_12_mask.SetValue(Job2X_12.給予花束, true);
                    Job2X_12_mask.SetValue(Job2X_12.索取拜金使的紋章, true);
                    Job2X_12_mask.SetValue(Job2X_12.收集花束, false);
                    //_3A64 = true;
                    //_3A78 = true;
                    //_3A74 = true;
                    //_3A73 = false;
                    Say(pc, 11000585, 131, "嗯…「花束」10束$R的确拿过来了$R;" +
                        "$R按照客人的订单$R确实的把商品送到$R对我们商人来说是最重要的事情$R;" +
                        "$P…什么?不是的…$R「花束」不是礼物$R啊，真是…都说不是了$R;" +
                        "$R是吧?小姐$R;");
                    Say(pc, 11000590, 131, "是的…真的不是的$R是因为我想要「花束」$R所以拜托的$R;" +
                        "$P事实是…$R我妈妈因为生病还在躺着呢…$R;" +
                        "$R要消除压力，用鲜花的精华是最好的$R古鲁杜先生教的$R;" +
                        "$P对古鲁杜先生一直都抱有感谢之心$R;" +
                        "$R但是…招待吃饭…有点困难…$R;");
                    Say(pc, 11000585, 131, "知道了?$R商人给客户转交的不仅仅是东西$R;" +
                        "$R其他需要什么，自己找吧$R;" +
                        "$R还好承受住了贸易家的苦练…$R辛苦了$R;" +
                        "$R虽然还有很多不足$R但还是认定你为『贸易家』$R;" +
                        "$P到阿克罗波利斯上城$R商人行会总管那里$R拿『贸易家的纹章』吧$R;");
                    return;
                }
                Say(pc, 11000585, 131, "!!!$R是「花束」10束啊!!$R重新再来吧!$R;");
                return;
            }

            Say(pc, 11000585, 131, "请在阿克罗波利斯上城的$R商人行会总管$R拿『贸易家的纹章』吧$R;");
        }
    }
}
