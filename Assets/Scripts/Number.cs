using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    public int num = 2;
    public GameControll gameControll;
    private Image image;
    private bool isDestorying = false;
    private void Start()
    {
        image = GetComponent<Image>();
        switch (num)
        {
            case 2:
                image.overrideSprite = Resources.Load("Textures/2", typeof(Sprite)) as Sprite;
                break;
            case 4:
                image.overrideSprite = Resources.Load("Textures/4", typeof(Sprite)) as Sprite;
                break;
            case 8:
                image.overrideSprite = Resources.Load("Textures/8", typeof(Sprite)) as Sprite;
                break;
            case 16:
                image.overrideSprite = Resources.Load("Textures/16", typeof(Sprite)) as Sprite;
                break;
            case 32:
                image.overrideSprite = Resources.Load("Textures/32", typeof(Sprite)) as Sprite;
                break;
            case 64:
                image.overrideSprite = Resources.Load("Textures/64", typeof(Sprite)) as Sprite;
                break;
            case 128:
                image.overrideSprite = Resources.Load("Textures/128", typeof(Sprite)) as Sprite;
                break;
            case 256:
                image.overrideSprite = Resources.Load("Textures/256", typeof(Sprite)) as Sprite;
                break;
            case 512:
                image.overrideSprite = Resources.Load("Textures/251", typeof(Sprite)) as Sprite;
                break;
            case 1024:
                image.overrideSprite = Resources.Load("Textures/1024", typeof(Sprite)) as Sprite;
                break;
            case 2048:
                image.overrideSprite = Resources.Load("Textures/2048", typeof(Sprite)) as Sprite;
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        if (transform.position.x <= -200 && !isDestorying)
        {
            isDestorying = true;
            Invoke("_Destroy", 0.5f);
        }
    }
    private void _Destroy()
    {
        Destroy(gameObject);
    }
}
