using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HpbarCtrl;

public class PlayerCtrl : MonoBehaviour
{
    // 컴포넌트를 캐시 처리할 변수
    private Transform tr;

    // 이동 속력 변수 (public으로 선언되어 인스펙터 뷰에 노출됨)
    private float moveSpeed = 5.0f;

    private Rigidbody rigid;
    private bool isJumping; // 이미 점프중인지 여부 체크
    private int jumpPower = 6;

    // 현재 이동 값 받아오는 변수
    float h;
    float v;

    // 애니메이션 적용을 위한 상태 정보
    public enum State
    {
        IDLE,
        WALK,
        RUN,
        JUMP
    }

    public State state = State.IDLE; // 캐릭터의 현재 상태, IDLE로 초기화
    private Animator anim;


    void Start()
    {
        // 컴포넌트를 추출해 변수에 대입
        tr = GetComponent<Transform>();

        //애니메이터 컴포넌트 할당
        anim = GetComponent<Animator>();

        anim.SetBool("walking", false);
        rigid = GetComponent<Rigidbody>(); // Rigidbody 컴포넌트 받아옴

        isJumping = false;

    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        // 전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(이동 방향 * 속력 * Time.deltaTime)
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

        Jump(); // 점프 함수
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