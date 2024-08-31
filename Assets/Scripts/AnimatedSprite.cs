using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int frame;
 
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable(){
            Invoke(nameof(Animate), 0.0f);

    }

    private void onDisable(){
        CancelInvoke();
    }

    private void Animate(){

        frame++;
        if(frame >= sprites.Length){
            frame = 0;
        }

        if(sprites.Length > 0 && sprites[frame] != null){
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);

    }

}
