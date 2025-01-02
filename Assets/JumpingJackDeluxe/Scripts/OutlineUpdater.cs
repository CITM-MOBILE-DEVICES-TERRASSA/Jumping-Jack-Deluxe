using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineUpdater : MonoBehaviour
{
    public Material outlineMaterial; // Material con el shader personalizado
    public Color baseColor = Color.white;
    public Color outlineColor = Color.blue;
    public float outlineThickness = 0.05f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (outlineMaterial != null)
        {
            // Asignar el material al SpriteRenderer
            spriteRenderer.material = outlineMaterial;
        }
    }

    void Update()
    {
        if (spriteRenderer.sprite != null && outlineMaterial != null)
        {
            // Actualizar propiedades del material
            outlineMaterial.SetTexture("_MainTex", spriteRenderer.sprite.texture);
            outlineMaterial.SetColor("_BaseColor", baseColor);
            outlineMaterial.SetColor("_OutlineColor", outlineColor);
            outlineMaterial.SetFloat("_OutlineThickness", outlineThickness);

            // Ajustar las UV dinámicamente al recorte del sprite
            Rect textureRect = spriteRenderer.sprite.textureRect;
            Vector2 textureSize = spriteRenderer.sprite.texture.texelSize;
            Vector4 uv = new Vector4(
                textureRect.x * textureSize.x,
                textureRect.y * textureSize.y,
                textureRect.width * textureSize.x,
                textureRect.height * textureSize.y
            );
            outlineMaterial.SetVector("_MainTex_ST", uv);
        }
    }
}
