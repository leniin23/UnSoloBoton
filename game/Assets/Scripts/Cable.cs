using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour
{
    private Transform origin;

    private Transform head;
    private Rigidbody rigidbodyHead;

    private LineRenderer lineRenderer;
    public Color color;
    private bool isConnected;
    
    // Start is called before the first frame update
    void Start()
    {
        head = transform.Find("Cable Head");
        origin = transform.Find("Cable Origin");
        rigidbodyHead = head.GetComponent<Rigidbody>();
        //lineRenderer = transform.AddComponent<LineRenderer>();
        lineRenderer = transform.GetComponent<LineRenderer>();
        lineRenderer.endColor = color;
        lineRenderer.startColor = color;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        head.GetComponent<MeshRenderer>().material = lineRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        //lineRenderer.SetPositions(new Vector3[]{origin.position, head.position});
        Restructure();
        if(!isConnected) PullHead();
    }

    private void PullHead()
    {
        var dir = origin.position - head.position;
        if(dir.magnitude > 0.8)
        {
            dir = dir.normalized;
            rigidbodyHead.AddForce(dir * (Time.deltaTime * 10f), ForceMode.Impulse);
        }
        else
        {
            isConnected = true;
            rigidbodyHead.isKinematic = true;
            rigidbodyHead.Move(origin.position, Quaternion.Euler(new Vector3(0,0,90)));
        }
    }
    
    private void Restructure()
    {
        var headPosition = head.position;
        var originPosition = origin.position;
        var distance = new Vector2(headPosition.x - originPosition.x, headPosition.y - originPosition.y);

        var nPoints = Mathf.CeilToInt(distance.magnitude*10) * 5;
        //var points = new Vector3()[nPoints];
        lineRenderer.positionCount = nPoints;
        for (int i = 0; i < nPoints; i++)
        {
            var progress = i / (float) (nPoints-1);
            var x = Mathf.Lerp(originPosition.x, headPosition.x, progress);
            var t = x - originPosition.x;
            var y = Mathf.Cos(t/distance.x*Mathf.PI+Mathf.PI/2f)*distance.magnitude/10f+t/distance.x*distance.y + originPosition.y;
            lineRenderer.SetPosition(i,new Vector3(x,y,0));
        }
    }

    public void Connect(Vector3 position)
    {
        Debug.Log("Connected");
        rigidbodyHead.isKinematic = true;
        head.position = position;
        isConnected = true;
    }

    public Color GetColor()
    {
        return color;
    }

    public void Grab()
    {
        rigidbodyHead.useGravity = false;
        rigidbodyHead.isKinematic = false;
        isConnected = true;
    }

    public void Release()
    {
        rigidbodyHead.useGravity = true;
        rigidbodyHead.isKinematic = false;
        isConnected = false;
        rigidbodyHead.WakeUp();
    }

    public void Move(Vector3 pos)
    {
            rigidbodyHead.Move(pos, Quaternion.identity);
        if (!rigidbodyHead.SweepTest(pos - head.position, out var hit))
        {
        }
        //head.position = new Vector3(pos.x,pos.y,head.position.z);
    }
}
