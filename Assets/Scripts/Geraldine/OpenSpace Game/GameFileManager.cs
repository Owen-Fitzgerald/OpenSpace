using System;
using System.IO;
using UnityEngine;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine.OpenSpaceGame.Interfaces;

namespace Geraldine.OpenSpaceGame
{
    public enum enGameLoadInstruction
    {
        New,
        Load,
        Save
    }

    public enum enMapLoadInstruction
    {
        Generate,
        Save,
        Load
    }

    public enum enLoadState
    {
        //Initial Checks
        Day0,
        //Game Load
        Day1,
        //Galaxy Load
        Day2,
        //Star System Load
        Day3,
        //Planet Load
        Day4,
        //Region Load
        Day5,
        //Hex Load
        Day6,
        //Final Checks
        Day7
    }

    public static class GameFileManager
    {
        const int mapFileVersion = 5;

        public static string MapFilePathBase = String.Format("{0}/{1}/", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Test Gaming/Witchlight/Maps");
        public static string GameFilePathBase = String.Format("{0}/{1}/", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Test Gaming/Witchlight/Save Data");
        public static string MapFileExtension = ".map";
        public static string GameFileExtension = ".save";

        public static enGameLoadInstruction GameLoadInstruction { get; set; }
        public static string GamePath { get; set; }
        public static enMapLoadInstruction MapLoadInstruction { get; set; }
        public static string MapToLoadPath { get; set; }
        public static enLoadState CurrentLoadState { get; set; }

        public static void SaveMap(INodeGraph<INode> grid)
        {
            using var writer = new BinaryWriter(File.Open(MapToLoadPath, FileMode.Create));
            writer.Write(mapFileVersion);
            grid.Save(writer);
        }

        public static void LoadMap(INodeGraph<INode> grid)
        {
            //Before Starting perform an error check
            CurrentLoadState = enLoadState.Day0;
            if (!File.Exists(MapToLoadPath))
            {
                Debug.LogError("File does not exist " + MapToLoadPath);
                return;
            }

            //Start loading map at head node
            using var reader = new BinaryReader(File.OpenRead(MapToLoadPath));
            int header = reader.ReadInt32();
            if (header <= mapFileVersion)
            {
                CurrentLoadState = enLoadState.Day1;
                grid.Load(reader, header);
            }
            else
            {
                Debug.LogWarning("Unknown map format " + header);
            }

            //Perform Final Checks
            CurrentLoadState = enLoadState.Day7;
        }

        public static void SaveGame(IGame Game)
        {
            using var writer = new BinaryWriter(File.Open(GamePath, FileMode.Create));
            writer.Write(mapFileVersion);
            Game.Save(writer);
        }

        public static void LoadGame(IGame Game)
        {
            //Before Starting perform an error check
            CurrentLoadState = enLoadState.Day0;
            if (!File.Exists(GamePath))
            {
                Debug.LogError("File does not exist " + GamePath);
                return;
            }

            //Start loading map at head node
            using var reader = new BinaryReader(File.OpenRead(GamePath));
            int header = reader.ReadInt32();
            if (header <= mapFileVersion)
            {
                CurrentLoadState = enLoadState.Day1;
                Game.Load(reader, header);
            }
            else
            {
                Debug.LogWarning("Unknown map format " + header);
            }

            //Perform Final Checks
            CurrentLoadState = enLoadState.Day7;
        }
    }
}
