using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [Header("Input systems:")]
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float touchRadious;

    [Header("Sprite")]
    [SerializeField] private Sprite SpriteX;
    [SerializeField] private Sprite Sprite0;

    [Header("Color")]
    [SerializeField] private Color colorX;
    [SerializeField] private Color color0;


    public UnityAction<Mark, Color> onWinAction;
    private LineRenderer lineRenderer;

    [SerializeField] private Mark[] marks;
    private Camera cam;
    private Mark currentMark;

    private bool canPlay;

    private int markCount = 0;

    void Start()
    {
        cam = Camera.main;
        currentMark = Mark.X;
        marks = new Mark[9];
        canPlay = true;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

    }
    void Update()
    {
        if (canPlay && Input.GetMouseButtonUp(0))
        {
            Vector2 touchPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapCircle(touchPoint, touchRadious, layerMask);
            if (hit)
            {
                HitBox(hit.GetComponent<Box>());
            }
        }
    }

    private void DrawLine(int i, int k)
    {
        lineRenderer.SetPosition(0, transform.GetChild(i).position);
        lineRenderer.SetPosition(1, transform.GetChild(k).position);
        Color color = GetColor();
        color.a = 0.3f;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.enabled = true;
    }

    public void HitBox(Box box)
    {
        if (!box.isMarked)
        {
            marks[box.index] = currentMark;
            box.SetAsMarked(GetSprite(), currentMark, GetColor());
            markCount++;
            bool win = CheckIfWin();
            if (win)
            {
                if (onWinAction != null)
                {
                    onWinAction.Invoke(currentMark, GetColor());
                }
                Debug.Log(currentMark.ToString() + "Wins");
                canPlay = false;
            }
            if (!win)
            {
                if (markCount == 9)
                {
                    if (onWinAction != null)
                    {
                        onWinAction.Invoke(Mark.None, Color.white);
                    }
                    Debug.Log("Nobody win");
                    canPlay = false;
                }
            }
            SwitchPlayer();
        }
    }

    private bool CheckIfWin()
    {
        return
        AreBoxesMatch(0, 1, 2) || AreBoxesMatch(3, 4, 5) || AreBoxesMatch(6, 7, 8) || AreBoxesMatch(0, 3, 6) || AreBoxesMatch(1, 4, 7) || AreBoxesMatch(2, 5, 8) ||
        AreBoxesMatch(0, 4, 8) || AreBoxesMatch(2, 4, 6);
    }

    private bool AreBoxesMatch(int i, int j, int k)
    {
        Mark m = currentMark;
        bool matched = (marks[i] == m && marks[j] == m && marks[k] == m);

        if (matched)
        {
            DrawLine(i, k);
        }
        return matched;
    }

    private void SwitchPlayer()
    {
        currentMark = (currentMark == Mark.X) ? Mark.o : Mark.X;
    }

    private Color GetColor()
    {
        return (currentMark == Mark.X) ? colorX : color0;
    }
    private Sprite GetSprite()
    {
        return (currentMark == Mark.X) ? SpriteX : Sprite0;
    }




}
