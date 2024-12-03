using TMPro;
using UnityEngine;

namespace Geraldine._4XEngine.Localization
{

    [RequireComponent(typeof(TMP_Text))]
    public class LocalizeTextBehaviour : BaseLocalizeBehaviour
    {

        #region Private Fields

        private TMP_Text text;

        #endregion



        #region Public Methods

        /// <summary>
        /// Update the value of the Text we are attached to.
        /// </summary>
        public override void UpdateLocale()
        {
            if (!text) return; // catching race condition
            Locale.GetFormattedLocalizedTextBody(localizationKey);
        }

        #endregion Public Methods

        #region Private Methods

        // Use this for initialization
        protected override void Start()
        {
            text = GetComponent<TMP_Text>();
            base.Start();
        }

        #endregion Private Methods
    }
}
