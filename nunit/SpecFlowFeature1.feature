Feature: SpecFlowFeature1

@TC#1
Scenario: Add a new task
	Given I open the app
	When I click on Add Task button
	And I enter Name "Automation Task" and Notes "test notes" 
	And I click Save
	Then the task "Automation Task" is saved and listed
@TC#2
Scenario: Edit the task
	Given I open the app
	When I open the task "Automation Task"
	And I enter Name "Automation Task-Upd" and Notes "test notes" 
	And I click Save
	Then the task "Automation Task-Upd" is saved and listed
	