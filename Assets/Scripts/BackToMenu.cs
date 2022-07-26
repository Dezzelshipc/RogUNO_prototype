using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(SceneChange), 1e-6f);
    }

    void SceneChange()
    {
        gameObject.GetComponent<SceneChanger>().ChangeScene("MainMenu");
    }
}
