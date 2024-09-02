using System;
using System.Collections.Generic;

namespace ValantDemoApi.Models
{
  public class MazeDefinition
  {
    public string Id { get; set; }
    public char[][] Grid { get; set; }
    public Position Start { get; set; }
    public Position End { get; set; }
  }

  public class Position
  {
    public int X { get; set; }
    public int Y { get; set; }

    public override bool Equals(object obj)
    {
      if (obj is Position other)
      {
        return X == other.X && Y == other.Y;
      }
      return false;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y);
    }
  }

  public class MazeNavigationResult
  {
    public Position CurrentPosition { get; set; }
    public bool IsAtEnd { get; set; }
    public string[] Movements { get; set; }
  }

  public class MazeState
  {
    public char[][] Grid { get; }
    public Position StartPosition { get; set; }
    public Position CurrentPosition { get; set; }
    public List<string> Movements { get; } = new List<string>();

    public MazeState() { }

    public MazeState(char[][] grid, Position startPosition)
    {
      Grid = grid;
      StartPosition = startPosition;
      CurrentPosition = startPosition;
    }

    public Position CalculateNewPosition(string direction)
    {
      var newPosition = new Position
      {
        X = CurrentPosition.X,
        Y = CurrentPosition.Y
      };

      switch (direction.ToLower())
      {
        case "up":
          newPosition.Y -= 1;
          break;
        case "down":
          newPosition.Y += 1;
          break;
        case "right":
          newPosition.X += 1;
          break;
        case "left":
          newPosition.X -= 1;
          break;
      }

      return newPosition;
    }

    public bool IsMoveValid(Position position)
    {
      if (position.Y < 0 || position.Y >= Grid.Length || position.X < 0 || position.X >= Grid[position.Y].Length)
      {
        return false;
      }

      return Grid[position.Y][position.X] != 'X';
    }
  }

  public class MazeStateDto
  {
    public char[][] Grid { get; set; }
    public Position CurrentPosition { get; set; }
    public Position StartPosition { get; set; }
  }
}
