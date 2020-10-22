[System.Serializable]
public struct Vector4Bounds
{
    public float leftX, leftY, rightX, rightY;
    public Vector4Bounds(float leftX, float leftY, float rightX, float rightY)
    {
        this.leftX = leftX;
        this.leftY = leftY;
        this.rightX = rightX;
        this.rightY = rightY;
    }
}