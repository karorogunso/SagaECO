using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap.Localization;
using SagaMap.Localization.Languages;

namespace SagaMap.Manager
{
    public class LocalManager:Singleton<LocalManager>
    {
        public enum Languages
        {
            English,
            Chinese,
        }

        Strings stringset = new SagaMap.Localization.Languages.English();
        Languages lan = Languages.English;

        public LocalManager()
        {
        }

        public Languages CurrentLanguage
        {
            get
            {
                return this.lan;
            }
            set
            {
                switch (value)
                {
                    case Languages.English:
                        stringset = new English();
                        break;
                    case Languages.Chinese:
                        stringset = new Chinese();
                        break;
                    default :
                        stringset = new English();
                        break;
                }
                lan = value;
            }
        }

        public Strings Strings
        {
            get
            {
                return stringset;
            }
        }
    }
}
