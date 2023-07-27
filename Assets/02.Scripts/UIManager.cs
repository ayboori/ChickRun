using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static HpbarCtrl;


public class UIManager : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadSceneAsync("run");
        HpbarCtrl.Restart();
    }
}