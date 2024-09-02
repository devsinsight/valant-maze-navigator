using Moq;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using ValantDemoApi.Models;
using ValantDemoApi.Repositories;
using ValantDemoApi.Services;

namespace ValantDemoApi.Tests
{

  [Binding]
  public class MazeNavigationSteps
  {
    private Mock<IMazeRepository> _mockMazeRepository;
    private IMazeService _mazeService;
    private MazeState _mazeState;
    private readonly ScenarioContext _scenarioContext;

    public MazeNavigationSteps(ScenarioContext scenarioContext)
    {
      _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void Setup()
    {
      _mockMazeRepository = new Mock<IMazeRepository>();
      _mazeService = new MazeService(_mockMazeRepository.Object);
    }

    #region Scenario 1 - Upload a Maze Definition File

    [Given(@"the user is on the maze navigator page")]
    public void GivenTheUserIsOnTheMazeNavigatorPage()
    {
    }

    [Given(@"they upload a maze definition file")]
    public void GivenTheyUploadAMazeDefinitionFile()
    {
      _mockMazeRepository = new Mock<IMazeRepository>();
      _mazeService = new MazeService(_mockMazeRepository.Object);

      var mazeId = "TestMaze";
      var startPosition = new Position { X = 0, Y = 0 };
      var grid = new char[][]
         {
                new char[] { 'S', 'O', 'X' },
                new char[] { 'X', 'O', 'X' },
                new char[] { 'X', 'O', 'E' }
         };

      _mazeState = new MazeState(grid, startPosition);

      _mockMazeRepository.Setup(repo => repo.Get(mazeId))
                 .Callback<string>(id => Console.WriteLine($"Get called with id: {id}"))
                 .Returns(_mazeState);
      _mockMazeRepository.Setup(repo => repo.Save(mazeId, It.IsAny<MazeState>()))
                         .Callback<string, MazeState>((id, state) => _mazeState = state);

      _scenarioContext["UploadedMazeId"] = mazeId;
    }

    [Then(@"the maze is available to be loaded by the maze navigator")]
    public void ThenTheMazeIsAvailableToBeLoadedByTheMazeNavigator()
    {
      var mazeId = _scenarioContext["UploadedMazeId"].ToString();
      var mazeState = _mazeService.GetMazeState(mazeId);
      Assert.NotNull(mazeState);
      _scenarioContext["MazeState"] = mazeState;
    }

    #endregion

    #region Scenario 2 - Selecting a Previously Uploaded Maze

    [Given(@"the user has previously uploaded a maze definition file")]
    public void GivenTheUserHasPreviouslyUploadedAMazeDefinitionFile()
    {
      GivenTheyUploadAMazeDefinitionFile();
    }

    [When(@"the user clicks the maze selector control")]
    public void WhenTheUserClicksTheMazeSelectorControl()
    {
      var mazeId = _scenarioContext["UploadedMazeId"]?.ToString();
      var mazeState = _mazeService.GetMazeState(mazeId);
      _scenarioContext["MazeState"] = mazeState;
    }

    [Then(@"the maze appears in the list of available mazes")]
    public void ThenTheMazeAppearsInTheListOfAvailableMazes()
    {
      var mazeState = (MazeStateDto)_scenarioContext["MazeState"];
      Assert.NotNull(mazeState);
    }

    #endregion

    #region Scenario 3 - Loadting and Navigating a Maze

    [When(@"the user selects an available maze")]
    public void WhenTheUserSelectsAnAvailableMaze()
    {
      var mazeId = _scenarioContext["UploadedMazeId"]?.ToString();
      var mazeState = _mazeService.GetMazeState(mazeId);
      _scenarioContext["MazeState"] = mazeState;
      Assert.NotNull(mazeState);
    }

    [Then(@"the maze navigator positions the user at the start of the maze")]
    public void ThenTheMazeNavigatorPositionsTheUserAtTheStartOfTheMaze()
    {
      var mazeState = (MazeStateDto)_scenarioContext["MazeState"];
      Assert.AreEqual(mazeState.StartPosition, new Position { X = 0, Y = 0 });
    }

    [Then(@"the user is presented with options to navigate to an adjoining cell")]
    public void ThenTheUserIsPresentedWithOptionsToNavigateToAnAdjoiningCell()
    {
      var availableMoves = new[] { "Up", "Down", "Right", "Left" };
      var moves = _mazeService.GetNextAvailableMoves();
      Assert.IsNotEmpty(moves);
    }

    #endregion

    #region Scenario 4 - Completing the Maze

    [Given(@"the user has loaded a maze")]
    public void GivenTheUserHasLoadedAMaze()
    {
      var mazeId = _scenarioContext["UploadedMazeId"].ToString();
      var mazeState = _mazeService.GetMazeState(mazeId);
      Assert.NotNull(mazeState);

      _scenarioContext["MazeState"] = mazeState;
    }

    [When(@"the user has successfully navigated to the end of the maze")]
    public void WhenTheUserHasSuccessfullyNavigatedToTheEndOfTheMaze()
    {
      var mazeId = _scenarioContext["UploadedMazeId"].ToString();

      _mazeService.Navigate(mazeId, "Right");
      Assert.AreEqual(new Position { X = 1, Y = 0 }, _mazeState.CurrentPosition);


      _mazeService.Navigate(mazeId, "Down");
      Assert.AreEqual(new Position { X = 1, Y = 1 }, _mazeState.CurrentPosition);


      _mazeService.Navigate(mazeId, "Down");
      Assert.AreEqual(new Position { X = 1, Y = 2 }, _mazeState.CurrentPosition);


      _mazeService.Navigate(mazeId, "Right");
      Assert.AreEqual(new Position { X = 2, Y = 2 }, _mazeState.CurrentPosition);


      _scenarioContext["MazeState"] = _mazeState;
      Assert.AreEqual('E', _mazeState.Grid[2][2]);
    }

    [Then(@"the maze navigator should display a success message")]
    public void ThenTheMazeNavigatorShouldDisplayASuccessMessage()
    {
      var mazeState = (MazeState)_scenarioContext["MazeState"];
      Assert.AreEqual(mazeState.CurrentPosition, new Position { X = 2, Y = 2 });
    }

    #endregion
  }

}
