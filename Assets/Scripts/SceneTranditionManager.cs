using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �V�[�������[�h���鏈���̐ӔC���󂯎��X�N���v�g
/// </summary>
public class SceneTranditionManager : SingletonMonoBehaviour<SceneTranditionManager>
{
   public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}