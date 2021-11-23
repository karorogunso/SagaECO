
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000007 : Event
    {
        public S910000007()
        {
            this.EventID = 910000007;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000007) >= 1)
            {
                TakeItem(pc, 910000007, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            List<uint> list = getlist();
            GiveItem(pc, list[SagaLib.Global.Random.Next(0, list.Count - 1)],1);
            GiveItem(pc, list[SagaLib.Global.Random.Next(0, list.Count - 1)], 1);
            GiveItem(pc, list[SagaLib.Global.Random.Next(0, list.Count - 1)], 1);
        }
        List<uint> getlist()
        {
            List<uint> dsM = new List<uint>();
            //暗帝套装
            dsM.Add(50215300);
            dsM.Add(50215301);
            dsM.Add(50215400);
            dsM.Add(50215401);
            dsM.Add(50046800);
            dsM.Add(50046801);
            dsM.Add(50046900);
            dsM.Add(50046901);
            dsM.Add(50095200);
            dsM.Add(50095300);
            dsM.Add(50123000);
            dsM.Add(50123100);
            dsM.Add(50229900);
            dsM.Add(50230000);
            dsM.Add(50214600);
            dsM.Add(50214601);
            dsM.Add(50011242);
            dsM.Add(50011312);
            dsM.Add(50046400);
            dsM.Add(50046401);
            dsM.Add(50046300);
            dsM.Add(50046301);
            dsM.Add(50091300);
            dsM.Add(50091301);
            dsM.Add(50123200);
            dsM.Add(50123300);
            dsM.Add(50230100);
            dsM.Add(50230200);
            dsM.Add(50214700);
            dsM.Add(50214701);
            dsM.Add(50214800);
            dsM.Add(50214801);
            dsM.Add(50011243);
            dsM.Add(50011313);
            dsM.Add(50046600);
            dsM.Add(50046601);
            dsM.Add(50046500);
            dsM.Add(50046501);
            dsM.Add(50091400);
            dsM.Add(50091401);
            dsM.Add(50123400);
            dsM.Add(50123500);
            dsM.Add(50230300);
            dsM.Add(50230400);
            dsM.Add(50214900);
            dsM.Add(50214901);
            dsM.Add(50215000);
            dsM.Add(50215001);
            dsM.Add(50046700);
            dsM.Add(50046701);
            dsM.Add(50091600);
            dsM.Add(50046700);
            dsM.Add(50046701);
            dsM.Add(50099000);
            dsM.Add(50091500);
            dsM.Add(50098900);
            dsM.Add(50123600);
            dsM.Add(50123700);
            dsM.Add(50230500);
            dsM.Add(50215100);
            dsM.Add(50215101);
            dsM.Add(50011244);
            dsM.Add(50011314);
            dsM.Add(50047900);
            dsM.Add(50047901);
            dsM.Add(50114300);
            dsM.Add(50114301);
            dsM.Add(50091700);
            dsM.Add(50091701);
            dsM.Add(50081900);
            dsM.Add(50095400);
            dsM.Add(50123800);
            dsM.Add(50123900);
            dsM.Add(50230600);
            dsM.Add(50230700);
            dsM.Add(50215500);
            dsM.Add(50215501);
            dsM.Add(50215600);
            dsM.Add(50215601);
            dsM.Add(50011245);
            dsM.Add(50011315);
            dsM.Add(50047000);
            dsM.Add(50047001);
            dsM.Add(50114400);
            dsM.Add(50114401);
            dsM.Add(50091800);
            dsM.Add(50091801);
            dsM.Add(50095500);
            dsM.Add(50230800);
            dsM.Add(50230900);
            dsM.Add(50124000);
            dsM.Add(50215700);
            dsM.Add(50215701);
            dsM.Add(50215800);
            dsM.Add(50215801);
            dsM.Add(50047100);
            dsM.Add(50047101);
            dsM.Add(50047200);
            dsM.Add(50047201);
            dsM.Add(50091900);
            dsM.Add(50099100);
            dsM.Add(50011568);
            dsM.Add(50124100);
            dsM.Add(50231000);
            dsM.Add(50231100);
            dsM.Add(50215900);
            dsM.Add(50215901);
            dsM.Add(50216000);
            dsM.Add(50216001);
            dsM.Add(50011246);
            dsM.Add(50011316);
            dsM.Add(50114600);
            dsM.Add(50114601);
            dsM.Add(50114500);
            dsM.Add(50114501);
            dsM.Add(50092000);
            dsM.Add(50092001);
            dsM.Add(50011569);
            dsM.Add(50124200);
            dsM.Add(50124300);
            dsM.Add(50231200);
            dsM.Add(50231300);
            dsM.Add(50216100);
            dsM.Add(50216101);
            dsM.Add(50216200);
            dsM.Add(50216201);
            dsM.Add(50011247);
            dsM.Add(50011317);
            dsM.Add(50047300);
            dsM.Add(50047301);
            dsM.Add(50114700);
            dsM.Add(50114701);
            dsM.Add(50092100);
            dsM.Add(50092101);
            dsM.Add(50048000);
            dsM.Add(50048001);
            dsM.Add(50095600);
            dsM.Add(50124400);
            dsM.Add(50124500);
            dsM.Add(50216300);
            dsM.Add(50216301);
            dsM.Add(50216400);
            dsM.Add(50216401);
            dsM.Add(50047400);
            dsM.Add(50047401);
            dsM.Add(50047500);
            dsM.Add(50047501);
            dsM.Add(50081500);
            dsM.Add(50124600);
            dsM.Add(50124700);
            dsM.Add(50231600);
            dsM.Add(50231700);
            dsM.Add(50216500);
            dsM.Add(50216501);
            dsM.Add(50216600);
            dsM.Add(50216601);
            dsM.Add(50011248);
            dsM.Add(50011318);
            dsM.Add(50114800);
            dsM.Add(50114801);
            dsM.Add(50047600);
            dsM.Add(50047601);
            dsM.Add(50092200);
            dsM.Add(50092201);
            dsM.Add(50092300);
            dsM.Add(50092301);
            dsM.Add(50081600);
            dsM.Add(50081700);
            dsM.Add(50124800);
            dsM.Add(50124900);
            dsM.Add(50125000);
            dsM.Add(50125100);
            dsM.Add(50231800);
            dsM.Add(50231900);
            dsM.Add(50216700);
            dsM.Add(50216701);
            dsM.Add(50011249);
            dsM.Add(50011319);
            dsM.Add(50114900);
            dsM.Add(50114901);
            dsM.Add(50047700);
            dsM.Add(50047701);
            dsM.Add(50081700);
            dsM.Add(50081800);
            dsM.Add(50095800);
            dsM.Add(50095900);
            dsM.Add(50125200);
            dsM.Add(50125300);
            dsM.Add(50232000);
            dsM.Add(50232100);
            dsM.Add(50232200);
            dsM.Add(50216800);
            dsM.Add(50216801);
            dsM.Add(50216900);
            dsM.Add(50216901);
            dsM.Add(50217000);
            dsM.Add(50217001);
            dsM.Add(50047800);
            dsM.Add(50047801);
            dsM.Add(50115000);
            dsM.Add(50115001);
            dsM.Add(50125400);
            dsM.Add(50125500);
            dsM.Add(50232300);
            dsM.Add(50232400);
            dsM.Add(50232500);
            //圣帝套装
            return dsM;
        }
    }
}

