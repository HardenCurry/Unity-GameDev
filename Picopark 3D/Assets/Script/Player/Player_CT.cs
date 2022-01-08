using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public enum Player_Num
{
    P1,
    P2,
    P3,
    P4
}
public class Player_CT : MonoBehaviourPunCallbacks,IPunObservable
{
    Rigidbody rb;
    // RaycastHit hit;
    Camera mainCam;

    public int playerSum; //when player in sb head, playerSum++
    float height;
    float width;
    Vector3 vec;
    float originSpeed = 3.0f;
    public float originjumpPow = 5;
    public bool IsMove;
    public bool Isjump;
    public bool IsElevator;
    string s;
    [HideInInspector]
    public float x;

    public Player_Num P_num;
    LayerMask layer;
    public float speed;
    public float jumpPow = 5;
    PhotonView PV;

    //Photon
    Vector3 curPos;
    private void Awake()
    {
        mainCam = Camera.main;
        height = mainCam.orthographicSize; //half height
        width = height * mainCam.aspect;

        rb = GetComponent<Rigidbody>();
        s = P_num.ToString();
        layer = LayerMask.GetMask("Player");
        speed = originSpeed;
        playerSum = 1;

        if (gameObject.GetComponent<PhotonView>())
        {
            PV = GetComponent<PhotonView>();
            DontDestroyOnLoad(gameObject);
        }
        if(PV.IsMine) PV.RPC("P_Init",RpcTarget.AllBuffered,P_num);//Material
    }
    //void Update()
    //{
    //    //Move
    //    if (!PV || PV.IsMine)
    //    {
    //        x = Input.GetAxisRaw(s);
    //        if (x != 0)
    //        {
    //            IsMove = true;
    //            if (x == 1) transform.rotation = Quaternion.Euler(0, 0, 0);
    //            else if (x == -1) transform.rotation = Quaternion.Euler(0, 180, 0);
    //        }
    //        else IsMove = false;
    //        vec = Vector3.zero;
    //        vec.x = x;
    //        //Jump
    //        if (Input.GetButtonDown(s + "_Jump") && !Isjump)
    //        {
    //            Isjump = true;
    //            rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);
    //        }
    //        //Interactive UseRay(버그 생길 가능성때문에 배재)
    //        // Debug.DrawRay(transform.position - offset, transform.right * length, Color.cyan);
    //        // if (Physics.Raycast(transform.position - offset, transform.right, out hit, length))
    //        // {
    //        //     if (hit.collider.tag == "Player")
    //        //     {
    //        //         speed = 0;
    //        //     }
    //        // }
    //        // else
    //        // {
    //        //     speed = originSpeed;
    //        // }

    //        //Interactive constraint
    //        if (Physics.CheckBox(transform.position + new Vector3(.525f, 0, 0) * x, new Vector3(0.02f, 0.45f, 0.8f), Quaternion.identity, layer))
    //        {
    //            speed = 0;
    //            rb.velocity = new Vector3(0, rb.velocity.y, 0); //괜춘?
    //        }
    //        else speed = originSpeed;

    //        //Camera range
    //        float posx;
    //        // float posy;
    //        if (transform.position.x >= mainCam.transform.position.x + width)
    //        {
    //            posx = mainCam.transform.position.x + width;
    //            transform.position = new Vector3(posx, transform.position.y, 0);
    //        }
    //        else if (transform.position.x <= mainCam.transform.position.x - width)
    //        {
    //            posx = mainCam.transform.position.x - width;
    //            transform.position = new Vector3(posx, transform.position.y, 0);

    //        }
    //        // if (transform.position.y >= mainCam.transform.position.y + height)
    //        // {
    //        //     posy = mainCam.transform.position.y + height;
    //        // }
    //        // else if (transform.position.y <= mainCam.transform.position.y - height)
    //        // {
    //        //     posy = mainCam.transform.position.y - height;
    //        // }

    //        //respawn when falling
    //        if (transform.position.y <= -5)
    //        {
    //            transform.position = new Vector3(transform.position.x - 6, 7, 0);
    //        }
    //    }
    //}
    //public void Startspawn(Vector3 _spawnpos)
    //{
    //    transform.position = _spawnpos;
    //}
    //void FixedUpdate()
    //{
    //    if (!IsElevator) rb.MovePosition(rb.position + vec * Time.deltaTime * speed);
    //    else transform.position += vec * Time.deltaTime * speed;
    //}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Door"))
    //    {
    //        if (other.gameObject.GetComponent<Door>().IsOpen)
    //        {
    //            if (Input.GetButton(s + "_Jump"))
    //            {
    //                Scene_Mng.Instance.ClearStageCt();
    //                gameObject.SetActive(false);
    //            }
    //        }
    //    }
    //}
    #region Photon
    void Update()
    {
        //Move
        if (PV.IsMine)
        {
            mainCam = Camera.main;

            x = Input.GetAxisRaw(s);
            if (x != 0)
            {
                IsMove = true;
                PV.RPC("Turnaround", RpcTarget.AllBuffered, x);
            }
            else IsMove = false;

            vec = Vector3.zero;
            vec.x = x;

            //Jump
            if (Input.GetButtonDown(s + "_Jump") && !Isjump)
            {
                Isjump = true;
                PV.RPC("Jump", RpcTarget.AllBuffered);
            }

            //Interactive constraint
            if (Physics.CheckBox(transform.position + new Vector3(.525f, 0, 0) * x, new Vector3(0.02f, 0.45f, 0.8f), Quaternion.identity, layer))
            {
                speed = 0;
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
            else speed = originSpeed;

            //Camera range
            float posx;
            if (transform.position.x >= mainCam.transform.position.x + width)
            {
                posx = mainCam.transform.position.x + width;
                transform.position = new Vector3(posx, transform.position.y, 0);
            }
            else if (transform.position.x <= mainCam.transform.position.x - width)
            {
                posx = mainCam.transform.position.x - width;
                transform.position = new Vector3(posx, transform.position.y, 0);
            }

            //respawn when falling
            if (transform.position.y <= -5)
            {
                transform.position = new Vector3(transform.position.x - 6, 7, 0);
            }
        }
        //!PV.IsMine
        else if ((transform.position - curPos).sqrMagnitude >= 50) transform.position = curPos;
        else
        {
            transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
        }
    }
    //Move
    void FixedUpdate() 
    {
        if (!IsElevator) rb.MovePosition(rb.position + vec * Time.deltaTime * speed);
        else transform.position += vec * Time.deltaTime * speed;
    }
    #endregion

    [PunRPC]
    void Turnaround(float _x)
    {
        if (_x == 1) transform.rotation = Quaternion.Euler(0, 0, 0);
        else if(_x==-1) transform.rotation= Quaternion.Euler(0, 180, 0);
    }
    [PunRPC]
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpPow, ForceMode.Impulse);     
    }
    [PunRPC]
    void P_Init(Player_Num _num)
    {
        P_num = _num;
        transform.GetChild(0).GetComponent<MeshRenderer>().material = Resources.Load(_num.ToString()) as Material;
    }
    public void Startspawn(Vector3 _spawnpos)
    {
        transform.position = _spawnpos;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (other.gameObject.GetComponent<Door>().IsOpen)
            {
                if (Input.GetButton(s + "_Jump"))
                {
                    Scene_Mng.Instance.ClearStageCt();
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            curPos = (Vector3)stream.ReceiveNext();
        }
    }
}
