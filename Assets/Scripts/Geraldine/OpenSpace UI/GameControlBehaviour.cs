using UnityEngine;
using UnityEngine.UI;
using Geraldine.OpenSpaceGame;
using Geraldine.OpenSpaceGame.GameFlow;
using Geraldine.OpenSpaceGame.Interfaces;
using System;

namespace Geraldine.OpenSpaceUI
{
    public class GameControlBehaviour : MonoBehaviour
    {
        #region member fields
        public static string GameObjectName = "GameController";

        public IGame GameData { get; private set; }

        public event EventHandler evPlayerTurnChanged;

        public GameObject LoadingScreen;
        public Slider LoadingSlider;
        #endregion

        public void Start()
        {
            bool gameLoaded = false;
            bool mapLoaded = false;
            LoadingScreen.SetActive(true);

            enGameLoadInstruction GameLoadInstruction = GameFileManager.GameLoadInstruction;
            GameData = new Game();
            //switch (GameLoadInstruction)
            //{
            //    case enGameLoadInstruction.New:
            //        //Create New Game
            //        enMapLoadInstruction MapLoadInstruction = GameFileManager.MapLoadInstruction;
            //        switch (MapLoadInstruction)
            //        {
            //            case enMapLoadInstruction.Generate:
            //                //Generate New World
            //                GalaxyInitializer init = new GalaxyInitializer() { Size = GameFileManager.Map };
            //                nodeGraph.DefineGraph(GalaxyFactory.GenerateGalaxy(init, 0));

            //                mapLoaded = true;
            //                break;
            //            case enMapLoadInstruction.Load:
            //                //Load Existing World
            //                GameFileManager.LoadMap(GameData.Galaxy);
            //                mapLoaded = true;
            //                break;
            //        }
            //        GenerateNewGame();
            //        gameLoaded = true;
            //        break;
            //    case enGameLoadInstruction.Load:
            //        GameFileManager.LoadGame(GameData);
            //        mapLoaded = true;
            //        gameLoaded = true;
            //        break;
            //}

            if(!mapLoaded || !gameLoaded)
            {
                Debug.LogError("Game not successfully created");
            }

            //Update UI
            //HexMapCamera.ValidatePosition();
            evPlayerTurnChanged?.Invoke(this, new EventArgs());
            LoadingScreen.SetActive(false);
            //RefreshShaderData();
        }

        public void OnGUI()
        {
            if(GameFileManager.CurrentLoadState != enLoadState.Day7)
            {
                LoadingSlider.maxValue = (int)enLoadState.Day7;
                LoadingSlider.value = (int)GameFileManager.CurrentLoadState;
            }
        }


        public void GenerateNewGame()
        {

            //Create Starting Units
            //foreach (IPlayer player in GameData.Players)
            //{
            //    INode StartingTile = GetStartingTile();

            //    WitchlightUnit startingSettler = Instantiate(WitchlightUnit.Prefab);
            //    startingSettler.GetComponent<WitchlightUnit>().ApplyBlueprint(UnitInfoDatabase.Instance.LoadedDatabase.FirstOrDefault());
            //    startingSettler.GetComponent<WitchlightUnit>().OwnerID = player.ID;

            //    player.OwnedUnits.Add(startingSettler.GetComponent<WitchlightUnit>());
            //    GameData.Galaxy.AddOccupant(startingSettler, StartingTile, UnityEngine.Random.Range(0f, 360f));

            //    INode StartingNeighbor = null;
            //    for (int d = 0; d <= (int)HexDirection.NW; d++)
            //    {
            //        if(StartingTile.TryGetNeighbor((HexDirection)d, out StartingNeighbor) &&
            //            !StartingNeighbor.IsUnderwater && StartingNeighbor.Occupant == null)
            //        { 
            //            break;
            //        }
            //    }

            //    WitchlightUnit startingRanger = Instantiate(WitchlightUnit.Prefab);
            //    startingRanger.GetComponent<WitchlightUnit>().ApplyBlueprint(UnitInfoDatabase.Instance.LoadedDatabase.FirstOrDefault(_ => _.UniqueID == "UNIT_RANGER"));
            //    startingRanger.GetComponent<WitchlightUnit>().OwnerID = player.ID;

            //    player.OwnedUnits.Add(startingRanger.GetComponent<WitchlightUnit>());
            //    GameData.Galaxy.AddOccupant(startingRanger, StartingNeighbor, UnityEngine.Random.Range(0f, 360f));

            //    player.StoredResources[enStrategicResource.Orders] = 5;

            //    IList<INode> VisibleTiles = new List<INode>();
            //    foreach (WitchlightUnit unit in player.OwnedUnits)
            //    {
            //        foreach (INode node in GameData.Galaxy.GetVisibleCells(unit.Location, unit.Vision).Where(_ => !VisibleTiles.Contains(_)))
            //        {
            //            VisibleTiles.Add(node);
            //        }
            //    }

            //    player.DiscoveredTiles = player.DiscoveredTiles.Union(VisibleTiles).ToList();
            //    player.VisibleTiles = VisibleTiles;
            //}
        }


        //public void MoveGameToNextTurn()
        //{
        //    bool needPlayerInput = false;
        //    //Move AI Units in Player Order
        //    for (int i = GameData.CurrentPlayerID + 1; i <= GameData.Players.LastOrDefault().ID; i++)
        //    {
        //        IPlayer player = GameData.Players.FirstOrDefault(_ => _.ID == i);
        //        GameData.CurrentPlayerID = player.ID;
        //        if (player.PlayerController == enPlayerController.AI) { player.MakeAIMovements(); }
        //        else
        //        {
        //            needPlayerInput = true;
        //            break;
        //        }
        //    }

        //    if (GameData.CurrentPlayerID != GameData.Players.LastOrDefault().ID && needPlayerInput)
        //    {
        //        //Adjust to the next Human Player control
        //        //Reset Shader to new player
        //        IPlayer player = GameData.Players.FirstOrDefault(_ => _.ID == GameData.CurrentPlayerID);

        //        IList<INode> VisibleTiles = new List<INode>();
        //        foreach (WitchlightCity city in player.OwnedCities)
        //        {
        //            foreach (INode node in GameData.Galaxy.DetermineCityInfluencedCells(city.Location).Where(_ => !VisibleTiles.Contains(_)))
        //            {
        //                VisibleTiles.Add(node);
        //            }
        //        }
        //        foreach (WitchlightUnit unit in player.OwnedUnits)
        //        {
        //            foreach (INode node in GameData.Galaxy.GetVisibleCells(unit.Location, unit.Vision).Where(_ => !VisibleTiles.Contains(_)))
        //            {
        //                VisibleTiles.Add(node);
        //            }
        //        }

        //        //Update UI
        //        OrderSystem.Instance.Clear();
        //        evPlayerTurnChanged.Invoke(this, new EventArgs());

        //        player.DiscoveredTiles = player.DiscoveredTiles.Union(VisibleTiles).ToList();
        //        player.VisibleTiles = VisibleTiles;
        //    }
        //    else
        //    {
        //        //Harvest harvestables and reset for next turn
        //        GameData.NextTurn();
        //        foreach (IPlayer p in GameData.Players)
        //        {
        //            p.NextTurn();
        //        }

        //        //Begin New Turn
        //        GameData.CurrentPlayerID = 0;

        //        //Update UI
        //        OrderSystem.Instance.Clear();
        //        evPlayerTurnChanged.Invoke(this, new EventArgs());
        //        //UpdateUI();

        //        //Adjust to the next Human Player control
        //        //Reset Shader to new player
        //        IPlayer player = GameData.Players.FirstOrDefault(_ => _.ID == GameData.CurrentPlayerID);

        //        IList<INode> VisibleTiles = new List<INode>();
        //        foreach (WitchlightCity city in player.OwnedCities)
        //        {
        //            foreach (INode node in GameData.Galaxy.DetermineCityInfluencedCells(city.Location).Where(_ => !VisibleTiles.Contains(_)))
        //            {
        //                VisibleTiles.Add(node);
        //            }
        //        }
        //        foreach (WitchlightUnit unit in player.OwnedUnits)
        //        {
        //            foreach (INode node in GameData.Galaxy.GetVisibleCells(unit.Location, unit.Vision).Where(_ => !VisibleTiles.Contains(_)))
        //            {
        //                VisibleTiles.Add(node);
        //            }
        //        }

        //        player.DiscoveredTiles = player.DiscoveredTiles.Union(VisibleTiles).ToList();
        //        player.VisibleTiles = VisibleTiles;
        //    }

        //    if(m_LastShaderRenderedPlayerID != GameData.CurrentPlayerID) { RefreshShaderData(); }
        //}

        public void Update()
        {

        }

        //private int m_LastShaderRenderedPlayerID = -1;

        //private void RefreshShaderData()
        //{
        //    //Adjust to the next Human Player control
        //    //Reset Shader to new player
        //    GameData.Galaxy.ResetVisibility();

        //    //Adjust to the next Human Player control
        //    //Reset Shader to new player
        //    IPlayer player = GameData.Players.FirstOrDefault(_ => _.ID == GameData.CurrentPlayerID);

        //    foreach (INode node in player.DiscoveredTiles)
        //    {
        //        node.MarkAsExplored();
        //    }
        //    GameData.Galaxy.IncreaseVisibility(player.VisibleTiles.ToList());

        //    m_LastShaderRenderedPlayerID = GameData.CurrentPlayerID;
        //}
    }
}