using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaScript.Chinese.Enums;
//正在測試的開發內容
namespace SagaScript.M50131000 //只有書的白色世界
{
    public class S11002323 : Event
    {
        public S11002323()
        {
            this.EventID = 11002323;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch (Select(pc, "要做甚麼?", "", "『空白的瞬間』", "『通往巔峰的存在』", "『               』", "不要做"))
            {
                case 1:
                    Warp(pc, 20190000, 100, 100);
                    break;

                case 2:
                    break;

                case 3:
                    break;
            }

        }

        /*
        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, " $R;" +
            "$R $R;" +
            "$R $R;", "白色書本");

            //
            /*
             Say(pc, 0, "。$R;" +
            "$R$R;" +
            "$R？$R;", "白い本");
            */
        }
    }
