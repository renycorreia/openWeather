using Autofac;
using openWeather.Apis.Suporte;
using System.Reflection;
using TechTalk.SpecFlow;

namespace openWeather.Utils.Hooks
{
    [Binding]
    public class Hooks
    {
        private static IContainer Container { get; set; }

        public Hooks()
        {
            RegisterServiceLocator();
        }

        [BeforeTestRun]
        public static void RegisterServiceLocator()
        {
            Container = Dependencies.CreateContainerBuilder().Build();
            var locator = new AutoFacServiceLocator(Container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }

        [AfterScenario]
        public static void UpdateStatusSkippedScenario(ScenarioContext _scenarioContext)
        {
            if (_scenarioContext.TestError != null && _scenarioContext.TestError.ToString().Contains("Ignorado"))
            {
                PropertyInfo testStatusProperty = (typeof(ScenarioContext)).GetProperty("ScenarioExecutionStatus", BindingFlags.Instance | BindingFlags.Public);
                PropertyInfo testErrorProperty = typeof(ScenarioContext).GetProperty("TestError", BindingFlags.Instance | BindingFlags.Public);

                testStatusProperty.SetValue(_scenarioContext, ScenarioExecutionStatus.Skipped);
                testErrorProperty.SetValue(_scenarioContext, null);
            }
        }
    }
}