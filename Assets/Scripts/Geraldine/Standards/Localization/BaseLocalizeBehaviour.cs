using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Geraldine.HexEngine.Localization
{
    public abstract class BaseLocalizeBehaviour : MonoBehaviour
    {
        #region Public Fields

        public string localizationKey;

        #endregion Public Fields


        #region Public Properties


        #endregion Public Properties

        #region Private Properties


        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Update the value of the Text we are attached to.
        /// </summary>
        public abstract void UpdateLocale();

        protected virtual void Start()
        {
            // The first Text object getting here inits the CultureInfo data and loads the language file,
            // if any
            SetCurrentLanguage(Locale.PlayerLanguage);
            UpdateLocale();
        }

        /// <summary>
        /// This is to set the language by code. It also update all the Text components in the scene.
        /// </summary>
        /// <param name="language">The new language</param>
        public static void SetCurrentLanguage(SystemLanguage language)
        {
            Locale.CurrentLanguage = language;
            Locale.PlayerLanguage = language;
            LocalizeTextBehaviour[] allTexts = GameObject.FindObjectsOfType<LocalizeTextBehaviour>();
            for (int i = 0; i < allTexts.Length; i++)
                allTexts[i].UpdateLocale();
        }



        #endregion Public Methods

    }
}
