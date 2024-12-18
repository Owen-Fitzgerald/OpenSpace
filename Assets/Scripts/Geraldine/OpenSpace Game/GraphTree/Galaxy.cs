﻿using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Units;
using Geraldine._4XEngine.Units.Interfaces;
using Geraldine.OpenSpaceGame;
using System;
using System.IO;
using UnityEngine;

namespace Geraldine._4XEngine.GraphTree
{
    public class Galaxy : NodeGraph<StarSystem>
    {
        public const string GAME_OBJECT_NAME = "GalaxyGameObject"; 

        #region Base Node Interactions

        public override void Occupy(IExistOnNodeGraph OccupyingUnit)
        {
            throw new NotImplementedException();
        }

        public override void Release(IExistOnNodeGraph ReleasingUnit)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Serialization

        public override void Save(BinaryWriter writer)
        {
            //Save Properties specific to this graph
            base.Save(writer);

            //Save children nodes
            writer.Write(Nodes.Count);
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].Save(writer);
            }
        }

        public override void Load(BinaryReader reader, int header)
        {
            //Load properties specific this this graph
            GameFileManager.CurrentLoadState = enLoadState.Day2;
            base.Load(reader, header);

            //load children nodes
            int nodeCount = reader.ReadInt32();
            for (int i = 0; i < nodeCount; i++)
            {
                nodes[i] = new StarSystem()
                { ParentGraph = this };
                nodes[i].Load(reader, header);
            }
        }

        #endregion
    }
}
