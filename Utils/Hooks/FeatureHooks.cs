using openWeather.Apis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace openWeather.Utils.Hooks
{
    [Binding]
    public static class FeatureHooks
    {
        [BeforeFeature()]
        public static void BeforeHooks(FeatureContext _featureContext)
        {
            if (_featureContext.FeatureInfo.Tags.Contains("Api"))
            {
                
            }
        }
    }
}
