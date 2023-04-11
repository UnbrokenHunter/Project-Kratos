using System;
using System.Collections.Generic;
using System.Linq;

public static class Constants {
    public const string JoinKey = "j";
    public const string DifficultyKey = "d";
    public const string GameTypeKey = "t";

    public enum GameTypes {
        BattleRoyal,
        CaptureTheFlag,
        Brawl 
    }
    public static List<GameTypes> GameTypesList() => Enum.GetValues(typeof(Constants.GameTypes))
                .Cast<Constants.GameTypes>()
                .ToList(); 
    
    public static readonly List<string> Difficulties = new() { "Easy", "Medium", "Hard" };
}