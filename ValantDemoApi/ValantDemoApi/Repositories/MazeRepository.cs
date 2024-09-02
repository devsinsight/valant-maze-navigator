using Microsoft.Extensions.Caching.Memory;
using ValantDemoApi.Models;

namespace ValantDemoApi.Repositories
{

  public interface IMazeRepository
  {
    void Save(string mazeId, MazeState mazeState);
    MazeState Get(string mazeId);
  }

  public class MazeRepository : IMazeRepository
  {
    private readonly IMemoryCache _memoryCache;
    private readonly string _cachePrefix = "Maze_";

    public MazeRepository(IMemoryCache memoryCache)
    {
      _memoryCache = memoryCache;
    }

    public void Save(string mazeId, MazeState mazeState)
    {
      var cacheKey = GetCacheKey(mazeId);
      _memoryCache.Set(cacheKey, mazeState);
    }

    public MazeState Get(string mazeId)
    {
      var cacheKey = GetCacheKey(mazeId);
      _memoryCache.TryGetValue(cacheKey, out MazeState mazeState);
      return mazeState;
    }

    private string GetCacheKey(string mazeId)
    {
      return $"{_cachePrefix}{mazeId}";
    }
  }


}
