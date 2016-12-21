using UnityEngine;
using System.Collections;

// For predefining variable names.
[System.Serializable]
public class NamedFloat
{
	public string name;
	public float value;
}

[System.Serializable]
public class NamedColor
{
	public string name;
	public Color value;
}

[System.Serializable]
public class NamedInt
{
	public string name;
	public int value;
}

[System.Serializable]
public class NamedTex
{
	public string name;
	public Texture value;
}

// Post process class.
// Could be used as template for shader-specific class.
[ExecuteInEditMode]
public class PostProcessEffect : MonoBehaviour
{
	public Shader shader;
	private Material m_Material;
	public float intensity, threshold;

	protected Material material {
		get {
			if (m_Material == null) {
				m_Material = new Material (shader);
				m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return m_Material;
		}
	}

	protected void OnDisable ()
	{
		OnValidate ();
	}

	protected void OnValidate ()
	{
		if (m_Material) {
			DestroyImmediate (m_Material);
		}
	}

	void SetShaderVariables ()
	{
		material.SetFloat ("_gsIntensity", intensity);
		material.SetFloat ("_gsThreshold", threshold);
	}

	void Awake ()
	{
		SetShaderVariables ();
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		SetShaderVariables ();
		Graphics.Blit (source, destination, material);
	}
}
