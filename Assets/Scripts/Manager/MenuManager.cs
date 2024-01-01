using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Animator menuAnimator;

    private bool ispressAnyKey = true;

    [SerializeField]
    private AudioClip confirmClip;

    [SerializeField]
    private SceneName nextScene = SceneName.Defense;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ispressAnyKey)
        {
            if (Input.anyKey)
            {
                menuAnimator.SetTrigger("onPress");
                AudioManager.Instance.PlaySoundEffect(confirmClip);

                ispressAnyKey = false;
            }
        }
    }

    public void OnClickStoryMode()
    {
        AudioManager.Instance.PlaySoundEffect(confirmClip);
        LoadSceneManager.Instance.LoadScene(nextScene);
    }

    public void OnClickMultiplayer()
    {
        Debug.Log("Multiplayer Not Available");
        AudioManager.Instance.PlaySoundEffect(confirmClip);
        // SceneManager.Instance.LoadScene(nextScene);
    }

    public void OnClickQuit()
    {
        AudioManager.Instance.PlaySoundEffect(confirmClip);
        Application.Quit();
    }
}
