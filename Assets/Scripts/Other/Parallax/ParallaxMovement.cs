using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parallax
{
    public class ParallaxMovement : MonoBehaviour
    {
        Transform cam; // Main Camera
        Vector3 camStartPos;
        float distanceX;

        GameObject[] backgrounds;
        Material[] mat;
        float[] backSpeed;

        float farthestBack;

        [Range(0.01f, 1f)]
        public float parallaxSpeed;

        void Start()
        {
            cam = Camera.main.transform;
            camStartPos = cam.position;

            int backCount = transform.childCount;
            mat = new Material[backCount];
            backSpeed = new float[backCount];
            backgrounds = new GameObject[backCount];

            for (int i = 0; i < backCount; i++)
            {
                backgrounds[i] = transform.GetChild(i).gameObject;
                mat[i] = backgrounds[i].GetComponent<Renderer>().material;
            }

            BackSpeedCalculate(backCount);
        }

        void BackSpeedCalculate(int backCount)
        {
            for (int i = 0; i < backCount; i++) // tìm background xa nhất
            {
                if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
                {
                    farthestBack = backgrounds[i].transform.position.z - cam.position.z;
                }
            }

            for (int i = 0; i < backCount; i++) // tính tốc độ
            {
                backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
            }
        }

        private void LateUpdate()
        {
            // chỉ parallax theo trục X
            distanceX = cam.position.x - camStartPos.x;

            // background đi theo cả X và Y của camera
            transform.position = new Vector3(
                cam.position.x - 1,   // giữ gốc X theo camera
                cam.position.y,       // cập nhật theo Y camera
                transform.position.z  // giữ nguyên Z
            );

            for (int i = 0; i < backgrounds.Length; i++)
            {
                float speed = backSpeed[i] * parallaxSpeed;
                // chỉ offset theo X
                mat[i].SetTextureOffset("_MainTex", new Vector2(distanceX, 0) * speed);
            }
        }
    }
}
