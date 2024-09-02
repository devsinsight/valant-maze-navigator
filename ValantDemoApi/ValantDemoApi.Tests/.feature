Feature: Maze Navigation

  Scenario: 01 - Uploading a maze file
    Given the user is on the maze navigator page
    And they upload a maze definition file
    Then the maze is available to be loaded by the maze navigator

  Scenario: 02 - Selecting a previously uploaded maze
    Given the user is on the maze navigator page
    And the user has previously uploaded a maze definition file
    When the user clicks the maze selector control
    Then the maze appears in the list of available mazes

  Scenario: 03 - Loading and navigating a maze
    Given the user is on the maze navigator page
    And the user has previously uploaded a maze definition file
    When the user selects an available maze
    Then the maze navigator positions the user at the start of the maze
    And the user is presented with options to navigate to an adjoining cell

  Scenario: 04 - Completing the maze
    Given the user has previously uploaded a maze definition file
    And the user has loaded a maze
    When the user has successfully navigated to the end of the maze
    Then the maze navigator should display a success message
