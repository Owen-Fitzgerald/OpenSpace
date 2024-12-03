using UnityEngine;
using UnityEngine.UI;

namespace Geraldine._4XEngine.Localization
{
    [RequireComponent(typeof(Image))]
    public class LocalizeImageBehaviour : MonoBehaviour
    {
        #region Public Fields

        public string localizationKey;

        #endregion Public Fields

        #region Private Fields

        private const string STR_LOCALIZATION_PREFIX = "localization/UI/";

        private Image image;

        #endregion Private Fields

        #region Public Methods

        /// <summary>
        /// This is to set the Sprite according to the selected language by code. It update all the Image
        /// components in the scene. This MUST be called AFTER the language has been set in Localize class.
        /// </summary>
        public static void SetCurrentLanguage()
        {
            LocalizeImageBehaviour[] allTexts = GameObject.FindObjectsByType<LocalizeImageBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID);
            for (int i = 0; i < allTexts.Length; i++)
                allTexts[i].UpdateLocale();
        }

        /// <summary>
        /// Update the Sprite of the Image we are attached to. It has a 100ms delay to allow Start operations.
        /// </summary>
        public void UpdateLocale()
        {
            if (!image) return; // catching race condition
            Invoke("_updateLocale", 0.1f);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Update the Sprite of the Image we are attached to. If language has not been set yet try again
        /// in 100ms.
        /// </summary>
        private void _updateLocale()
        {
            UpdateLocale();
        }

        private void Start()
        {
            image = GetComponent<Image>();
            UpdateLocale();
        }

        #endregion Private Methods
    }
}
