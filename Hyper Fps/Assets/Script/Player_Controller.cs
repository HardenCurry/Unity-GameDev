using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class Player_Controller : MonoBehaviourPunCallbacks, IDamagable
{
    [SerializeField] Image healthbarImage;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] Item[] items;
    int itemIndex;
    int previousItemIndex = -1;
    float verticalLookRotation;
    bool grounded;
    const float maxHelath = 100f;
    float currentHealth = maxHelath;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;
    Rigidbody rb;
    PhotonView PV;
    Player_Manager playermanager;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();

        playermanager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<Player_Manager>();
    }
    private void Start()
    {
        if (PV.IsMine)
        {
            EquipItem(0);
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            Destroy(UI);
        }
    }
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        Move();
        Jump();
        Look();

        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString())) //*
            {
                EquipItem(i);
                break;
            }
        }
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {   //up
            EquipItem((itemIndex + 1) % items.Length);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {   //down
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        }
        if (transform.position.y <= -10f)
        {
            Die();
        }
    }
    void Move()
    {
        //1
        // Vector3 moveDir = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        // moveDir.Normalize();
        //2
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed),
        ref smoothMoveVelocity, smoothTime);
    }
    void FixedUpdate() //physics&movement
    {
        if (!PV.IsMine)
        {
            return;
        }
        //1
        // rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
        //2
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(transform.up * jumpForce);
        }
    }
    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
        // cameraHolder.transform.localRotation = Quaternion.Euler(-verticalLookRotation, 0, 0);
    }
    void EquipItem(int _Index)
    {
        if (previousItemIndex == _Index)
        {
            return;
        }
        itemIndex = _Index;
        items[itemIndex].itemGameObject.SetActive(true);
        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
        }
        previousItemIndex = itemIndex;

        if (PV.IsMine) //?
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }

    public void SetGrondedState(bool _grounded)
    {
        grounded = _grounded;
    }
    public void TakeDamage(float damamge)  //interface ++ runs on shooter's computer
    {
        PV.RPC("RPC_TakeDamage", RpcTarget.All, damamge);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage) //runs on everyones' computer
    {
        if (!PV.IsMine) return;
        Debug.Log("took damage: " + damage);

        currentHealth -= damage;

        healthbarImage.fillAmount = currentHealth / maxHelath;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        playermanager.Die();
    }
}
