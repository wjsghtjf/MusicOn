using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool s_canPresskey = true;

    //이동
    bool btnClick = false;
    public float moveSpeed = 3;
    public Vector3 dir = new Vector3();
    public Vector3 destPos = new Vector3();

    //큐브 회전
    public float spinSpeed = 270;
    Vector3 rotDir = new Vector3();
    Quaternion destRot = new Quaternion();
    public Transform fakeCube = null;
    public Transform realCube = null;

    //반동
    public float recoilPosY = 0.25f;
    public float recoilSpeed = 1.5f;

    //기타
    TimingManager theTimingManager;
    CameraController theCam;
    Result theResult;
    NoteManager theNote;
   


    bool canMove = true;
    Vector3 originPos = new Vector3();
    bool isFalling = false;
    Rigidbody myRigid;



    // Start is called before the first frame update
    private void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>();
        theTimingManager = FindObjectOfType<TimingManager>();
        theCam = FindObjectOfType<CameraController>();
        myRigid = GetComponentInChildren<Rigidbody>();
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckFalling();

        //Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W)
        if (btnClick)
        {
            //판정 체크
            if (canMove&&s_canPresskey&&!isFalling)
            {
                Calc();
                if (theTimingManager.CheckTiming())
                {
                    StartAction();

                }
            }
            btnClick = false;
        }
    }

    void Calc()
    {
        //방향 계산
        //dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        //이동 목표값 계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        //회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;


    }


    void StartAction()
    {
       
        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());
        StartCoroutine(theCam.ZoomCam());
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

    void CheckFalling()
    {
        if(!isFalling&& canMove)
        {
            if(!Physics.Raycast(transform.position,Vector3.down,1.1f))
            {
                Falling();
            }

        }
    }

    void Falling()
    {
        isFalling = true;
        myRigid.useGravity = true;
        myRigid.isKinematic = false;
       
    }

    public void ResetFalling()
    {
        isFalling = false;
        myRigid.useGravity = false;
        myRigid.isKinematic = true;
        transform.position = originPos;
        realCube.localPosition = new Vector3(0, 0, 0);
        theResult.ShowResult();
        theNote.RemoveNote();
    }

    public void MoveUp()
    {
        btnClick = true;
        dir.Set(1, 0, 0);
    }
    public void MoveRight()
    {
        btnClick = true;
        dir.Set(0, 0, 1);
    }
    public void MoveLeft()
    {
        btnClick = true;
        dir.Set(0, 0, -1);
    }
    public void MoveDown()
    {
        btnClick = true;
        dir.Set(-1, 0, 0);

    }



}
