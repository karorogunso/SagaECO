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
    public class RKUJI : SagaMap.Scripting.Item

    {
        public RKUJI()
        {
            
            Init(85000001, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券1");
                TakeItem(pc, 22000104, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000002, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券2");
                TakeItem(pc, 22000105, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000003, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券3");
                TakeItem(pc, 22000106, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000004, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券4");
                TakeItem(pc, 22000107, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000005, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券5");
                TakeItem(pc, 22000108, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000006, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券6");
                TakeItem(pc, 22000109, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000007, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券7");
                TakeItem(pc, 22000110, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000008, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券8");
                TakeItem(pc, 22000111, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000009, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券9");
                TakeItem(pc, 22000112, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
            Init(85000010, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券10");
                TakeItem(pc, 22000113, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000045, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "周年纪念☆特别篇!");
                TakeItem(pc, 22000180, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000011, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券11");
                TakeItem(pc, 22000114, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000012, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券12");
                TakeItem(pc, 22000115, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000046, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "春☆特别篇!");
                TakeItem(pc, 22000181, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000013, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券13");
                TakeItem(pc, 22000116, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000047, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "初夏☆特别篇!");
                TakeItem(pc, 22000182, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000014, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券14");
                TakeItem(pc, 22000117, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000015, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券15");
                TakeItem(pc, 22000118, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000016, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券16");
                TakeItem(pc, 22000119, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000048, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券DX");
                TakeItem(pc, 22000183, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });  
	    Init(85000017, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券17");
                TakeItem(pc, 22000122, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });          
	    Init(85000049, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "新春精选");
                TakeItem(pc, 22000184, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });
	    Init(85000018, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券18");
                TakeItem(pc, 22000125, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            }); 
	    Init(85000050, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券DX第二弹");
                TakeItem(pc, 22000185, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });  
	    Init(85000019, delegate(ActorPC pc)
            {
                GiveRandomTreasure(pc, "激赏礼券19");
                TakeItem(pc, 22000123, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });








        Init(85000051, delegate(ActorPC pc)//特殊
            {
                GiveRandomTreasure(pc, "女王和小伙伴们的作死活动");
                TakeItem(pc, 22010100, 1);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
            });


        Init(85100050, delegate(ActorPC pc)//特殊
        {
            GiveRandomTreasure(pc, "全服通用作死套装");
            TakeItem(pc, 22110100, 1);
            PlaySound(pc, 2040, false, 100, 50);
            Say(pc, 0, "出现了随机的礼物!", "激赏礼券");
        });


	    Init(85002045, delegate(ActorPC pc)//打开ECO之心
            {
                //GiveRandomTreasure(pc, "激赏礼券10");
                TakeItem(pc, 22000103, 1);
		GiveItem(pc, 16000300, 10);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "得到了10个时空之钥E!", "ECO之心");
            });
	    Init(90000210, delegate(ActorPC pc)//打开10个装
            {
                //GiveRandomTreasure(pc, "激赏礼券10");
                TakeItem(pc, 16000301, 1);
		GiveItem(pc, 16000300, 10);
                PlaySound(pc, 2040, false, 100, 50);
                Say(pc, 0, "得到了10个时空之钥E!", "ECO之心");
            });
        }
    }
}