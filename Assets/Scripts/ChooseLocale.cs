using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChooseLocale : MonoBehaviour
{
    public void Choose(int index)
    {
        Initialize();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    private IEnumerator Initialize()
    {
        yield return LocalizationSettings.InitializationOperation;
    }
}
