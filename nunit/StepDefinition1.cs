﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Should;
using nunit.POM;
using nunit.Lib;
namespace nunit
{
    [Binding]
    public sealed class StepDefinition1 : StepBase
    {


        [Given(@"I open the app")]
        public void GivenIOpenTheApp()
        {
            Console.WriteLine("Given");


        }

        [When(@"I click on Add Task button")]
        public void WhenIClickOnAddTaskButton()
        {
            app.Tap(TaskPage.instance.btnAddTask);
            //app.Tap(b => b.Button("Add Task"));
            //app.Repl();
        }

        [When(@"I enter Name ""(.*)"" and Notes ""(.*)""")]
        public void WhenIEnterNameAndNotes(string name, string notes)
        {
           
            app.WaitForElement(TaskPage.instance.txtName);
            WidgetHelper.inst.EnterTextField(TaskPage.instance.txtName, name);
            System.Threading.Thread.Sleep(1000);
            WidgetHelper.inst.EnterTextField(TaskPage.instance.txtNotes, notes);
            System.Threading.Thread.Sleep(1000);
            //app.ClearText(e => e.TextField("txtName"));
            //app.EnterText(e => e.TextField("txtName"), name);
            //app.ClearText(e => e.TextField("txtNotes"));
            //app.EnterText(e => e.TextField("txtNotes"), notes);
            //System.Threading.Thread.Sleep(1000);
            // app.Repl();
        }

        [When(@"I click Save")]
        public void WhenIClickSave()
        {
            app.Tap(b => b.Button("btnSave"));
        }

        [Then(@"the task ""(.*)"" is saved and listed")]
        public void ThenTheTaskIsSavedAndListed(string taskName)
        {
            app.WaitForElement(e => e.Marked("lstTasks"));
            app.Query(e => e.Marked(taskName)).Length.ShouldBeGreaterThan(0);

        }

        [When(@"I open the task ""(.*)""")]
        public void WhenIOpenTheTask(string taskName)
        {
            app.Query(e => e.Marked(taskName)).Length.ShouldBeGreaterThan(0);
            app.DoubleTap(e => e.Marked(taskName));
            
        }

        [When(@"I click Delete")]
        public void WhenIClickDelete()
        {
            app.Tap(TaskPage.instance.btnDeleteTask);
        }

        [Then(@"the task ""(.*)"" is deleted from the list")]
        public void ThenTheTaskIsDeletedFromTheList(string taskName)
        {
            app.WaitForElement(e => e.Marked("lstTasks"));
            app.Query(e => e.Marked(taskName)).Length.ShouldEqual(0);
        }

    }
}
