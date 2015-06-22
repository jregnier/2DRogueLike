using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour
{
    public Sprite dmgSprite;
    public int hp = 4;

    private SpriteRenderer renderer;

    // Use this for initialization
    void Awake()
    {
        this.renderer = this.GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        renderer.sprite = dmgSprite;
        hp -= loss;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
