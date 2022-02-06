using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositionInMap : MonoBehaviour
{
    [SerializeField] private GameObject you;
    [SerializeField] private Image pointer; 
    public RectTransform _transform;
    public Vector2 topLeft = new Vector2(99f, -3f);
    public Vector2 bottomRight = new Vector2(-32, 115f);
    private Vector2 offset;
    private Vector2 mapDim;
    public float scaling;

    // Start is called before the first frame update
    void Start()
    {
        //_transform = GetComponentInParent<RectTransform>();
        mapDim = _transform.sizeDelta;


    }

    // Update is called once per frame
    void Update()
    { 
        scaling = (_transform.sizeDelta.y)/Mathf.Abs((topLeft-bottomRight).y);
        pointer.rectTransform.anchoredPosition = PointToMap(PositionTo2d(you.transform));
    }

    private Vector2 PositionTo2d(Transform position)
    {
        var vectorPos = position.position;
        return new Vector2(vectorPos.x, vectorPos.z);
    }

    private Vector2 PointToMap(Vector2 point)
    {

        var x = -(point - topLeft) * scaling;
        return x;
    }
}
