using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaDB.Item; //引用道具系統作交換

using SagaScript.Chinese.Enums;
//正在測試的開發內容
namespace SagaScript.M20190000 //白色世界 空白的瞬間 任務
{
    public class S11002278 : Event
    {
        public S11002278()
        {
            this.EventID = 11002278;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
        //未完成
        /*.制作[百战炼磨之符（F、SU）]和[士魂商才之证（BP）]
  ·需要用到道具精制技能，地图上也有个NPC会做，5000一次
  ·50个[珠魂] 或者1个[大珠魂]做一次
  ·大致的成功率是[百战炼磨之符]>[合成失败物]>>>>>>>>>>>>>>>>>>>>>[士魂商才之证]
  ·士魂商才之证10组大概能成功一组的样子，至少我做的时候只用了10组，不过wiki上也有30组才出的脸黑报告……
  */


        // 註:本地圖有傳點 EVENT ID: 11001862
    }
}