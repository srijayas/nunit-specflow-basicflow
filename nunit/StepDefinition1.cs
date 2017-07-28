using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace nunit
{
    [Binding]
    public sealed class StepDefinition1
    {

        [Given("I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredSomethingIntoTheCalculator(int number)
        {
            Console.WriteLine("In given step");
        }

    }
}
