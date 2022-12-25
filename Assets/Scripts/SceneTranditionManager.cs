using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンをロードする処理の責任を受け持つスクリプト
/// </summary>
public class SceneTranditionManager : SingletonMonoBehaviour<SceneTranditionManager>
{
   public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}