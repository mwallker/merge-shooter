using System;
using System.Collections.Generic;

public class Pool<T> where T : class
{
  private readonly Stack<T> _objectPool = new();

  private readonly Func<T> _onCreate;

  private readonly Action<T> _onGet;

  private readonly Action<T> _onRelease;

  public Pool(Func<T> onCreate, Action<T> onGet, Action<T> onRelease)
  {
    _onCreate = onCreate;
    _onGet = onGet;
    _onRelease = onRelease;
  }

  public T Get()
  {
    T objectReference = _objectPool.Count > 0 ? _objectPool.Pop() : _onCreate();
    _onGet(objectReference);

    return objectReference;
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