using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSounds : MonoBehaviour
{
    [SerializeField] GameObject player;
    Camera cam => Camera.main;
    float dist;

    void Start()
    {
        if(cam)
        {
            AkSoundEngine.PostEvent("Play_Aliens", cam.gameObject);
        }
    }

    void Update()
    {
        if(player)
        {
            dist = Vector3.Distance(gameObject.transform.position, player.transform.position);
            AkSoundEngine.SetRTPCValue("DistanceFromPlayer", dist);
        }    
    }

    private void OnDestroy()
    {
        AkSoundEngine.PostEvent("Stop_Aliens", cam.gameObject);
    }
}
