using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Xamarin.UITest;
namespace nunit
{
    
    //[TestFixture("Android")]
    public class FeatureBase
    {
        
        public FeatureBase(Platform  type)
        {

            TestContext.CurrentContext.Test.Properties.Add("fc_type", type);
            Console.WriteLine("FeatureBase constructor for " + type.ToString());
            
        }
    }
}