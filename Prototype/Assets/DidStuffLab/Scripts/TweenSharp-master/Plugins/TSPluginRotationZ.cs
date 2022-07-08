﻿using UnityEngine;

namespace TS
{
    public class TSPluginRotationZ : TSTransformPlugin
    {
        private readonly string PROPERTY_NAME = "rotationZ";

        public override float Value
        {
            get { return transform.rotation.eulerAngles.z; }
            set
            {
                Quaternion rotation = transform.localRotation;
                Vector3 euler = rotation.eulerAngles;
                euler.z = value;
                rotation.eulerAngles = euler;
                transform.localRotation = rotation;
            }
        }

        public override string PropertyName
        {
            get { return PROPERTY_NAME; }
        }
    }
}