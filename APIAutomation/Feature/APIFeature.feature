Feature: API Automation for REQRES

Background:
Given I have a HTTP Client
And I am testing URL
And I have a route parameter 
And I accept content-type application/json

Scenario: Register a user
	Given the following parameters
	| email                 | password |
	| sruthyjosep@gmail.com | 1234567  |
	And I have the request body
	And I send the request
	And verify the response


