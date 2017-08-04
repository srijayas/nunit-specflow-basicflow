README:
POC to demonstrate the use of Xamarin UITest for XPlatform mobile UI automation.
This framework automates the functional acceptance BDD tests.

The suite uses the following tools/framework:
- NUnit test framework
- Techtalk.Specflow for BDD integration
- Xamarin.UITest for xPlatform tests (currently implemented and tested only on Android, created placeholders for iOS)
- Gurock TestRail Rest API
- TaskyPro app for Android as a sample app: 
(Precomplied binary: https://github.com/RobGibbens/BddWithXamarinUITest/tree/master/binaries)

Implemented BDD hooks to use:
1) BeforeFeature to start App based on platform only once per feature
2) Create TestRail Plan, Run entries and report result per test and step


To Run:
-------
* Locally Debug/Run from Visual Studio using Test Explorer.

* Locally run from commandline:
 - Make sure Android emulator is running before executing this command. 
 - If using Jenkins to run the job, use the "Android Emulator" plugin to run the emulator

	 "%WORKSPACE%\packages\NUnit.Runners.2.6.4\tools\nunit-console.exe" /run nunit.SpecFlowFeature1Feature(Android) /labels /out=TestResult.txt /xml=TestResult.xml "%WORKSPACE%\nunit\bin\Debug\nunit.dll"

* Run on Xamarin cloud:
"%WORKSPACE%\packages\Xamarin.UITest.2.0.9\tools\test-cloud.exe" submit "%WORKSPACE%\nunit\bin\Debug\Binary\com.xamarin.samples.taskydroidnew.exampleapp.apk" <api_key> --devices <deviceid> --series "POC-singleDevice" --locale "en_US" --user srijaya.suresh@slalom.com --assembly-dir "%WORKSPACE%\nunit\bin\Debug" --nunit-xml "nresults.xml"


Issues encountered and resolution/workaround:
--------------------------------------------
1) "Run in Xamarin Test Cloud" option is not coming up in Visual Studio.

Looks like this is a known issue for some instances of VS.
Submitted to cloud thru commandline:
Ex:
"%WORKSPACE%\packages\Xamarin.UITest.2.0.9\tools\test-cloud.exe" submit "%WORKSPACE%\nunit\bin\Debug\Binary\com.xamarin.samples.taskydroidnew.exampleapp.apk" API_KEY --devices <devices> --series "POC-singleDevice" --locale "en_US" --user srijaya.suresh@slalom.com --assembly-dir "%WORKSPACE%\nunit\bin\Debug" --nunit-xml "nresults.xml"

2) Specflow 2.2 error:
System.BadImageFormatException : MVAR 1 () cannot be expanded in this context with 1 instantiations

Downgrade Specflow to 2.1.0 as per https://github.com/techtalk/SpecFlow/issues/889

3) Nunit version error when submitting the run to Cloud:
NUnit Version 3.7.1.0 is not supported at this time. The recommended version is 2.6.4.

Downgrade Nunit to 2.6.4
But cannot use TestContext.CurrentContext.Test.Arguments[0])  to get the TestFixture param. Available only for nunit 3+. So added TestContext.CurrentContext.Test.Properties,Add ("fc_type")
Changed OnetimeSetup/TearDown attributes to TestFixtureSetup/TearDown attributes
Downloaded nunit 2.6.4 msi for the gui runner

4) Run error:
Android - libmonosgen library sqlite problems with app native libraries while running on Android devices >= v.7

More: <https://forums.xamarin.com/discussion/78234/android-libmonosgen-library-sqlite-problems-with-app-native-libraries> 
Created new test run on X Test cloud for new series with new set of devices < Android 7

5) Jenkins local run:
"C:\Program Files (x86)\Android\android-sdk\tools\emulator" -avd "VisualStudio_android-23_x86_phone" 
Throws:
PANIC: Unknown AVD name [VisualStudio_android-23_x86_phone], use -list-avds to see valid list.

Added system env variable: 
ANDROID_AVD_HOME=c:\Users\<user>\.android\avd
Refer https://stackoverflow.com/questions/35701174/could-not-open-avd-name-avd-cache-img/36749380
Updated my Android SDK.
Added another system env variable ANDROID_SDK_ROOT.
Nothing helped. 
Finally ended up using Jenkins Android emulator: https://wiki.jenkins.io/display/JENKINS/Android+Emulator+Plugin
Emulator is still not visible during run, but the tests run and pass. Changing Jenkins service credentials to use my login didnt display the emulator either.



















