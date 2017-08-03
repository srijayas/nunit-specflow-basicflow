README:
This suite uses the following tools/framework:
NUnit test framework
Techtalk.Specflow for BDD integration
Xamarin.UITest for xPlatform tests (currently implemented and tested only on Android, created placeholders for iOS)
TaskyPro app for Android as a sample app: 
(Precomplied binary: https://github.com/RobGibbens/BddWithXamarinUITest/tree/master/binaries)

Implemented BDD hooks to use:
1) BeforeFeature to start App only once per feature

To Run:
-------
Locally Debug/Run from Visual Studio:

Locally run from commandline:
 - Make sure Android emulator is running before executing this command. 
 - If using Jenkins to run the job, use the "Android Emulator" plugin to run the emulator

	 "%WORKSPACE%\packages\NUnit.Runners.2.6.4\tools\nunit-console.exe" /run nunit.SpecFlowFeature1Feature(Android) /labels /out=TestResult.txt /xml=TestResult.xml "%WORKSPACE%\nunit\bin\Debug\nunit.dll"

Run on Xamarin cloud:
"%WORKSPACE%\packages\Xamarin.UITest.2.0.9\tools\test-cloud.exe" submit "%WORKSPACE%\nunit\bin\Debug\Binary\com.xamarin.samples.taskydroidnew.exampleapp.apk" <api_key> --devices <deviceid> --series "POC-singleDevice" --locale "en_US" --user srijaya.suresh@slalom.com --assembly-dir "%WORKSPACE%\nunit\bin\Debug" --nunit-xml "nresults.xml"
