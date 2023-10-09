using System;

public delegate void GunShootEvent(Gun gun);

public delegate void GunBuildEvent(GunPlatform platform);

public delegate void GunUpgradeEvent(Gun platform);

public delegate void MonsterDefeatedEvent(Monster monster);

public delegate void MonsterHitCoreEvent(Monster monster);

public delegate void LevelStateChangedEvent(LevelState phase);

public delegate void LevelBalanceChangeEvent(float amount);

public static class Messaging<T> where T : Delegate
{
  private static T handle;

  public static void Register(T callback)
  {
    handle = Delegate.Combine(handle, callback) as T;
  }

  public static void Unregister(T callback)
  {
    handle = Delegate.Remove(handle, callback) as T;
  }

  public static T Trigger
  {
    get { return handle; }
  }
}