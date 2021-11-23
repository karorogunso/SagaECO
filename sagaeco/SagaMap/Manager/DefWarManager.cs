using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

using Newtonsoft.Json;

using SagaLib;
using SagaDB.Actor;
using SagaDB.Map;
using SagaDB.DefWar;



namespace SagaMap.Manager
{
    class DefWarManager : Singleton<DefWarManager>
    {
        public DefWarManager()
        {
            string s = ScriptManager.Instance.VariableHolder.AStr["defwarlist"];
            if(!string.IsNullOrEmpty(s))
                list = JsonConvert.DeserializeObject<Dictionary<uint, List<DefWar>>>(s);
        }

        void save()
        {
            ScriptManager.Instance.VariableHolder.AStr["defwarlist"] =
                JsonConvert.SerializeObject(list);
        }

        Dictionary<uint, List<DefWar>> list = new Dictionary<uint, List<DefWar>>();

        public List<DefWar> GetDefWarList(uint mapid)
        {
            if (list.ContainsKey(mapid))
                return list[mapid];
            return null;
        }

        public bool IsDefWar(uint mapid)
        {
            return list.ContainsKey(mapid) && list[mapid].Count > 0;
        }


        public bool SetDefWar(uint mapid, uint dwid, byte dwn, byte r1,byte r2)
        {
            if (list.ContainsKey(mapid))
            {
                DefWar dw = list[mapid].First(x => x.ID == dwid);
                if (dw == null)
                {
                    //dw = DefWarFactory.Instance.GetItem(dwid);
                    dw = new DefWar(dwid);
                    list[mapid].Add(dw);
                }
                dw.Number = dwn;
                dw.Result1 = r1;
                dw.Result2 = r2;
                MapManager.Instance.GetMap(mapid).DefWarChange(dw);
                save();
            }
            return true;
        }

        public void DefWarResult(uint mapid, byte r1, byte r2, int exp, int jobexp, int cp, byte u = 0)
        {
            MapManager.Instance.GetMap(mapid).DefWarResult(r1, r2, exp, jobexp, cp, u);
        }

        public void DefWarState(uint mapid, byte rate)
        {
            MapManager.Instance.GetMap(mapid).DefWarState(rate);
        }

        public void DefWarStates(Dictionary<uint, byte> list)
        {
            foreach (KeyValuePair<uint, byte> i in list)
            {
                MapManager.Instance.GetMap(i.Key).DefWarStates(list);
            }
        }

        public void AddDefWar(uint mapid, DefWar dw)
        {
            if (!list.ContainsKey(mapid))
                list[mapid] = new List<DefWar>();
            if (!list[mapid].Exists(x => x.ID == dw.ID))
                list[mapid].Add(dw);
            save();
        }

        public void MapClear(uint mapid)
        {
            if (list.ContainsKey(mapid))
                list[mapid].Clear();
            save();
        }

        public void AllClear()
        {
            foreach (var i in list)
            {
                i.Value.Clear();
            }
            save();
        }

    }
}
