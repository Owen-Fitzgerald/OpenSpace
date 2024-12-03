using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Geraldine._4XEngine.GraphTree.Interfaces;
using Geraldine.OpenSpaceUI.Behaviours;
using Geraldine._4XEngine.Factories;
using Geraldine._4XEngine.Initializers;
using System.Drawing;
using Geraldine._4XEngine.GraphTree;

namespace Geraldine.OpenSpaceUI.Controllers
{
	/// <summary>
	/// Component that applies UI commands to the hex map.
	/// Public methods are hooked up to the in-game UI.
	/// </summary>
	public class MapEditorUI : MonoBehaviour
	{
		static readonly int cellHighlightingId = Shader.PropertyToID(
			"_CellHighlighting");

		[SerializeField]
		Material terrainMaterial;

		[SerializeField]
		SaveLoadMenu SaveLoadMenu;

		[SerializeField]
		Toggle HeightEditorToggle, BiomeEditorToggle, RegionEditorToggle, FeatureEditorToggle, IsMagicRegionToggle;

		[SerializeField]
		TMP_InputField SeedInputField, CurrentRegionNameInputField;

		[SerializeField]
		TMP_Dropdown BiomeDropdown, MagicBiomeDropdown;

		[SerializeField]
		GameObject WorldEditorContent, BiomeEditorContent, RegionEditorContent, FeatureEditorContent, HamburgerMenu;

		[SerializeField]
		TMP_Text HeightEditorLabelTMP, VegetationEditorLabelTMP;

		//Brush
		int brushSize;
		bool isDrag;
		int previousCellIndex = -1;
		bool updatingUI;

		//Tile Editing
		int activeElevation;
		int activeTerrain;
		int activeVegetation;

		//Resource Editing

		//Feature Editing
		bool applyUrbanLevel;
		bool applyRoad;

        public void Start()
        {
			HexMapCamera.ValidatePosition();
		}

        //Save/Load Map
        public void OpenSaveLoad(bool save)
		{
			SaveLoadMenu.Open(save ? enLoadSaveMode.SaveMap : enLoadSaveMode.LoadMap);
		}


		public void ShowGrid(bool visible)
		{
			if (visible)
			{
				terrainMaterial.EnableKeyword("_SHOW_GRID");
			}
			else
			{
				terrainMaterial.DisableKeyword("_SHOW_GRID");
			}
		}

		public void OpenCloseHamburgerMenu(bool open)
		{
			HamburgerMenu.SetActive(open);
		}
	}
}