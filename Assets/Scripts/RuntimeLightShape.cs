using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(SpriteShapeController))]
[RequireComponent(typeof(CustomCollider2D))]
public class RuntimeLightShape : MonoBehaviour
{
    [field:SerializeField]
    public float lightRange { get; private set; }
    [field:SerializeField]
    public float lightArc { get; private set; }
    [field:SerializeField]
    public int raycastCount { get; private set; }
    [field:SerializeField]
    public float shapePointsMinimumGap { get; private set; }
    
    private SpriteShapeController _spriteShapeController;
    private CustomCollider2D _customCollider2D;
    private readonly List<Vector3> _splinePoints = new();

    private void Start()
    {
        _spriteShapeController = GetComponent<SpriteShapeController>();
        _customCollider2D = GetComponent<CustomCollider2D>();
        
        var initialAngle = -lightArc / 2f;
        var angleStep = lightArc / (raycastCount - 1);
        for (int i = 0; i < raycastCount; i++)
        {
            var angle = initialAngle + i * angleStep;
            var angleRad = angle * Mathf.Deg2Rad;
            var direction = new Vector3(-Mathf.Sin(angleRad), Mathf.Cos(angleRad));
            _splinePoints.Add(direction);
        }
    }

    private void FixedUpdate()
    {
        var initialAngle = transform.eulerAngles.z - lightArc / 2f;
        var angleStep = lightArc / (raycastCount - 1);
        Vector3 previousPoint = Vector3.zero;
        var pointsList = new List<Vector2>();

        _spriteShapeController.spline.Clear();
        _spriteShapeController.spline.InsertPointAt(0, Vector3.zero);
        _spriteShapeController.spline.SetHeight(0, 0f);
        _spriteShapeController.spline.SetCorner(0, true);
        
        var j = 1;
        for (int i = 0; i < raycastCount; i++)
        {
            var angle = initialAngle + i * angleStep;
            var angleRad = angle * Mathf.Deg2Rad;
            var direction = new Vector2(-Mathf.Sin(angleRad), Mathf.Cos(angleRad));
            var hit = Physics2D.Raycast(transform.position, direction, lightRange, LayerMask.GetMask("Wall"));
            var distance = hit.collider != null ? hit.distance : lightRange;
            var nextPoint = distance * _splinePoints[i];

            if (Vector3.Distance(previousPoint, nextPoint) < shapePointsMinimumGap)
            {
                continue;
            }

            _spriteShapeController.spline.InsertPointAt(j, nextPoint);
            pointsList.Add(nextPoint);
            _spriteShapeController.spline.SetHeight(j, 0f);
            _spriteShapeController.spline.SetCorner(j, true);

            previousPoint = nextPoint;
            j++;
        }

        if (pointsList.Count < 2)
        {
            return;
        }

        var physicsShapeGroup2D = new PhysicsShapeGroup2D();
        for (int i = 0; i < pointsList.Count - 1; i++)
        {
            var tempList = new List<Vector2> { Vector2.zero, pointsList[i], pointsList[i + 1] };
            physicsShapeGroup2D.AddPolygon(tempList);
        }

        _customCollider2D.ClearCustomShapes();
        _customCollider2D.SetCustomShapes(physicsShapeGroup2D);
    }
}