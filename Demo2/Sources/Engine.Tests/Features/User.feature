Feature: User
	To make sure blog events are handled correctly I want to make sure they are stored correctly in Neo4j

@user
Scenario: User created
	When The following user created event is send
		| Name   |
		| Thomas |
	Then I expect the following nodes:
		| Node | Name   |
		| User | Thomas |