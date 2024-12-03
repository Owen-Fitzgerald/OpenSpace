using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Geraldine.OpenSpaceGame.Interfaces;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Util;
using Geraldine._4XEngine.GraphTree;

namespace Geraldine.OpenSpaceGame.GameFlow
{

    public class Game : IGame
    {

        public Galaxy Galaxy { get => GameObject.Find(Galaxy.GAME_OBJECT_NAME).GetComponent<Galaxy>(); }
        public int CurrentPlayerID { get => m_CurrentPlayer; set => m_CurrentPlayer = value; }
        public int CurrentTurn { get => m_CurrentTurn; }
        public IList<IPlayer> Players { get => m_Players; }

        public Game(int NumPlayers = 2)
        {
            for (int i = 0; i < NumPlayers; i++)
            {
                enPlayerController controller = enPlayerController.AI;
                if (i == 0) { controller = enPlayerController.Local; }
                Players.Add(new Player(i, controller));
            }

            m_CurrentPlayer = 0;
            m_CurrentTurn = 1;
        }


        public void NextTurn()
        {
            m_CurrentTurn += 1;
        }

        public void Save(BinaryWriter Writer)
        {
            Galaxy.Save(Writer);
        }

        public void Load(BinaryReader reader, int header)
        {
            Galaxy.Load(reader, header);
        }

        private IList<IPlayer> m_Players = new List<IPlayer>();
        private int m_CurrentPlayer;
        private int m_CurrentTurn;
    }
}
