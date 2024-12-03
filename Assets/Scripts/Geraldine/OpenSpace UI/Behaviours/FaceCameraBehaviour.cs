using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Geraldine.OpenSpaceUI.Behaviours
{
    public class FaceCameraBehaviour : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            if (TheCamera == null)
                TheCamera = Camera.main;
        }

        public Camera TheCamera;

        // Update is called once per frame
        void Update()
        {
            transform.rotation = TheCamera.transform.rotation;
            transform.localScale = new Vector3((TheCamera.fieldOfView / 60), (TheCamera.fieldOfView / 60), (TheCamera.fieldOfView / 60));
        }
    }
}