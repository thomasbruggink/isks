Feature: Blogs
	As a user I want to read and create blogs

Background: 
	Given The following accounts are created
         | UserName |
         | Thomas   |
         | Wiljag   |
	And The following blogs are created
		 | Title                       | Content                                                                      | Writer |
		 | Mijn eerste blog            | Dit is mijn eerste blog!                                                     | Thomas |
		 | Advances in test automation | In de isks van 2016 gaan we in op advances in test automation binnen knowNow | Wiljag |

@blogs
Scenario: Read a blog
	Given I look at the 'blog' page
	Then I see the following blogs
	| Title                       | Content                                                                      | Writer |
	| Mijn eerste blog            | Dit is mijn eerste blog!                                                     | Thomas |
	| Advances in test automation | In de isks van 2016 gaan we in op advances in test automation binnen knowNow | Wiljag |

@blogs
Scenario: When logged in I can create a blog
	When I login as 'Thomas'
	And I create the following blogs
	| Title            | Content                                                |
	| Mijn tweede blog | Dit is mijn tweede blog omdat de eerste zo goed lukte! |
	Then I see the following blogs
    | Title            | Content                                                | Writer |
    | Mijn tweede blog | Dit is mijn tweede blog omdat de eerste zo goed lukte! | Thomas |

@blogs
Scenario: When a user creates a blog other users can read it
	When I login as 'Thomas'
	And I create the following blogs
    | Title     | Content                                   |
    | ISKS 2016 | Deze blog beschrijft details over de ISKS |
	And I login as 'Wiljag'
	And I look at the 'blog' page
	Then I see the following blogs
    | Title     | Content                                   | Writer |
    | ISKS 2016 | Deze blog beschrijft details over de ISKS | Thomas |