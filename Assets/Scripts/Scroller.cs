using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    
    public float _scrollSpeed;
    [SerializeField]
    private GameObject background1, background2;

    [SerializeField]
    private float _textureWidth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var bg1Pos = background1.transform.position;
        var bg2Pos = background2.transform.position;

        
        background1.transform.position =
            new Vector3(bg1Pos.x - _scrollSpeed * Time.deltaTime, bg1Pos.y, bg1Pos.z);

        background2.transform.position = new Vector3(bg2Pos.x - _scrollSpeed * Time.deltaTime, bg1Pos.y, bg1Pos.z);
        
        if (bg1Pos.x < _textureWidth)
        {
            background1.transform.position = new Vector3(bg2Pos.x + Mathf.Abs(_textureWidth), bg1Pos.y, bg1Pos.z);
        }
        if (bg2Pos.x < _textureWidth)
        {
            background2.transform.position = new Vector3(bg1Pos.x + Mathf.Abs(_textureWidth), bg2Pos.y, bg2Pos.z);
        }
        
    }
}
