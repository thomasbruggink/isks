Feature: Blog
	To make sure blog events are handled correctly I want to make sure they are stored correctly in Neo4j

Background: 
	Given The following user created events have been sent
		| Name   |
		| Thomas |
		| Wiljag |

@blog
Scenario: Create blog
	When The following blog created events are send
		| Name                      | Writer |
		| Dit is mijn eerste blog   | Thomas |
		| De blog over de ISKS 2016 | Wiljag |
	Then I expect the following nodes:
		| Node | Name                      |
		| Blog | Dit is mijn eerste blog   |
		| Blog | De blog over de ISKS 2016 |
	And I expect the following relations:
		| From Node | From Name | To Node | To Name                   | RelationName | Attributes |
		| User      | Thomas    | Blog    | Dit is mijn eerste blog   | CREATED      | -          |
		| User      | Wiljag    | Blog    | De blog over de ISKS 2016 | CREATED      | -          |
	And I expect not to see the following relations:
		| From Node | From Name | To Node | To Name                   | RelationName |
		| User      | Wiljag    | Blog    | Dit is mijn eerste blog   | CREATED      |
		| User      | Thomas    | Blog    | De blog over de ISKS 2016 | CREATED      |

@blog
Scenario: Read blog
	Given The following blog created events have been sent
		| Name                    | Writer |
		| Dit is mijn eerste blog | Thomas |
	When The following blog read events are send
		| Reader | Blog Name               |
		| Wiljag | Dit is mijn eerste blog |
	Then I expect the following relations:
		| From Node | From Name | To Node | To Name                 | RelationName | Attributes |
		| User      | Wiljag    | Blog    | Dit is mijn eerste blog | READ         | -          |
