using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaMap;
using SagaMap.Manager;
using SagaScript.Chinese.Enums;
using SagaMap.ActorEventHandlers;
using SagaMap.Mob;
using SagaDB.Mob;
using SagaMap.Skill;
namespace SagaScript.M30210000
{
    public class S70001003 : Event
    {
        public S70001003()
        {
            this.EventID = 70001003;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.MapID != pc.TInt["AAA序章剧情图2"]) return;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            ActorMob mob = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图2"], 16580002, 10006100, 0, 86, 160, 1, 1, 0, AAA序章怪物.有攻击力的伊利斯Info(), AAA序章怪物.有攻击力的伊利斯AI(), null, 0)[0];
            ((MobEventHandler)mob.e).AI.Master = pc;
            Timer timer = new Timer("AAA序章步骤4", 0, 3000);
            timer.OnTimerCall += (s, e) =>
            {
                timer.Deactivate();
                if (pc == null) return;

                ShowDialog(pc, 10008);
                ActorMob 巴鲁鲁 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图2"], 10990000, 10006100, 0, 118, 165, 1, 1, 0, AAA序章怪物.受伤的巴鲁鲁Info(), AAA序章怪物.受伤的巴鲁鲁AI(), (x, y) =>
                {
                    //SkillHandler.Instance.ActorSpeak(x.mob, "呜........");
                    ShowDialog(pc,10010);
                    SetNextMoveEvent(pc, 70001004);
                }, 1)[0];
                MobEventHandler eh = (MobEventHandler)巴鲁鲁.e;
                巴鲁鲁.HP = 巴鲁鲁.MaxHP / 10;
                eh.AI.Master = pc;
                ActorMob 红鹫 = map.SpawnCustomMob(10000000, (uint)pc.TInt["AAA序章剧情图2"], 10140000, 10006100, 1, 119, 165, 1, 1, 0, AAA序章怪物.饥饿的红鹫Info(), AAA序章怪物.饥饿的红鹫AI(), (x, y) =>
                {
                    SagaMap.Skill.Additions.Global.OtherAddition skill = new SagaMap.Skill.Additions.Global.OtherAddition(null, pc,巴鲁鲁, "AAA剧情序章巴鲁鲁自杀", 50000,2000);
                    skill.period = 2000;
                    byte count = 0;
                    ChatArg parg = new ChatArg();
                    parg.motion = (SagaLib.MotionType)361;
                    parg.loop = 1;
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, null, 巴鲁鲁, false);
                    skill.OnUpdate += (z, w) =>
                    {
                        if(巴鲁鲁.HP == 0)
                            skill.Deactivate();
                        if (count == 1)
                            SkillHandler.Instance.ActorSpeak(巴鲁鲁, "呜呜呜.....");
                        if (count == 3)
                            SkillHandler.Instance.ActorSpeak(巴鲁鲁, "呜呜.....");
                        if (count >=6)
                        {
                            SkillHandler.Instance.ActorSpeak(巴鲁鲁, "呜........");
                            SkillHandler.Instance.CauseDamage(pc, 巴鲁鲁, (int)(巴鲁鲁.HP * 3), true);
                            
                            SkillHandler.Instance.ShowEffectOnActor(巴鲁鲁, 8044);
                            skill.Deactivate();
                        }
                        SkillHandler.Instance.ShowVessel(巴鲁鲁, 5000,0,0, SkillHandler.AttackResult.Critical);
                        count++;

                    };
                    SkillHandler.ApplyAddition(巴鲁鲁, skill);
                }, 1)[0];
                MobEventHandler eh3 = (MobEventHandler)红鹫.e;
                eh3.AI.DamageTable.Add(pc.ActorID, 10000);
                MobEventHandler eh2 = (MobEventHandler)巴鲁鲁.e;
                SkillHandler.Instance.ActorSpeak(红鹫, "呷！！！！！");
                eh2.AI.Hate.Add(巴鲁鲁.ActorID, 1000000);
            };
            timer.Activate();
        }
    }
}

