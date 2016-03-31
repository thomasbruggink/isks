Feature: User
	As a new user I want to create an account and be able to login

@user
Scenario: Create an account
	When I create an account as 'Thomas'
	Then I see the 'Account created!' message

@user
Scenario: After I created an account I can login
	Given The following accounts are created
	| UserName |
	| Thomas   |
	When I login as 'Thomas'
	Then I see the 'Welcome Thomas!' message 
