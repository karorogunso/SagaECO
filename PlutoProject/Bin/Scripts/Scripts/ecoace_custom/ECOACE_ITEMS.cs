using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Network.Client;
using SagaMap.Scripting;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class ACEITEMS : SagaMap.Scripting.Item
    {
        public ACEITEMS()
        {
            Init(90000386, delegate(ActorPC pc)
            {
                pc.CEXP += 5000;
                TakeItem(pc, 16014700, 1);
                ShowEffect(pc, 4131);
                PlaySound(pc, 2162, false, 100, 50);
            });
            Init(90000388, delegate(ActorPC pc)
            {
                pc.JEXP += 5000;
                TakeItem(pc, 16014900, 1);
                ShowEffect(pc, 4131);
                PlaySound(pc, 2162, false, 100, 50);
            });
            Init(82201017, delegate(ActorPC pc)
            {
                GiveItem(pc, 16000300, 10);
                TakeItem(pc, 22000103, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "获得了10个「时空之钥E」!", "");//90000212 e
            });
            Init(90000065, delegate(ActorPC pc)
            {
                Say(pc, 0, "不管怎么样, 感谢您查看这份指南", "初心者指南");
                Say(pc, 0, "ECOAce已经开始了它的第一个旅程$R作为开发者,我很兴奋也很担心$R", "初心者指南");
                Say(pc, 0, "这样的情况已经维持了很长时间$R在一些时候F子认为这有些出格了", "初心者指南");
                Say(pc, 0, "但是,还有大家在,我相信我会没问题$R", "初心者指南");
                Say(pc, 0, "带胶布打,萌大奶一", "初心者指南");
                Say(pc, 0, "谢谢你的支持.$R国服没有了,$RECOAce会与你同行$R伴你长在.", "初心者指南");
                Say(pc, 0, "作为看了这份指南的证明,$R会送你一个小礼品$R请您收下吧!", "初心者指南");
                PlaySound(pc, 4012, false, 100, 50);
                Wait(pc, 2000);
                Say(pc, 0, "经验什么的....$R出去外面随便打个怪就生效了. ", "初心者指南");
                TakeItem(pc, 10020105, 1);
                GiveItem(pc, 16014700, 10);//90000386 exp+
                GiveItem(pc, 16014900, 10);//90000388 job+
            });
            //82201011
            Init(82201011, delegate(ActorPC pc)
            {
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "打开了新手礼包, 出现了道具.", "新手礼包");
                GiveItem(pc, 16014700, 1);//90000386 exp+
                GiveItem(pc, 16014900, 1);//90000388 job+
                //GiveItem(pc, 22000100, 99);//22001000
                TakeItem(pc, 22000104, 1);
                Wait(pc, 2000);
                PlaySound(pc, 4012, false, 100, 50);
                Say(pc, 0, "经验什么的....$R出去外面随便打个怪就生效了. ", "新手礼包");
            });
            //16005602 90000264
            Init(90000264, delegate(ActorPC pc)
            {
                Say(pc, 0, "唔. 现在可以通过这个$R查看和增加声望$R.......$R$R需要查看并增加声望吗?" + pc.Fame, "测试3");
                switch (Select(pc, "要查看声望吗?", "", "要!", "不要"))
                {
                    case 1:
                        {
                            //pc.Fame += uint.Parse(InputBox(pc, "增加多少声望?", InputType.ItemCode));
                            pc.Fame += 10;
                            Say(pc, 0, "声望提高了10点...$R现在的声望值是:" + pc.Fame, "测试3");
                            ShowEffect(pc, 4131);
                            PlaySound(pc, 2162, false, 100, 50);
                            TakeItem(pc, 16005602, 1);
                            Say(pc, 0, "呵呵......还可以这样啊....", "众人");
                            break;
                        }
                    case 2:
                        {
                            Say(pc, 0, "....那就算了", "测试3");
                            return;
                        }
                }
                PlaySound(pc, 2040, false, 100, 50);
            });
        }
    }
}