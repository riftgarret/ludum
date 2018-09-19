using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Build
{
    public class HelloWorld : MonoBehaviour
    {
        [SerializeField]
        private float rate = 0.5f;

        [SerializeField]
        private float radius = 5f;

        public void Update()
        {
            //gameObject.transform.position.x = radius * Math.Sin(rate * Time.fixedTime);
        }
    }
}
