using UnityEngine;
using System.Collections;

public class Meniu : MonoBehaviour
{
    public Texture2D buttonimg=null;
    public PlayerBehaviour player;
    public void StartGame()
    {
        Debug.Log("Sttarted");
        player.stopped = false;
    }
  /*  {
        if(GUI.Button(new Rect(Screen.width/2  , Screen.height/2 ,Screen.width/2,Screen.height/2), ""))
        {
            player.stopped = false;
        }
*/
    }
