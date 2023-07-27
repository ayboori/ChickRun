using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HpbarCtrl : MonoBehaviour
{
    // ü�� �� ���� ����
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

   public static void HpControl() // �÷��̾��� �浹 �Լ����� ȣ���� �� �ִ� ü�� ����, ���ӿ��� �Լ�
    {
        if (curhp <= 10)
        {
            SceneManager.LoadSceneAsync("Main");
        }
        else
        {
            Debug.Log("�浹");
            curhp -= 10;
        }
    }

    public static void HpStar()
    {
        if (curhp < 100) { 
            curhp += 10;
         }
    }

    public static void Restart() // ����۽� ü�� �ʱ�ȭ
    {
        curhp = 100;
    }
}
