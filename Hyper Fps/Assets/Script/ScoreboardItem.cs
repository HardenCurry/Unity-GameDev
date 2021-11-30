using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;  //Player
public class ScoreboardItem : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text killText;
    public TMP_Text deathText;

    public void Initialize(Player player)
    {
        usernameText.text = player.NickName;
    }

}
