using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpbarCtrl : MonoBehaviour
{
    // 체력 바 관련 변수
    Slider hpbar;
    private static float maxhp = 100;
    private static float curhp = 100;

    // Start is called before the first frame update
    void Start()
    {
        hpbar = GetComponent<Slider>();
        transform.Find("Fill Area").gameObject.SetActive(true);
        hpbar.value = (float)curhp / (float)maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.value = (float)curhp / (float)maxhp;
    }

   public static void HpControl() // 플레이어의 충돌 함수에서 호출할 수 있는 체력 감소, 게임오버 함수
    {
        if (curhp <= 10)
        {
            SceneManager.LoadSceneAsync("Main");
        }
        else
        {
            Debug.Log("충돌");
            curhp -= 10;
        }
    }

    public static void HpStar()
    {
        if (curhp < 100) { 
            curhp += 10;
         }
    }

    public static void Restart() // 재시작시 체력 초기화
    {
        curhp = 100;
    }
}
