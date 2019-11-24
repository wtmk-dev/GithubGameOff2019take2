using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skinner : MonoBehaviour
{
   private List<Sprite> sprites;
   [SerializeField]
   private List<SpriteRenderer> spriteRenderers;

    public void Init(List<Sprite> sprites)
    {
        this.sprites = sprites;

        for(int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].sprite = sprites[i];
        }
    }
}
