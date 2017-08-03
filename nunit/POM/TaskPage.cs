using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;
using TechTalk.SpecFlow;
namespace nunit.POM
{
    public class TaskPage
    {
        public static TaskPage instance = new TaskPage();
        IApp app;
        Platform type;
        public TaskPage()
        {
            app = (IApp) FeatureContext.Current["fc_app"];
            type = (Platform)FeatureContext.Current["fc_type"];
        }
        public Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery> btnAddTask
        {
            get
            {
                if (type is Platform.Android)
                    return (e => e.Button("Add Task"));
                else //iOS placeholder
                    return (e => e.Button("Add Task"));
            }
        }
        public Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery> txtName
        {
            get
            {
                if (type is Platform.Android)
                    return (e => e.TextField("txtName"));
                else //iOS placeholder
                    return (e => e.TextField("txtName"));
            }
        }
        public Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery> btnDeleteTask
        {
            get
            {
                if (type is Platform.Android)
                    return (e => e.Button("btnCancelDelete"));
                else //iOS placeholder
                    return (e => e.Button("btnCancelDelete"));
            }
        }
        public Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery> txtNotes
        {
            get
            {
                if (type is Platform.Android)
                    return (e => e.TextField("txtNotes"));
                else //iOS placeholder
                    return (e => e.TextField("txtNotes"));
            }
        }

    }
}
