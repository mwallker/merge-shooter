using System;
using System.Collections.Generic;

public class Pool<T> where T : class
{
  private readonly Stack<T> _objectPool = new();

  private readonly Func<T> _onCreate;

  private readonly Action<T> _onRelease;

  public Pool(Func<T> onCreate, Action<T> onRelease)
  {
    _onCreate = onCreate;
    _onRelease = onRelease;
  }

  public T Get()
  {
    if (_objectPool.Count > 0)
    {
      return _objectPool.Pop();
    }
    else
    {
      return _onCreate();
    }
  }

  public void Release(T obj)
  {
    _objectPool.Push(obj);
    _onRelease(obj);
  }

  public void Clear()
  {
    _objectPool.Clear();
  }
}