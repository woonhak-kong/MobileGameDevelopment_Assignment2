using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlaySceneUIManager : MonoBehaviour
{
    public GameObject GameoverPannel;

    public void OnClickGotoMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void EnableGameoverPannel()
    {
        GameoverPannel.SetActive(true);
    }
}
