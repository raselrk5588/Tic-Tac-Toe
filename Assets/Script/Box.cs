using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int index;
    public Mark mark;
    public bool isMarked;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        index = transform.GetSiblingIndex();
        mark = Mark.None;
        isMarked = false;
    }

    public void SetAsMarked(Sprite sprite, Mark mark, Color color)
    {
        isMarked = true;
        this.mark = mark;
        spriteRenderer.color = color;
        spriteRenderer.sprite = sprite;
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
