import 'cypress-file-upload';

describe('Maze Upload', () => {
  beforeEach(() => {
    // Visit the maze navigator page
    cy.visit('http://localhost:4200/maze');  // Adjust the URL as necessary
  });

  it('should allow the user to upload a maze definition file', () => {
    // Simulate the upload of a maze definition file
    const fileName = 'test-maze.txt';

    //Load the maze from fixtures
    cy.get('input[type=file]').attachFile(fileName)

    //Click on Upload Maze
    cy.get('button').click();

    //Navigate in the Labyrinth
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Down"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Down"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Down"]').click();
    cy.get('button[id="Down"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Up"]').click();
    cy.get('button[id="Up"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Right"]').click();
    cy.get('button[id="Down"]').click();
    cy.get('button[id="Down"]').click();
    cy.get('button[id="Down"]').click();
    cy.get('button[id="Down"]').click();
    
    cy.on('window:alert', (str) => {
      expect(str).to.equal(`Success! You have reached the end of the maze.`)
    })

  });
});
