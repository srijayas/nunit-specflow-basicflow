using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xamarin.UITest;
using NUnit.Framework;
using System.Configuration;
using Newtonsoft.Json.Linq;
using nunit.Lib.TestRailLib;
namespace nunit
{
    [Binding]
    public sealed class Hooks1
    {
        private bool _testRailIntegration = false;
        private  TestRail _testRail = null;
        private static Hooks1 hInst = null;
        private JObject _testPlan = null;
        private JObject _testRunEntry = null;
        private bool _testCaseTagPresent = false;
        private List<string> _casesPerFeature;


        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Hooks.BeforeTestRun");
            hInst = new Hooks1();

            //----
            // Test Rail:
            if (ConfigurationManager.AppSettings["test_rail_update"] == "true")
            {
                hInst._testRailIntegration = true;
                hInst._testRail = new TestRail();
                
            }

        }

        [BeforeFeature()]
        public static void BeforeFeature()
        {
            //TestContext.CurrentContext.Test.Arguments[0])  available only for nunit 3+
            FeatureContext.Current.Add("fc_type", (Platform)TestContext.CurrentContext.Test.Properties["fc_type"]);// TestContext.CurrentContext.Test.Arguments[0]);
            Console.WriteLine("Hooks.BeforeFeature " + FeatureContext.Current["fc_type"].ToString());
            Platform platform = (Platform) FeatureContext.Current["fc_type"]; //Platform.Android;
            IApp app;
            //-----
            if (platform == Platform.Android)
            {
                
                string ass = AppDomain.CurrentDomain.BaseDirectory; //System.Reflection.Assembly.GetExecutingAssembly().Location;
                app = ConfigureApp
                    .Android.ApkFile(System.IO.Path.GetDirectoryName(ass) + "/Binary/com.xamarin.samples.taskydroidnew.exampleapp.apk")
                    .StartApp(Xamarin.UITest.Configuration.AppDataMode.DoNotClear);
                FeatureContext.Current.Add("fc_app", app);

            }
            else
            {
                Assert.Pass("Ignored - iOS not defined");
                
            }
            if (hInst._testPlan == null && hInst._testRailIntegration)
            {
                // Query Test Plans list to see if the testplan exists for the Fixture
                hInst._testPlan = (hInst._testRail.FindTestPlan(platform.ToString()) ?? null);
                // If null, create a Test Plan for the Platform ---------------
                if (hInst._testPlan == null)
                {
                    PlanFields testplan = new PlanFields();
                    testplan.name =  platform.ToString();
                    testplan.description = "auto desc for " + platform.ToString();
                    hInst._testPlan = hInst._testRail.AddPlan(testplan);
                }
            }
 

           //TODO: Cleanup

        }

        [BeforeScenario]
        public static void BeforeScenario()
        {
            Console.WriteLine("Hooks.BeforeScenario");
            // Specflow Tag contans TC id
            if (ScenarioContext.Current.ScenarioInfo.Tags.Any(e=>e.Contains("TC#")) && hInst._testRailIntegration)
            {
                hInst._testCaseTagPresent = true;
                string tcid = ScenarioContext.Current.ScenarioInfo.Tags.Single(e => e.Contains("TC#"));
                tcid = tcid.Split('#')[1];
                ScenarioContext.Current.Add("tr_tcid", tcid);

                // Add a test run to plan ----------------
                // Create a run entry once for the Feature if not created already --------------
                // This is because a caseid is needed inorder to create a run entry.
                string planID = hInst._testPlan["id"].ToString();
 
                
                if (hInst._testRunEntry == null)
                {
                    RunFields testrun = new RunFields();
                    testrun.case_ids = new string[1] { tcid };
                    testrun.name = "autotestun " + DateTime.Now.ToString("MMddyyyy-hhmmss");
                    testrun.description = "auto desc";
                    hInst._testRunEntry = hInst._testRail.AddRunToPlan(planID, testrun);
                    hInst._casesPerFeature = new List<string>();
                    hInst._casesPerFeature.Add(tcid);

                }
                else
                {
                    PlanRunFields pf = new PlanRunFields();
                    pf.name = hInst._testRunEntry["runs"].FirstOrDefault()["name"].ToString();
                    hInst._casesPerFeature.Add(tcid);
                    pf.case_ids = hInst._casesPerFeature.ToArray();

                    string planRunID = hInst._testRunEntry["id"].ToString();//["runs"].FirstOrDefault()["id"].ToString();
                    JObject jrun = hInst._testRail.UpdateRunInPlan(planID, planRunID, pf);
                }
                
            }
        }

        [AfterStep]
        public static void AfterStep()
        {
            if (hInst._testRailIntegration && hInst._testCaseTagPresent)
            {
                List<StepResultFields> lststepres = null;
                if (ScenarioContext.Current.ContainsKey("sc_steps"))
                {
                    lststepres = ScenarioContext.Current.Get<List<StepResultFields>>("sc_steps");
                }
                else
                {
                    lststepres = new List<StepResultFields>();
                    ScenarioContext.Current.Add("sc_steps", lststepres);
                }
                if (ScenarioContext.Current.TestError != null)
                {
                    
                    lststepres.Add(new StepResultFields
                    {
                        status_id = ResultID.Failed,
                        content = ScenarioStepContext.Current.StepInfo.Text,
                        actual = ScenarioContext.Current.TestError.Message
                    });

                }
                else //Pass
                {
                    lststepres.Add(new StepResultFields
                    {
                        status_id = ResultID.Passed,
                        content = ScenarioStepContext.Current.StepInfo.Text,
                        actual = "Step Passed"
                    });

                }
                ScenarioContext.Current["sc_steps"] = lststepres;
            }

        }
        [AfterScenario]
        public static void AfterScenario()
        {
            if (hInst._testRailIntegration && hInst._testCaseTagPresent)
            {
                // Post scenario cleanup 
                hInst._testCaseTagPresent = false;  
                

                ResultFields testres = new ResultFields();
                testres.custom_step_results = ScenarioContext.Current.Get<List<StepResultFields>>("sc_steps");
                testres.status_id = ResultID.Passed;
                //failure
                if (ScenarioContext.Current.TestError != null)
                {
                    testres.status_id = ResultID.Failed;
                }
                string runID = hInst._testRunEntry["runs"].Children().FirstOrDefault()["id"].ToString();
                hInst._testRail.AddResultForTestCase(runID, ScenarioContext.Current["tr_tcid"].ToString(), testres);
            }
            Console.WriteLine("Hooks.AfterScenario");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            hInst._testRunEntry = null;
            hInst._casesPerFeature = null;
        
        }
    }
}
