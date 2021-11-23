using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript
{
    public class P10001035 : RandomPortal
    {
        public P10001035()
        {
            EventID = 10001035;
            //Init(10001035, 10035000, 97, 200, 105, 211);
        }

        public override void OnEvent(ActorPC pc)
        {
            BitMask<TravelFGarden> TravelFGarden_mask = pc.CMask["TravelFGarden"];
            if (pc.Account.GMLevel >= 100 || TravelFGarden_mask.Test(TravelFGarden.已經辦理手續))
            {
                switch (Select(pc, "這是往軍艦島的飛空庭喔", "", "搭乘飛空庭", "暫時不用"))
                {
                    case 1:
                        TravelFGarden_mask.SetValue(TravelFGarden.已經辦理手續, false);
                        //_6a57 = false;
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 0, 131, "向軍艦島出發$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 223, 170, 8066);
                        Wait(pc, 2000);
                        Warp(pc, 10035000, 101, 205);
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 0, 131, "請辦理搭乘手續喔$R;");
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:軍艦島(10035000)
    //目標坐標:(97,200) ~ (105,211)

    public class P10001037 : RandomPortal
    {
        public P10001037()
        {
            this.EventID = 10001037;
            //Init(10001037, 20146000, 199, 142, 200, 145);
        }

        public override void OnEvent(ActorPC pc)
        {

            BitMask<TravelFGarden> TravelFGarden_mask = pc.CMask["TravelFGarden"];
            if (pc.Account.GMLevel >= 100 || TravelFGarden_mask.Test(TravelFGarden.已经买票))
            {
                switch (Select(pc, "這是往光之塔的飛空庭喔", "", "搭乘飛空庭", "暫時不用"))
                {
                    case 1:
                        TravelFGarden_mask.SetValue(TravelFGarden.已经买票, false);
                        //_6a57 = false;
                        PlaySound(pc, 2426, false, 100, 50);
                        Say(pc, 0, 131, "向光之塔出發$R;");
                        PlaySound(pc, 2438, false, 100, 50);
                        ShowEffect(pc, 37, 133, 8066);
                        Wait(pc, 2000);
                        Warp(pc, 20146000, 199, 142);
                        break;
                    case 2:
                        break;
                }
                return;
            }
            Say(pc, 0, 131, "請辦理搭乘手續喔$R;");
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:光之塔下層(20146000)
    //目標坐標:(199,142) ~ (200,145)

    public class P10001038 : RandomPortal
    {
        public P10001038()
        {
            Init(10001038, 10060000, 157, 91, 158, 92);
        }
    }
    //原始地圖:摩根傭兵軍團本部(30094000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(157,91) ~ (158,92)

    public class P10001039 : RandomPortal
    {
        public P10001039()
        {
            Init(10001039, 30094000, 6, 10, 6, 10);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根傭兵軍團本部(30094000)
    //目標坐標:(6,10) ~ (6,10)

    public class P10001040 : RandomPortal
    {
        public P10001040()
        {
            Init(10001040, 10060000, 155, 68, 156, 69);
        }
    }
    //原始地圖:戰士系行會分會(30096000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(155,68) ~ (156,69)

    public class P10001041 : RandomPortal
    {
        public P10001041()
        {
            Init(10001041, 30096000, 7, 12, 9, 12);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:戰士系行會分會(30096000)
    //目標坐標:(7,12) ~ (9,12)

    public class P10001042 : RandomPortal
    {
        public P10001042()
        {
            Init(10001042, 10060000, 139, 68, 140, 68);
        }
    }
    //原始地圖:摩根政府大廈(30095000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(139,68) ~ (140,68)

    public class P10001043 : RandomPortal
    {
        public P10001043()
        {
            Init(10001043, 30095000, 8, 12, 8, 12);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根政府大廈(30095000)
    //目標坐標:(8,12) ~ (8,12)

    public class P10001044 : RandomPortal
    {
        public P10001044()
        {
            Init(10001044, 10060000, 109, 97, 109, 97);
        }
    }
    //原始地圖:國營精煉所(30099000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(109,97) ~ (109,97)

    public class P10001045 : RandomPortal
    {
        public P10001045()
        {
            Init(10001045, 30099000, 7, 15, 7, 15);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:國營精煉所(30099000)
    //目標坐標:(7,15) ~ (7,15)

    public class P10001046 : RandomPortal
    {
        public P10001046()
        {
            Init(10001046, 10060000, 142, 97, 142, 97);
        }
    }
    //原始地圖:迪澳曼特私人精煉所(30099001)
    //目標地圖:摩根市(10060000)
    //目標坐標:(142,97) ~ (142,97)

    public class P10001047 : RandomPortal
    {
        public P10001047()
        {
            Init(10001047, 30099001, 7, 15, 7, 15);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:迪澳曼特私人精煉所(30099001)
    //目標坐標:(7,15) ~ (7,15)

    public class P10001048 : RandomPortal
    {
        public P10001048()
        {
            Init(10001048, 10060000, 119, 186, 120, 186);
        }
    }
    //原始地圖:國營精煉所(30099002)
    //目標地圖:摩根市(10060000)
    //目標坐標:(119,186) ~ (120,186)

    public class P10001049 : RandomPortal
    {
        public P10001049()
        {
            Init(10001049, 30099002, 7, 15, 7, 15);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:國營精煉所(30099002)
    //目標坐標:(7,15) ~ (7,15)

    public class P10001050 : RandomPortal
    {
        public P10001050()
        {
            this.EventID = 10001050;
            //Init(10001050, 10060000, 81, 152, 81, 153);
        }

        public override void OnEvent(ActorPC pc)
        {
            NPCMotion(pc, 11000828, 159);
            NPCMotion(pc, 11000829, 159);
            Wait(pc, 1000);
            Say(pc, 0, 131, "謝謝唷$R;", "\"介紹負責人\"");
            Warp(pc, 10060000, 81, 152);
        }
    }
    //原始地圖:商人行會總部(30097000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(81,152) ~ (81,153)

    public class P10001051 : RandomPortal
    {
        public P10001051()
        {
            Init(10001051, 30097000, 8, 12, 8, 12);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:商人行會總部(30097000)
    //目標坐標:(8,12) ~ (8,12)

    public class P10001052 : RandomPortal
    {
        public P10001052()
        {
            Init(10001052, 10060000, 105, 175, 105, 176);
        }
    }
    //原始地圖:碳礦監督廳(30098000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(105,175) ~ (105,176)

    public class P10001053 : RandomPortal
    {
        public P10001053()
        {
            Init(10001053, 30098000, 6, 8, 6, 8);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:碳礦監督廳(30098000)
    //目標坐標:(6,8) ~ (6,8)

    public class P10001054 : RandomPortal
    {
        public P10001054()
        {
            Init(10001054, 10060000, 97, 101, 97, 101);
        }
    }
    //原始地圖:礦工之部屋(30092000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(97,101) ~ (97,101)

    public class P10001055 : RandomPortal
    {
        public P10001055()
        {
            Init(10001055, 30092000, 4, 7, 4, 7);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:礦工之部屋(30092000)
    //目標坐標:(4,7) ~ (4,7)

    public class P10001056 : RandomPortal
    {
        public P10001056()
        {
            Init(10001056, 10060000, 149, 163, 149, 164);
        }
    }
    //原始地圖:摩根咖啡館(30010008)
    //目標地圖:摩根市(10060000)
    //目標坐標:(149,163) ~ (149,164)

    public class P10001057 : RandomPortal
    {
        public P10001057()
        {
            Init(10001057, 30010008, 3, 5, 3, 5);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根咖啡館(30010008)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001058 : RandomPortal
    {
        public P10001058()
        {
            Init(10001058, 10060000, 127, 78, 128, 78);
        }
    }
    //原始地圖:摩根寵物咖啡館(30010009)
    //目標地圖:摩根市(10060000)
    //目標坐標:(127,78) ~ (128,78)

    public class P10001059 : RandomPortal
    {
        public P10001059()
        {
            Init(10001059, 30010009, 3, 5, 3, 5);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根寵物咖啡館(30010009)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001060 : RandomPortal
    {
        public P10001060()
        {
            Init(10001060, 10060000, 170, 91, 170, 92);
        }
    }
    //原始地圖:摩根古董商店(30014002)
    //目標地圖:摩根市(10060000)
    //目標坐標:(170,91) ~ (170,92)

    public class P10001061 : RandomPortal
    {
        public P10001061()
        {
            Init(10001061, 30014002, 3, 5, 3, 5);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根古董商店(30014002)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001062 : RandomPortal
    {
        public P10001062()
        {
            Init(10001062, 10060000, 151, 130, 152, 130);
        }
    }
    //原始地圖:摩根裁縫店(30020006)
    //目標地圖:摩根市(10060000)
    //目標坐標:(151,130) ~ (152,130)

    public class P10001063 : RandomPortal
    {
        public P10001063()
        {
            Init(10001063, 30020006, 3, 5, 3, 5);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根裁縫店(30020006)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001064 : RandomPortal
    {
        public P10001064()
        {
            Init(10001064, 10060000, 167, 130, 168, 130);
        }
    }
    //原始地圖:摩根寶石商店(30021004)
    //目標地圖:摩根市(10060000)
    //目標坐標:(167,130) ~ (168,130)

    public class P10001065 : RandomPortal
    {
        public P10001065()
        {
            Init(10001065, 30021004, 3, 5, 3, 5);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根寶石商店(30021004)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001066 : RandomPortal
    {
        public P10001066()
        {
            Init(10001066, 10060000, 139, 149, 140, 149);
        }
    }
    //原始地圖:摩根武器商店(30012004)
    //目標地圖:摩根市(10060000)
    //目標坐標:(139,149) ~ (140,149)

    public class P10001067 : RandomPortal
    {
        public P10001067()
        {
            Init(10001067, 30012004, 3, 5, 3, 5);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:摩根武器商店(30012004)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001068 : RandomPortal
    {
        public P10001068()
        {
            Init(10001068, 10060000, 145, 124, 145, 125);
        }
    }
    //原始地圖:闇黑武器屋(30002002)
    //目標地圖:摩根市(10060000)
    //目標坐標:(145,124) ~ (145,125)

    public class P10001069 : RandomPortal
    {
        public P10001069()
        {
            Init(10001069, 30002002, 3, 5, 3, 5);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:闇黑武器屋(30002002)
    //目標坐標:(3,5) ~ (3,5)

    public class P10001095 : RandomPortal
    {
        public P10001095()
        {
            Init(10001095, 50021000, 8, 12, 8, 12);
        }
    }
    //原始地圖:摩根市(10060000)
    //目標地圖:古魯杜的大宅(50021000)
    //目標坐標:(8,12) ~ (8,12)

    public class P10001096 : RandomPortal
    {
        public P10001096()
        {
            Init(10001096, 10060000, 80, 130, 81, 131);
        }
    }
    //原始地圖:古魯杜的大宅(50021000)
    //目標地圖:摩根市(10060000)
    //目標坐標:(80,130) ~ (81,131)

}