using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using TechTalk.SpecFlow;
namespace nunit.Lib
{
    public class WidgetHelper
    {
        public static WidgetHelper inst = new WidgetHelper();

        private static IApp app;
        private static Platform type;
        public WidgetHelper()
        {
            app = (IApp)FeatureContext.Current["fc_app"];
            type = (Platform)FeatureContext.Current["fc_type"];
    }
        //IApp app;
        //Platform type;
        //public TaskPage()
        //{
        //    app = (IApp)FeatureContext.Current["fc_app"];
        //    type = (Platform)FeatureContext.Current["fc_type"];
        //}
        public void EnterTextField(Func<AppQuery,AppQuery> txt, string val)
        {
            app.ClearText(txt);
            app.EnterText(txt, val);
        }
    }
}
