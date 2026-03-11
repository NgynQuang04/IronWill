using System;

public static class GameEvents
{
    public static Action<int, int> OnEnergyChanged;

    public static Action OnPlayerDeath;

    public static Action OnGameWin;

    public static Action<int> OnAreaReached;
}