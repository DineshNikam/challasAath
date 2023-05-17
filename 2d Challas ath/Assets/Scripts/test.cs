using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class test : MonoBehaviour
    {

        // Use this for initialization

        public void runThis()
        {
            int num = 5;
            num = (num == 5) ? 8 : num;
            print(num);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}