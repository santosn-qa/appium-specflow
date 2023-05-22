Feature: Sample App - Home Screen

Scenario: Verify color change on the home screen
	Given I am on the home screen
	When I click on COLOR
	Then I should see a change in color

Scenario: Verify notification on the home screen
	Given I am on the home screen
	When I click on NOTIFICATION
	Then I should receive a notification on the device

Scenario: Capture text on the home screen
	Given I am on the home screen
	When I click on TEXT
	Then I capture the displayed text

Scenario: Verify pop-up message on the home screen
	Given I am on the home screen
	When I click on TOAST
	Then I should see a pop up message

Scenario: Capture upload and download speed on the speed testing screen
	Given I am on the home screen
	When I click on SPEED TEST
	Then I start the speed test from the speed test page
	And I capture the upload/download speed
	And I navigate back to the home screen