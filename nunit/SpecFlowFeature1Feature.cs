using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Xamarin.UITest;
namespace nunit
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public partial class SpecFlowFeature1Feature : FeatureBase
    {
        public SpecFlowFeature1Feature(Platform type): base(type)
        {
            
            Console.WriteLine("SpecFlowFeature1Feature constructor for " + type);
        }

       
        
    }
}
