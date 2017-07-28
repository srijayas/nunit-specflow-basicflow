using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
namespace nunit
{
    [TestFixture("Android")]
    [TestFixture("iOS")]
    public partial class SpecFlowFeature1Feature : FeatureBase
    {
        public SpecFlowFeature1Feature(string type): base(type)
        {
            
            Console.WriteLine("SpecFlowFeature1Feature constructor for " + type);
        }
    }
}
