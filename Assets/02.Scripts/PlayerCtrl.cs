using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HpbarCtrl;

public class PlayerCtrl : MonoBehaviour
{
    // ������Ʈ�� ĳ�� ó���� ����
    private Transform tr;

    // �̵� �ӷ� ���� (public���� ����Ǿ� �ν����� �信 �����)
    private float moveSpeed = 5.0f;

    private Rigidbody rigid;
    private bool isJumping; // �̹� ���������� ���� üũ
    private int jumpPower = 6;

    // ���� �̵� �� �޾ƿ��� ����
    float h;
    float v;

    // �ִϸ��̼� ������ ���� ���� ����
    public enum State
    {
        IDLE,
        WALK,
        RUN,
        JUMP
    }

    public State state = State.IDLE; // ĳ������ ���� ����, IDLE�� �ʱ�ȭ
    private Animator anim;


    void Start()
    {
        // ������Ʈ�� ������ ������ ����
        tr = GetComponent<Transform>();

        //�ִϸ����� ������Ʈ �Ҵ�
        anim = GetComponent<Animator>();

        anim.SetBool("walking", false);
        rigid = GetComponent<Rigidbody>(); // Rigidbody ������Ʈ �޾ƿ�

        isJumping = false;

    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // �����¿� �̵� ���� ���� ���
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(�̵� ���� * �ӷ� * Time.deltaTime)
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime);
        

        if (h == 0.0 && v == 0.0 && !isJumping)
        {
            anim.SetBool("walking", false);
            anim.SetBool("jumping", false);
            state = State.IDLE;
        }
        else if(isJumping)
        {

            anim.SetBool("jumping", true);
            state = State.JUMP;
        }
        else
        {
            anim.SetBool("walking", true);
            anim.SetBool("jumping", false);
            state = State.WALK;

        }

        Jump(); // ���� �Լ�
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isJumping)
            {
                isJumping = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            }
            else
            {
                return;
            }
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("SGROUND"))
        {
            moveSpeed = 4.0f;
            jumpPower = 7;
        }
        else if (collision.collider.CompareTag("GROUND"))        
        {
            moveSpeed = 5.0f;
            jumpPower = 6;
        }
        else if (collision.collider.CompareTag("SSROUND"))
        {
            moveSpeed = 3.5f;
            jumpPower = 7;
        }

       if (collision.collider.CompareTag("GROUND")|| collision.collider.CompareTag("SGROUND")|| collision.collider.CompareTag("SSROUND"))
        {
            isJumping = false;
        }else if (collision.collider.CompareTag("HURDLE"))
        {
            HpbarCtrl.HpControl();
        }


    }
}