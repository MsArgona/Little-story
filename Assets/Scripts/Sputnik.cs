using UnityEngine;

public class Sputnik : MonoBehaviour
{
    [SerializeField] private float speed;

    public bool IsMoving
    {
        get { return isMoving; }
        set { isMoving = value; }
    }
    private bool isMoving;

    private Vector2 min; //слева-внизу точка
    private Vector2 max; //справа-верху точка

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        isMoving = false;

        spriteRenderer = GetComponent<SpriteRenderer>();       
    }

    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x + GetComponent<SpriteRenderer>().sprite.bounds.extents.x + 2f;
        min.x = min.x - GetComponent<SpriteRenderer>().sprite.bounds.extents.x - 2f;
    }

    private void Update()
    {
        if (!isMoving)
        {
            spriteRenderer.color = new Color(255, 255, 255, 0);
            return;
        }          
        else
            spriteRenderer.color = new Color(255, 255, 255, 255);

        Vector2 position = transform.position;

        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        transform.position = position;

        if (transform.position.x < min.x)
        {
            isMoving = false;
        }     
    }

    public void SetStartPos(Transform pos)
    {
        transform.position = pos.position;
       
    }
}
