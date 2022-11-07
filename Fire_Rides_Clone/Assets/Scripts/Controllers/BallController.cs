using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class BallController : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Vector3 _grapplePoint;
        private LayerMask _layer;
        private SpringJoint _springJoint;
        private int maxDistance = 100;

        public Transform GrapStartPoint , Ball; 


        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            MouseActions();
        }
        private void LateUpdate()
        {
            DrawRope();
        }

        private void MouseActions()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                StopGrapple();
            }
        }

        void StartGrapple()
        {
            RaycastHit hit;
            if (Physics.Raycast(GrapStartPoint.position, GrapStartPoint.forward, out hit, maxDistance))
            {
                _grapplePoint = hit.transform.position;
                _springJoint = Ball.gameObject.AddComponent<SpringJoint>();
                _springJoint.autoConfigureConnectedAnchor = false;
                _springJoint.connectedAnchor = _grapplePoint;

                float distanceFromPoint = Vector3.Distance(GrapStartPoint.position, Ball.position);

                _springJoint.maxDistance = distanceFromPoint * 0.8f;
                _springJoint.minDistance = distanceFromPoint * 0.25f;

                _springJoint.spring = 2f;
                _springJoint.damper = 7f;
                _springJoint.massScale = 3f;

                _lineRenderer.positionCount = 2;
            }
        }

        void DrawRope()
        {
            if (!_springJoint) return;
            _lineRenderer.SetPosition(0,GrapStartPoint.position);
            _lineRenderer.SetPosition(1,_grapplePoint);
        }

        void StopGrapple()
        {
            _lineRenderer.positionCount = 0;
            Destroy(_springJoint);
        }
    }

}
