using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xamarin.UITest;
using NUnit.Framework;
namespace nunit
{
    [Binding]
    public sealed class Hooks1
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            Console.WriteLine("Hooks.BeforeTestRun");
            
        }

        [BeforeFeature()]
        public static void BeforeFeature()
        {
            Console.WriteLine("Hooks.BeforeFeature");
            Platform platform = Platform.Android;
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

           

            //----
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
