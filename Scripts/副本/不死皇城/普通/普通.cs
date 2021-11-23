
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace WeeklyExploration
{
    public partial class GQuest : Event
    {
        void 进入普通(ActorPC pc)
        {
            foreach (var item in pc.Party.Members)
            {
                if (!item.Value.Online) { Say(pc, 131, "有人不在这里", ""); return; }
                if (item.Value.MapID != pc.Party.Leader.MapID) { Say(pc, 131, "有人不在这里", ""); return; }
            }
            pc.Party.MaxMember = 4;
            uint map1 = (uint)CreateMapInstance(10068000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            谷仓地带刷怪(map1);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map1;
                pc.Party.Leader.TInt["副本savemapX"] = 135;
                pc.Party.Leader.TInt["副本savemapY"] = 7;
                Say(item.Value, 0, "经过了一段时间的旅程..$R$R来到了【不死王城】的外围...");
                Warp(item.Value, map1, 135, 7);
            }
        }
        void 第八关2(ActorPC pc)
        {
            uint map8 = pc.MapID;
            不死皇城2刷怪(map8);
        }
        void 第二关(ActorPC pc)
        {
            uint map2 = (uint)CreateMapInstance(10069000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            不死皇城刷怪(map2);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map2;
                pc.Party.Leader.TInt["副本savemapX"] = 144;
                pc.Party.Leader.TInt["副本savemapY"] = 207;
                Say(item.Value, 0, "......");
                Warp(item.Value, map2, 144, 207);
            }
        }
        void 第三关(ActorPC pc)
        {
            uint map3 = (uint)CreateMapInstance(20129000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            城堡入口刷怪(map3);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map3;
                pc.Party.Leader.TInt["副本savemapX"] = 7;
                pc.Party.Leader.TInt["副本savemapY"] = 43;
                Say(item.Value, 0, "不知道谁在门口贴了张纸条，$R上面写着：$R$R请大家让光头和我打架啊QAQ");
                Warp(item.Value, map3, 7, 43);
            }
        }
        void 第四关(ActorPC pc)
        {
            uint map4 = (uint)CreateMapInstance(20108000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            皇宫1F刷怪(map4);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map4;
                pc.Party.Leader.TInt["副本savemapX"] = 7;
                pc.Party.Leader.TInt["副本savemapY"] = 21;
                Say(item.Value, 0, "......");
                Warp(item.Value, map4, 7, 21);
            }
        }
        void 第五关(ActorPC pc)
        {
            uint map5= (uint)CreateMapInstance(20109000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            皇宫2F刷怪(map5);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map5;
                pc.Party.Leader.TInt["副本savemapX"] = 20;
                pc.Party.Leader.TInt["副本savemapY"] = 22;
                Say(item.Value, 0, "......");
                Warp(item.Value, map5, 20, 22);
            }
        }
        void 第六关(ActorPC pc)
        {
            uint map6 = (uint)CreateMapInstance(20110000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            皇宫3F刷怪(map6);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map6;
                pc.Party.Leader.TInt["副本savemapX"] = 20;
                pc.Party.Leader.TInt["副本savemapY"] = 2;
                Say(item.Value, 0, "......");
                Warp(item.Value, map6, 20, 2);
            }
        }
        void 第七关(ActorPC pc)
        {
            uint map7 = (uint)CreateMapInstance(20111000, 91000999, 21, 21, true, 0, true);
            謁見之間刷怪(map7,pc);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map7;
                pc.Party.Leader.TInt["副本savemapX"] = 31;
                pc.Party.Leader.TInt["副本savemapY"] = 21;
                Say(item.Value, 0, "嗯...一路过来挺轻松的吧");
                Warp(item.Value, map7, 31, 21);
            }
        }
        void 第八关(ActorPC pc)
        {
            uint map8 = (uint)CreateMapInstance(10069000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            不死皇城2刷怪(map8);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map8;
                pc.Party.Leader.TInt["副本savemapX"] = 111;
                pc.Party.Leader.TInt["副本savemapY"] = 83;
                Say(item.Value, 0, "额。。。");
                Warp(item.Value, map8, 111, 83);
            }
        }
        void 第九关(ActorPC pc)
        {
            uint map9 = (uint)CreateMapInstance(20119000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            本館4F刷怪(map9);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map9;
                pc.Party.Leader.TInt["副本savemapX"] = 7;
                pc.Party.Leader.TInt["副本savemapY"] = 32;
                Say(item.Value, 0, "啊！");
                Warp(item.Value, map9, 7, 32);
            }
        }
        void 第十关(ActorPC pc)
        {
            uint map10 = (uint)CreateMapInstance(20118000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            本館3F刷怪(map10);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map10;
                pc.Party.Leader.TInt["副本savemapX"] = 14;
                pc.Party.Leader.TInt["副本savemapY"] = 33;
                Say(item.Value, 0, "哎哟————！");
                Warp(item.Value, map10, 14, 33);
            }
        }
        void 第十一关(ActorPC pc)
        {
            uint map11 = (uint)CreateMapInstance(20117000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            本館2F刷怪(map11);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map11;
                pc.Party.Leader.TInt["副本savemapX"] = 14;
                pc.Party.Leader.TInt["副本savemapY"] = 27;
                Say(item.Value, 0, "噗嗤？");
                Warp(item.Value, map11, 14, 28);
            }
        }
        void 第十二关(ActorPC pc)
        {
            uint map12 = (uint)CreateMapInstance(20116000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            本館1F刷怪(map12);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map12;
                pc.Party.Leader.TInt["副本savemapX"] = 18;
                pc.Party.Leader.TInt["副本savemapY"] = 30;
                Say(item.Value, 0, "啧啧啧");
                Warp(item.Value, map12, 18, 30);
            }
        }
        void 第十三关(ActorPC pc)
        {
            uint map13 = (uint)CreateMapInstance(20130000, 91000999, 21, 21, true, 0, true);
            连接通道刷怪(map13,pc);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map13;
                pc.Party.Leader.TInt["副本savemapX"] = 7;
                pc.Party.Leader.TInt["副本savemapY"] = 37;
                Say(item.Value, 0, "地板裂了...摔了下来");
                Warp(item.Value, map13, 7, 36);
            }
        }
        void 第十四关(ActorPC pc)
        {
            uint map14 = (uint)CreateMapInstance(20123000, 91000999, 21, 21, true, 0, true);
            pc.Party.Leader.TInt["副本BOSSID"] = 0;
            礼拜堂2F刷怪(map14);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map14;
                pc.Party.Leader.TInt["副本savemapX"] = 13;
                pc.Party.Leader.TInt["副本savemapY"] = 21;
                Say(item.Value, 0, "......");
                Warp(item.Value, map14, 13, 21);
            }
        }
        void 第十五关(ActorPC pc)
        {
            uint map15 = (uint)CreateMapInstance(60907000, 91000999, 21, 21, true, 0, true);
            魂之宫殿刷怪(map15,pc);
            foreach (var item in pc.Party.Members)
            {
                item.Value.TInt["副本复活标记"] = 1;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                pc.Party.Leader.TInt["副本savemapid"] = (int)map15;
                pc.Party.Leader.TInt["副本savemapX"] = 25;
                pc.Party.Leader.TInt["副本savemapY"] = 43;
                Say(item.Value, 0, "喵喵喵喵喵！！！！！");
                Warp(item.Value, map15, 25, 43);
            }
        }
         void 第十五关X(ActorPC pc)
        {
            uint map15 = (uint)CreateMapInstance(60907000, 91000999, 21, 21, true, 0, true);
            魂之宫殿刷怪(map15, pc);
            foreach (var item in pc.Party.Members)
            {
                item.Value.Mode = PlayerMode.NORMAL;
                item.Value.TInt["副本复活标记"] = 0;
                item.Value.Party.Leader.TInt["复活次数"] = 0;
                item.Value.Party.Leader.TInt["设定复活次数"] = 0;
                Say(item.Value, 0, "喵喵喵喵喵！！！！！");
                Warp(item.Value, map15, 25, 43);
            }
        }
    }
}

