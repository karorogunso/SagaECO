using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M初心者箱子
{
    public class S90000074 : Event
    {
        public S90000074()
        {
            this.EventID = 90000074;
        }

        public override void OnEvent(ActorPC pc)
        {
            PlaySound(pc, 2040, false, 100, 50);
            Say(pc, 0, "虽然无法给你力量...$R", "女孩子的声音");
            Say(pc, 0, "但这是我留给你的一些心意...$R", "女孩子的声音");
            Say(pc, 0, "希望...可以为你提供一些帮助...$R", "女孩子的声音");
            TakeItem(pc, 10020114, 1);
            GiveItem(pc, 50073500, 1);
            GiveItem(pc, 60108900, 1);
            GiveItem(pc, 50020200, 1);
            GiveItem(pc, 50020100, 1);
            GiveItem(pc, 50054100, 1);
            GiveItem(pc, 60032300, 1);
            Wait(pc, 1000);
            PlaySound(pc, 2040, false, 100, 50);
            Say(pc, 0, "得到了初心者套装", "");
            pc.Gold += 3000;
            GiveItem(pc, 10000103, 100);
            GiveItem(pc, 10000102, 100);
            GiveItem(pc, 10000108, 100);
            GiveItem(pc, 10042801, 1);
            Say(pc, 0, "这些,应该可以在最初起到一些作用.....", "女孩子的声音");
            Say(pc, 0, "加油吧...", "女孩子的声音");
            Say(pc, 0, "声音消失了", "");
            
            //Wait(pc, 1000);
            //PlaySound(pc, 2040, false, 100, 50);
            //pc.Gold += 3000;
            //Say(pc, 0, "得到了3000G", "初心者箱子");
            //Wait(pc, 1000);
            //PlaySound(pc, 2040, false, 100, 50);
            //GiveItem(pc, 60060000, 1);
            //GiveItem(pc, 50001200, 1);
            //GiveItem(pc, 50210300, 1);
            //GiveItem(pc, 50066300, 1);
            //GiveItem(pc, 50054100, 1);
            //GiveItem(pc, 60044200, 1);
            //GiveItem(pc, 50045700, 1);
            //GiveItem(pc, 10020105, 1);
            //GiveItem(pc, 22000104, 1);
            //Say(pc, 0, "得到了初心者套装, 就算是初心者$R也可以装备的武具...", "初心者箱子");
            //Wait(pc, 1000);
            //PlaySound(pc, 2040, false, 100, 50);
            //Say(pc, 0, "得到了「阿克罗波利斯通行证」$R$R妈妈再也不用担心我进不去上城了!!", "初心者箱子");
            //GiveItem(pc, 10042801, 1);
            //TakeItem(pc, 10020114, 1);
        }
    }
}