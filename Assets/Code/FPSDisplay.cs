using System.Collections.Generic;
using UnityEngine;

namespace Voxic.Misc
{
    // TODO: Improve/Optimize this class
    public class FPSDisplay : MonoBehaviour
    {
        float deltaTime = 0.0f;
        int index = 0;
        List<float> data = new List<float>(300);

        void Update()
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            data.Add(deltaTime);
            if (data.Count > 300)
                data.RemoveAt(0);
        }

        void OnGUI()
        {
            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
            float msec = deltaTime * 1000.0f;
            float fps = 1.0f / deltaTime;

            float dtAvg = 0;
            for (int i = 0; i < data.Count; i++)
            {
                dtAvg += data[i];
            }
            dtAvg /= data.Count;
            float msecAvg = dtAvg * 1000.0f;
            float fpsAvg = 1.0f / dtAvg;

            string text = string.Format("CUR: {0:0.0} ms ({1:0.} fps)\nAVG: {2:0.0} ms ({3:0.} fps)", msec, fps, msecAvg, fpsAvg);
            GUI.Label(rect, text, style);
        }
    }
}
