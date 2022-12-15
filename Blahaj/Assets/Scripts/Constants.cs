using System;
using System.Collections;
using System.Collections.Generic;
namespace Constants
{
    public enum GameState
    {
        StartMenu,
        NewGame,
        InGame,
        WinLevel,
        NextLevel,
        PauseGame,
        DigestionState,
        WinGame,
        LoseGame,
        Crafting,
    }

    public enum Skill
    {
        HpUp,
        AttackUp,
        AttackSpeedUp,
        MovementSpeedUp,
        Stun,
        Poison,
        Explosion,
        Healing,
    }

    public enum Orbs
    {
        Red,
        Purple,
        Yellow,
    }

    public enum SpriteStates
    {
        Normal,
        Stun,
        Poison,
        Explosion,
    }

    public static class GameScenes
    {
        public const String StartMenu = "StartMenu";
        public const String Game1 = "Game1";
        public const String DigestionScene = "DigestionScene";
        public const String Game2 = "Game2";
        public const String WinGame = "WinGame";
        public const String LoseGame = "LoseGame";
    }

    public static class GameTags
    {
        public const String Player = "Player";
        public const String Enemy = "Enemy";
        public const String PurpleOrb = "PurpleOrb";
        public const String YellowOrb = "YellowOrb";
        public const String RedOrb = "RedOrb";
        public const String WinPortal = "WinPortal";

    }

    public static class GameStages
    {
        public static String[] stages = new String[] { 
            "Game1",
            "DigestionScene",
            "Game2",
            };
    }
}