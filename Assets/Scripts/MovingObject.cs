using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;
    public LayerMask blockingLayer;

    private BoxCollider2D box2d;
    private Rigidbody2D rigid2d;
    private float inverseMoveTime;

    // Use this for initialization
    protected virtual void Start()
    {
        box2d = this.GetComponent<BoxCollider2D>();
        rigid2d = this.GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = (Vector2)transform.position;
        Vector2 end = (Vector2)start + new Vector2(xDir, yDir);

        box2d.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        box2d.enabled = true;

        if (hit.transform == null)
        {
            this.StartCoroutine(this.SmoothMovement(end));
            return true;
        }

        return false;
    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rigid2d.position, end, inverseMoveTime * Time.deltaTime);
            rigid2d.MovePosition(newPosition);
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = this.Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
        {
            this.OnCantMove(hitComponent);
        }
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component;
}