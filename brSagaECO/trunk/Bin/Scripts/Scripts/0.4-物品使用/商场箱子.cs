using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;

using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    public class 商场箱子 : Item
    {
        public 商场箱子()
        {

            //名探偵セット
            Init(90000166, delegate(ActorPC pc)
            {
                GiveItem(pc, 50020651, 1);
                GiveItem(pc, 60114300, 1);
                GiveItem(pc, 50069200, 1);
                GiveItem(pc, 31136900, 1);
                GiveItem(pc, 60084500, 1);
                TakeItem(pc, 10066315, 1);
            });


            //真夏のお嬢様セット
            Init(90000179, delegate(ActorPC pc)
            {
                GiveItem(pc, 50101050, 1);
                GiveItem(pc, 60105810, 1);
                GiveItem(pc, 50018402, 1);
                TakeItem(pc, 10066311, 1);
            });

            //白亜の麗人セット
            Init(90000180, delegate(ActorPC pc)
            {
                GiveItem(pc, 50041053, 1);
                GiveItem(pc, 60106550, 1);
                GiveItem(pc, 50010458, 1);
                GiveItem(pc, 50017250, 1);
                TakeItem(pc, 10066312, 1);
            });

            //おしゃまな助手セット
            Init(90000167, delegate(ActorPC pc)
            {
                GiveItem(pc, 50020652, 1);
                GiveItem(pc, 60113500, 1);
                GiveItem(pc, 50044400, 1);
                GiveItem(pc, 10061350, 1);
                TakeItem(pc, 10066316, 1);
            });

            //真夏の元気印セット
            Init(90000178, delegate(ActorPC pc)
            {
                GiveItem(pc, 50101050, 1);
                GiveItem(pc, 60106001, 1);
                GiveItem(pc, 50015555, 1);
                GiveItem(pc, 50067205, 1);
                TakeItem(pc, 10066310, 1);
            });
            //わんわんセット♂
            Init(90000181, delegate(ActorPC pc)
            {
                GiveItem(pc, 50026950, 1);
                GiveItem(pc, 50002764, 1);
                GiveItem(pc, 50010156, 1);
                GiveItem(pc, 50064451, 1);
                TakeItem(pc, 10066313, 1);
            });

            //わんわんセット♀
            Init(90000182, delegate(ActorPC pc)
            {
                GiveItem(pc, 50026950, 1);
                GiveItem(pc, 50001464, 1);
                GiveItem(pc, 50010156, 1);
                GiveItem(pc, 50064451, 1);
                TakeItem(pc, 10066314, 1);
            });

            //冒険者セット♀
            Init(90000197, delegate(ActorPC pc)
            {
                GiveItem(pc, 50106500, 1);
                GiveItem(pc, 60114600, 1);
                GiveItem(pc, 50201500, 1);
                GiveItem(pc, 50069400, 1);
                GiveItem(pc, 50076900, 1);
                TakeItem(pc, 10066317, 1);
            });

            //冒険者セット♂
            Init(90000198, delegate(ActorPC pc)
            {
                GiveItem(pc, 50106500, 1);
                GiveItem(pc, 60114700, 1);
                GiveItem(pc, 50201600, 1);
                GiveItem(pc, 50069400, 1);
                GiveItem(pc, 50076900, 1);
                TakeItem(pc, 10066318, 1);
            });

            //パイロットセット
            Init(90000199, delegate(ActorPC pc)
            {
                GiveItem(pc, 50106600, 1);
                GiveItem(pc, 60114800, 1);
                GiveItem(pc, 50069500, 1);
                TakeItem(pc, 10066319, 1);
            });

            //乗馬セット
            Init(90000200, delegate(ActorPC pc)
            {
                GiveItem(pc, 50106700, 1);
                GiveItem(pc, 60114900, 1);
                GiveItem(pc, 60115000, 1);
                GiveItem(pc, 50069600, 1);
                TakeItem(pc, 10066320, 1);
            });

            //大人の回復ECO缶パック
            Init(90000175, delegate(ActorPC pc)
            {
                GiveItem(pc, 10048701, 20);
                TakeItem(pc, 10066307, 1);
            });

            //大人の回復ECO缶（ソーダ）
            Init(90000176, delegate(ActorPC pc)
            {
                GiveItem(pc, 10048702, 20);
                TakeItem(pc, 10066308, 1);
            });
            //おめかしタータンチェック　はじめての60DAYSセット Ver.1 
            Init(90000147, delegate(ActorPC pc)
            {
                GiveItem(pc, 60106400, 1);
                GiveItem(pc, 50039900, 1);
                GiveItem(pc, 50067300, 1);
                GiveItem(pc, 50075000, 1);
                GiveItem(pc, 60042800, 1);
                TakeItem(pc, 10049063, 1);
            });
            //おめかしタータンチェック　つづきの30DAYSセット Ver.1
            Init(90000148, delegate(ActorPC pc)
            {
                GiveItem(pc, 60106401, 1);
                GiveItem(pc, 50040000, 1);
                GiveItem(pc, 50067400, 1);
                GiveItem(pc, 50075000, 1);
                GiveItem(pc, 50055000, 1);
                TakeItem(pc, 10049064, 1);
            });
            //コラボレーションパッケージ 新世紀エヴァンゲリオンversion.レイ&アスカ 
            Init(90000044, delegate(ActorPC pc)
            {
                switch (Select(pc, "合作包 新世紀福音戰士EVAversion", "", "綾波麗", "明日香"))

                // 02/09/2015 by hoshinokanade
                /*
                switch (Select(pc, "コラボレーションパッケージ 新世紀エヴァンゲリオンversion", "", "レイ", "アスカ"))
                */
                {
                    case 1:
                        GiveItem(pc, 60100800, 1);
                        GiveItem(pc, 50028100, 1);
                        GiveItem(pc, 60100900, 1);
                        GiveItem(pc, 50053500, 1);
                        GiveItem(pc, 60082300, 1);
                        TakeItem(pc, 10049055, 1);
                        break;
                    case 2:
                        GiveItem(pc, 60100500, 1);
                        GiveItem(pc, 60100700, 1);
                        GiveItem(pc, 60101100, 1);
                        GiveItem(pc, 60082400, 1);
                        TakeItem(pc, 10049055, 1);
                        break;
                }


            });
            //コラボレーションパッケージ 攻殻機動隊Ｓ．Ａ．Ｃ．シリーズ
            Init(90000152, delegate(ActorPC pc)
            {
                GiveItem(pc, 10059000, 1);
                GiveItem(pc, 50043100, 1);
                GiveItem(pc, 60106900, 1);
                GiveItem(pc, 60107000, 1);
                GiveItem(pc, 10020771, 1);
                TakeItem(pc, 29007102, 1);
            });
        }
    }
}