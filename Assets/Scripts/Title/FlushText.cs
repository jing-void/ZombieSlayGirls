using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushText : MonoBehaviour
{
    public Text pressSpace;
    float time;
    float speed = 3.5f;
    private void Start()
    {
        pressSpace = GetComponent<Text>();
    }
    void Update()
    {
        pressSpace.color = AlphaColor(pressSpace.color);
    }

    public Color AlphaColor(Color color)
    {
        time += speed * Time.deltaTime;
        color.a = Mathf.Sin(time);

        return color;
    }


    public void CharacterSelectScene()
    {

    }
}
