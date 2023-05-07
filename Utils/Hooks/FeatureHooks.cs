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
