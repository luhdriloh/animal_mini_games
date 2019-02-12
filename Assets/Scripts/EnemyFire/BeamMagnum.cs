using UnityEngine;

public class BeamMagnum : MonoBehaviour
{
    public int _pixelSize;

    // maybe make the texture static and make all beams use this
    private Texture2D _beamTexture;
    private Rect _screenRect;
    private Vector2 _mousePosition;
    private Color32[] _clearTexture;

    private void Start()
    {
        _screenRect = new Rect(Vector2.zero, new Vector2(Camera.main.pixelWidth , Camera.main.pixelHeight));
        _beamTexture = new Texture2D((int)_screenRect.width, (int)_screenRect.height, TextureFormat.RGBA32, false)
        {
            wrapMode = TextureWrapMode.Clamp
        };

        _mousePosition = Vector2.zero;
        ClearTexture(_beamTexture);
        DrawLine(_beamTexture, 0, 0, 403, 403, Color.black);
        _beamTexture.Apply();
    }

    private void Update()
    {
        // get mouse position
        _mousePosition = Input.mousePosition;
    }

    private void OnGUI()
    {
        _beamTexture.SetPixels32(_clearTexture);
        //DrawLine(_beamTexture, 0, 0, 403, 403, Color.red);
        DrawLine(_beamTexture, 0, 0, (int)_mousePosition.x, (int)_mousePosition.y, Color.red);
        _beamTexture.Apply();
        Graphics.DrawTexture(_screenRect, _beamTexture);
    }

    private void ClearTexture(Texture2D tex)
    {
        _clearTexture = tex.GetPixels32();
        Color32 clear = new Color32(0, 0, 0, 0);

        for (int i = 0; i < _clearTexture.Length; i++)
        {
            _clearTexture[i] = clear;
        }
    }

    // code so that if the laser is out of bounds it automatically get the fuck out

    private void DrawLine(Texture2D tex, int x0, int y0, int x1, int y1, Color col)
    {

        int xStart = x0;
        int yStart = y0;

        int dy = (int)(y1 - y0);
        int dx = (int)(x1 - x0);
        int stepx, stepy;

        if (dy < 0) { dy = -dy; stepy = -1; }
        else { stepy = 1; }
        if (dx < 0) { dx = -dx; stepx = -1; }
        else { stepx = 1; }
        dy <<= 1;
        dx <<= 1;

        float fraction = 0;

        tex.SetPixel(x0, y0, col);
        if (dx > dy)
        {
            fraction = dy - (dx >> 1);
            while (Mathf.Abs((xStart + (x0 - xStart) * _pixelSize) - x1) > _pixelSize)
            {
                if (fraction >= 0)
                {
                    y0 += stepy;
                    fraction -= dx;
                }
                x0 += stepx;
                fraction += dy;
                
                //tex.SetPixel(x0, y0, col);
                SetPixelToSetSize(x0, y0, xStart, yStart, tex, col);
            }
        }
        else
        {
            fraction = dx - (dy >> 1);
            while (Mathf.Abs((yStart + (y0 - yStart) * _pixelSize) - y1) > _pixelSize)
            {
                if (fraction >= 0)
                {
                    x0 += stepx;
                    fraction -= dy;
                }
                y0 += stepy;
                fraction += dx;
                //tex.SetPixel(x0, y0, col);
                SetPixelToSetSize(x0, y0, xStart, yStart, tex, col);
            }
        }
    }


    private void SetPixelToSetSize(int x, int y, int xStart, int yStart, Texture2D texture, Color color)
    {
        int xStartPosition = xStart + (x - xStart) * _pixelSize;
        int yStartPosition = yStart + (y - yStart) * _pixelSize;

        for (int i = xStartPosition; i < xStartPosition + _pixelSize; i++)
        {
            for (int j = yStartPosition; j < yStartPosition + _pixelSize; j++)
            {
                texture.SetPixel(i, j, color);
            }
        }
    }
}
