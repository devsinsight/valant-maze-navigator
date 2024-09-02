using System;
using System.Collections.Generic;
using ValantDemoApi.Models;
using ValantDemoApi.Repositories;

namespace ValantDemoApi.Services
{
  public interface IMazeService
  {
    void SaveMaze(string mazeId, char[][] grid, Position startPosition);
    MazeStateDto GetMazeState(string mazeId);
    MazeNavigationResult Navigate(string mazeId, string direction);
    IEnumerable<string> GetNextAvailableMoves();
  }

  public class MazeService : IMazeService
  {
    private readonly IMazeRepository _mazeRepository;

    public MazeService(IMazeRepository mazeRepository)
    {
      _mazeRepository = mazeRepository;
    }

    public void SaveMaze(string mazeId, char[][] grid, Position startPosition)
    {
      var mazeState = new MazeState(grid, startPosition);
      _mazeRepository.Save(mazeId, mazeState);
    }

    public MazeStateDto GetMazeState(string mazeId)
    {
      var mazeState = _mazeRepository.Get(mazeId);
      if (mazeState == null)
      {
        throw new ArgumentException("Maze not found");
      }

      return new MazeStateDto
      {
        Grid = mazeState.Grid,
        StartPosition = mazeState.StartPosition
      };
    }

    public MazeNavigationResult Navigate(string mazeId, string direction)
    {
      var mazeState = _mazeRepository.Get(mazeId);
      if (mazeState == null)
      {
        throw new ArgumentException("Maze not found");
      }

      var newPosition = mazeState.CalculateNewPosition(direction);

      if (!mazeState.IsMoveValid(newPosition))
      {
        throw new InvalidOperationException("Invalid move. You hit a wall!");
      }

      mazeState.CurrentPosition = newPosition;
      mazeState.Movements.Add(direction);

      _mazeRepository.Save(mazeId, mazeState);

      return new MazeNavigationResult
      {
        CurrentPosition = mazeState.CurrentPosition,
        IsAtEnd = mazeState.Grid[newPosition.Y][newPosition.X] == 'E',
        Movements = mazeState.Movements.ToArray()
      };
    }

    public IEnumerable<string> GetNextAvailableMoves()
    {
      return new List<string> { "Up", "Down", "Left", "Right" };
    }
  }

}
