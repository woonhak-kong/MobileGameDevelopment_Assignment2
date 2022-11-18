using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainSceneUIManager : MonoBehaviour
{
    public GameObject InstructionPannel;


    public void OnClickStart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickInstruction()
    {
        InstructionPannel.SetActive(true);

    }

    public void OnClickBack()
    {
        InstructionPannel.SetActive(false);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }

   
}
