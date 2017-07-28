using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
namespace nunit
{
    
    //[TestFixture("Android")]
    public class FeatureBase
    {
        
        public FeatureBase(string type)
        {
            Console.WriteLine("FeatureBase constructor for " + type);
        }
    }
}