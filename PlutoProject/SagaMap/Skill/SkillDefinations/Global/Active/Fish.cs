
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaDB.Actor;
namespace SagaMap.Skill.SkillDefinations.Global
{
    /// <summary>
    /// 钓鱼
    /// </summary>
    public class Fish : ISkill
    {
        #region ISkill Members
        public int TryCast(ActorPC sActor, Actor dActor, SkillArg args)
        {
            if(sActor.PossesionedActors.Count > 0)
            {
                SagaMap.Network.Client.MapClient.FromActorPC(sActor).SendSystemMessage("憑依時不能進行釣魚");
                return -13;
            }

            Map map = Manager.MapManager.Instance.GetMap(sActor.MapID);
            if (map.Info.canfish[args.x, args.y] == 41000003 && map.ID == 10032000)
            {

                uint baititem = sActor.EquipedBaitID;
                if (baititem != null)
                {
                    return 0;
                }
                else
                {
                    SagaMap.Network.Client.MapClient.FromActorPC(sActor).SendSystemMessage("沒有魚餌");
                    return -13;
                }
               
            }
            else
            {
                //SagaMap.Network.Client.MapClient.FromActorPC(sActor).SendSystemMessage("指定坐标x:" + args.x + "y:" + args.y);
                return -13;
            }
        }
        public void Proc(Actor sActor, Actor dActor, SkillArg args, byte level)
        {
            int lifetime = 10000000;
            Additions.Global.Fish skill1 = new SagaMap.Skill.Additions.Global.Fish(args.skill, sActor, lifetime, 60000);
            SkillHandler.ApplyAddition(sActor, skill1);
        }
        #endregion
    }
}