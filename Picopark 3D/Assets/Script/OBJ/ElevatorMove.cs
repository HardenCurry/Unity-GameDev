using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ElevatorMove : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public TextMeshPro playercount_tmp;
    public float moveSpeed;
    public Transform Elevator_Collider;
    public int playerSum;
    public int playerCurrentCt;
    public int playerCurrentCtSum;
    public LayerMask layer;
    float updatedPosY;
    void Start()
    {
        playerSum = 2;  //매니저에서 값 받기
        playerCurrentCt = 0;
        UpdatePlayerCount();
        updatedPosY = transform.position.y;
    }

    void Update()
    {
        if (playerCurrentCt == playerSum)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos.position, moveSpeed * Time.deltaTime);
            updatedPosY = transform.position.y - updatedPosY;
        }
        else if (playerCurrentCt < playerSum)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos.position, moveSpeed * Time.deltaTime);
            updatedPosY = transform.position.y - updatedPosY;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Elevator_Collider.position, new Vector3(5, 0.1f, 1));
    }
    void UpdatePlayerCount()
    {
        playercount_tmp.text = (playerSum - playerCurrentCt).ToString();
    }
    public void RecalculatePlayerCt()
    {
        Collider[] colliders = Physics.OverlapBox(Elevator_Collider.position, new Vector3(5, 0.1f, 1), Quaternion.identity, layer);
        playerCurrentCtSum = 0;
        if (colliders != null)
        {
            foreach (Collider coll in colliders)
            {
                playerCurrentCtSum += coll.transform.parent.GetComponent<Player_CT>().playerSum;
            }
        }
        playerCurrentCt = playerCurrentCtSum;
        UpdatePlayerCount();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GroundCheck"))
        {
            other.transform.parent.GetComponent<Player_CT>().IsElevator = true;
            other.transform.parent.SetParent(transform);
            playerCurrentCt += other.transform.parent.GetComponent<Player_CT>().playerSum;
            UpdatePlayerCount();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GroundCheck"))
        {
            other.transform.parent.GetComponent<Player_CT>().IsElevator = false;
            other.transform.parent.SetParent(null);
            playerCurrentCt -= other.transform.parent.GetComponent<Player_CT>().playerSum;
            UpdatePlayerCount();
        }
    }
}
