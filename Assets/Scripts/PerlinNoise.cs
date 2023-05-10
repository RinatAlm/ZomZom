using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PerlinNoise
{
    
    int width = 16;
    int height = 16;

    public float scale = 5;
    public float offsetX;
    public float offsetY;

    public Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width,height);

        for(int x = 0;x<width;x++)
        {
            for(int y = 0;y<height;y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

     private Color CalculateColor(int x,int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;
        float sample = Mathf.PerlinNoise(x, y);
        return new Color(sample, sample, sample);
    }
}
