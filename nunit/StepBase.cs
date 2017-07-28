using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xamarin.UITest;
namespace nunit
{
    
    public class StepBase
    {
        public IApp app;
        
        public StepBase()
        {
            app = (IApp) FeatureContext.Current["fc_app"];
        }
    }
}