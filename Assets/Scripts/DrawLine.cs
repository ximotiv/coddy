using UnityEngine;
using System.Collections.Generic;

public class DrawLine : MonoBehaviour
{
    private LineRenderer _line;
    private Camera _camera;

    private int _lineID = 0;
    private const int _lineLimit = 50;
    private Vector3[] _posLine = new Vector3[_lineLimit];
    private List<Vector2> _newVerticies = new List<Vector2>();

    private float _maxMouseDistance = 0.3f;

    void Start()
    {
        _camera = Camera.main;
        _line = GetComponent<LineRenderer>();
    }
    private void OnMouseDrag()
    {
        if(_lineID < _lineLimit-1)
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            if (_lineID > 0)
            {
                float distance = Vector2.Distance(_posLine[_lineID-1], mousePos);
                if (distance > 0.15f && distance < _maxMouseDistance) AddLineCount(mousePos);
                else if (distance >= _maxMouseDistance)
                {
                    if(_posLine[_lineID - 1].x < mousePos.x)
                    {
                        mousePos = new Vector3(_posLine[_lineID - 1].x + _maxMouseDistance, _posLine[_lineID - 1].y, 0f);
                    }
                    else
                    {
                        mousePos = new Vector3(_posLine[_lineID - 1].x - _maxMouseDistance, _posLine[_lineID - 1].y, 0f);
                    }
                    AddLineCount(mousePos);
                }
            }
            else AddLineCount(mousePos);
        }
    }
    private void OnMouseDown()
    {
        ResetLine();
    }
    private void OnMouseUp()
    {
        if (_lineID > 1)
        {
            float distance = Vector2.Distance(_posLine[0], _posLine[_lineID - 1]);
            if(distance < 0.8f)
            {
                if (_posLine[0].x < _posLine[_lineID - 1].x)
                {
                    AddLineCount(new Vector3(_posLine[_lineID - 1].x + _maxMouseDistance + 5, _posLine[_lineID - 1].y, 0f));
                }
                else
                {
                    AddLineCount(new Vector3(_posLine[_lineID - 1].x - (_maxMouseDistance + 5), _posLine[_lineID - 1].y, 0f));
                }
                //Debug.Log("Дополнил " + lineID);
            }
            EdgeCollider2D col = gameObject.AddComponent<EdgeCollider2D>();
            for (int i = 0; i < _lineID - 1; i++) _newVerticies.Add(_posLine[i]);
            col.points = _newVerticies.ToArray();
            col.edgeRadius = 0.04f;
            _lineID = 0;
        }
        else _lineID = 0;
    }
    public void ResetLine()
    {
        Destroy(GetComponent<EdgeCollider2D>());
        //transform.position = new Vector3(transform.position.x, 0f, 0f);
        //transform.rotation = Quaternion.Euler(Vector3.forward * 360f);
        _newVerticies.Clear();
        _lineID = 0;
        _line.positionCount = 0;
        Debug.Log("reset");
    }
    private void AddLineCount(Vector2 mousePos)
    {
        _posLine[_lineID] = mousePos;
        _posLine[_lineID].z = 0;
        _line.positionCount = _lineID;
        _line.SetPositions(_posLine);
        _lineID++;
        Debug.Log("resuu");
    }
}
