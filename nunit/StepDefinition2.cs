using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using NUnit.Framework;
namespace nunit
{
    [Binding]
    public sealed class StepDefinition2 : StepBase
    {
        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int p0)
        {
            Assert.True(true,"This scenario will pass.");
        }

    }
}
