Feature: SpecFlowFeature2
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@TC#3
Scenario: Delete the task
	Given I open the app
	When I open the task "Automation Task-Upd"
	#And I enter Name "Automation Task-Upd" and Notes "test notes" 
	And I click Delete
	Then the task "Automation Task-Upd" is deleted from the list
