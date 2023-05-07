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
    public static class ScenarioHooks
    {
        [BeforeScenario()]
        public static void BeforeHooks(ScenarioContext _scenarioContext)
        {
            if (_scenarioContext.ScenarioInfo.Tags.Contains("Api"))
            {
               
            }
        }
    }
}
