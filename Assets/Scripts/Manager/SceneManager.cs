using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneName : byte
{
    Menu,
    Defense,
    Defeated,
    UndefenseNorth,
    UndefenseEast,
    UndefenseSouth,
    UndefenseWest,
    TheEnd
};

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public void LoadScene(SceneName sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad.ToString());
    }
}
