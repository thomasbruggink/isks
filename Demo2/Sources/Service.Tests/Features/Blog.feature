Feature: Blogs
	To verify a user can request his read blogs correctly

Background: 
	Given The following user created events have been sent
		| Name   |
		| Thomas |
		| Wiljag |
	And The following blog created events have been sent
		| Name                                            | Writer |
		| Dit is mijn eerste blog                         | Thomas |
		| Deze blog heeft informatie over Test Automation | Thomas |
		| De blog over de ISKS 2016                       | Wiljag |

@blog
Scenario: Request the read articles for a user
	Given The following blog read events are send
		| Reader | Blog Name                                       |
		| Wiljag | Dit is mijn eerste blog                         |
		| Wiljag | Deze blog heeft informatie over Test Automation |
	When 'Wiljag' requests his reads
	Then The following blogs are returned
		| User   | Name                                            |
		| Wiljag | Dit is mijn eerste blog                         |
		| Wiljag | Deze blog heeft informatie over Test Automation |

@blog
Scenario: Reading a blog will show up in the read blogs list
	Given The following blog read events are send
		| Reader | Blog Name                                       |
		| Wiljag | Dit is mijn eerste blog                         |
	When The following blog read events are send
		| Reader | Blog Name                                       |
		| Wiljag | Deze blog heeft informatie over Test Automation |
	And 'Wiljag' requests his reads
	Then The following blogs are returned
		| User   | Name                                            |
		| Wiljag | Dit is mijn eerste blog                         |
		| Wiljag | Deze blog heeft informatie over Test Automation |
