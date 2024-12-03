using UnityEngine;
using Geraldine.OpenSpaceGame;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine._4XEngine.Contracts;
using Geraldine._4XEngine.Loaders;
using Geraldine._4XEngine.Factories;
using Geraldine._4XEngine.Initializers;
using Geraldine._4XEngine.GraphTree;

namespace Geraldine.OpenSpaceUI.Behaviours
{
	/// <summary>
	/// Component that applies actions from the new map menu UI to the hex map.
	/// Public methods are hooked up to the in-game UI.
	/// </summary>
	public class NewMapMenu : MonoBehaviour
	{
		bool generateMaps = true;

		public void ToggleMapGeneration(bool toggle) => generateMaps = toggle;

		public void Open()
		{
			gameObject.SetActive(true);
			HexMapCamera.Locked = true;
		}

		public void Close()
		{
			gameObject.SetActive(false);
			HexMapCamera.Locked = false;
		}

		public void CreateTinyMap() => CreateMap(GalaxySizeLoader.GetContractByReference("gs_tiny"));

		public void CreateSmallMap() => CreateMap(GalaxySizeLoader.GetContractByReference("gs_small"));

		public void CreateMediumMap() => CreateMap(GalaxySizeLoader.GetContractByReference("gs_medium"));

		public void CreateLargeMap() => CreateMap(GalaxySizeLoader.GetContractByReference("gs_large"));

		public void CreateEpicMap() => CreateMap(GalaxySizeLoader.GetContractByReference("gs_epic"));

		void CreateMap(GalaxySize size)
		{
			GalaxyInitializer init = new GalaxyInitializer()
			{
				Size = size,
				HyperlaneDensity = .25f
			};
            Galaxy newGalaxy = GalaxyFactory.GenerateGalaxy(init, 0);

			HexMapCamera.ValidatePosition();
			Close();
		}
	}
}