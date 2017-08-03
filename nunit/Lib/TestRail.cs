using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gurock.TestRail;
using TechTalk.SpecFlow;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace nunit.Lib.TestRailLib
{
    public enum ResultID { Passed = 1, Blocked, Untested, Retest, Failed }

    public class PlanFields
    {
        public PlanFields() { }
        public string name { get; set; }
        public string description { get; set; }
        public string ReturnJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    public class PlanRunFields
    {
        public PlanRunFields() { }
        //public string suite_id { get; set; } = "1";
        public string name { get; set; }
        public string description { get; set; }
        public string[] case_ids { get; set; }
        public string ReturnJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
    public class RunFields
    {
        public RunFields() { }
        public string suite_id { get; set; } = "1";
        public string name { get; set; }
        public string description { get; set; }
        public bool include_all { get; set; } = false;
        public string[] case_ids { get; set; }
        public string ReturnJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    public class StepResultFields
    {
        public StepResultFields() { }
        public string content { get; set; }
        public string expected { get; set; }
        public string actual { get; set; }
        public ResultID status_id { get; set; }
        public string ReturnJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

    }
    public class ResultFields
    {
        public ResultFields() { }
        public ResultID status_id { get; set; }
        public List< StepResultFields> custom_step_results { get; set; }
        public string ReturnJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
    public class TestRail
    {
        
        //-- Template classes to store data
    

        private APIClient _api = null;
        private string _projID = string.Empty;
        public APIClient api
        {
            get { return _api; }
            set { _api = value; }
        }
        public TestRail()
        {
                _api = new APIClient(ConfigurationManager.AppSettings["test_rail_server"].ToString());
                _api.User = ConfigurationManager.AppSettings["test_rail_user"];
                _api.Password = ConfigurationManager.AppSettings["test_rail_pwd"];
                _projID = ConfigurationManager.AppSettings["test_rail_project_id"];
                this.api = _api;
                this.ProjectID = _projID;
        
        }
        public string ProjectID { get; set; }

        public JObject FindTestPlan(string planName)
        {
            //string runfields = runFields.ReturnJSON();
            
            JArray jobj = (JArray)_api.SendGet("get_plans/" + _projID);
            JObject plan = null;
            try
            {
                plan = jobj.Children<JObject>().Where(e => e["name"].Value<string>().Equals(planName)).FirstOrDefault();
            }
            catch { }
            return plan;
        }

        public JObject AddPlan(PlanFields planFields)
        {
            //string runfields = runFields.ReturnJSON(); //needs serializable obj
            JObject jobj = (JObject)_api.SendPost("add_plan/" + _projID, planFields);
            return jobj;
        }

        public JObject AddRunToPlan(string planID, RunFields runFields)
        {
            JObject jobj = (JObject)_api.SendPost("add_plan_entry/" + planID, runFields);
            return jobj;
        }

        public JObject UpdateRunInPlan(string planID, string runEntryID, PlanRunFields runFields)
        {
            try
            {
                JObject jobj = (JObject)_api.SendPost("update_plan_entry/" + planID + "/" + runEntryID, runFields);
                return jobj;
            }
            catch 
            {
                return null;
            }
        }

        public JObject AddRun(RunFields runFields)
        {
            JObject jobj = (JObject)_api.SendPost("add_run/" + _projID, runFields);
            return jobj;
        }

        public JObject AddResultForTestCase(string runid, string tcid, ResultFields resFields)
        {
            JObject jobj = (JObject)_api.SendPost("add_result_for_case/" + runid + "/" + tcid, resFields);
            return jobj;
        }


    }
}
