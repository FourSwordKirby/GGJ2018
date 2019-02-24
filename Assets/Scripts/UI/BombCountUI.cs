using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCountUI : MonoBehaviour {

    public ShmupPlayer player;
    public List<Image> bombMarkers;

    private int lastBombCount;

    private void Start()
    {
        if (player == null)
            player = ShmupGameManager.instance.player;
        lastBombCount = (int)player.bombCharge;
    }

    private void Update()
    {
        for(int i = 0; i < bombMarkers.Count; i++)
        {
            float playerCharge = (float)Mathf.Max(player.bombCharge - i * ShmupPlayer.MIN_BOMB_CHARGE, 0.0f) / (float)ShmupPlayer.MIN_BOMB_CHARGE;
            bombMarkers[i].transform.localScale = Mathf.Min(1.0f, playerCharge) * Vector3.one;
            bombMarkers[i].GetComponent<Animator>().SetFloat("FlashTiming", Time.time % 1.0f);

            if (player.bombCharge >= (i+1)*ShmupPlayer.MIN_BOMB_CHARGE)// && (i+1) > lastBombCount)
            {
                bombMarkers[i].GetComponent<Animator>().SetBool("Available", true);
            }
            else
                bombMarkers[i].GetComponent<Animator>().SetBool("Available", false);
        }
        lastBombCount = (int)player.bombCharge;
    }
}
