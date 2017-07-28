using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace nunit
{
    [Binding]
    public sealed class Hooks1
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Hooks.BeforeTestRun");
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            Console.WriteLine("Hooks.BeforeFeature");
        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            Console.WriteLine("Hooks.BeforeScenario");
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            Console.WriteLine("Hooks.AfterScenario");
        }
    }
}
