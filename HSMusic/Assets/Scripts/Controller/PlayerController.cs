using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //이동
    public float moveSpeed = 3;
    Vector3 dir = new Vector3();
    Vector3 destPos = new Vector3();

    //큐브 회전
    public float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();
    public Transform fakeCube = null;
    public Transform realCube = null;

    //반동
    public float recoilPosY = 0.25f;
    public float recoilSpeed = 1.5f;
    bool canMove = true;

    //기타
    TimingManager theTimingManager;

    // Start is called before the first frame update
    private void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            //판정 체크
            if(canMove&&theTimingManager.CheckTiming())
            {
                StartAction();
        
            }
        }
    }

    void StartAction()
    {
        //방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"),0,Input.GetAxisRaw("Horizontal"));

        //이동 목표값 계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        //회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());
    }

    IEnumerator MoveCo()
    {
        canMove = false;
        while(Vector3.SqrMagnitude(transform.position-destPos)>=0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;

        }
        transform.position = destPos;
        canMove = true;
    }

    IEnumerator SpinCo()
    {
        while (Quaternion.Angle(realCube.rotation,destRot)>0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;

        }
        realCube.rotation = destRot;
    }

    IEnumerator RecoilCo()
    {
        while (realCube.position.y<recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;

        }
        while (realCube.position.y> 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;

        }
        realCube.localPosition = new Vector3(0,0,0);
    }

    
}
