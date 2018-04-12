using UnityEngine;
using System.Collections;


public class AutoTransparent : MonoBehaviour
{
    private Material[] m_OldMaterials = null;
    private Shader m_OldShader = null;
    private Color m_OldColor = Color.black;
    private float m_Transparency = 0.3f;
	private const float m_TargetTransparancy = 0.3f;
	private const float m_FallOff = 0.1f; // returns to 100% in 0.1 sec


	public void BeTransparent()
	{
		// reset the transparency;
		m_Transparency = m_TargetTransparancy;
		/*if (m_OldShader == null)
		{
			// Save the current shader
			m_OldShader = GetComponent<Renderer>().material.shader;
			m_OldColor  = GetComponent<Renderer>().material.color;
			GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");
		}*/
        if (m_OldMaterials == null)
        {
            Material[] rendererMat = GetComponent<Renderer>().materials;
            m_OldMaterials = new Material[rendererMat.Length];
            for (int i = 0; i < rendererMat.Length; i++)
            {
                m_OldMaterials[i] = rendererMat[i];
                rendererMat[i] = new Material(rendererMat[i]);
                rendererMat[i].shader = Shader.Find("Transparent/Diffuse");
            }
        }
    }
	void Update()
	{
        /*if (m_Transparency < 1.0f)
		{
			Color C = GetComponent<Renderer>().material.color;
			C.a = m_Transparency;
			GetComponent<Renderer>().material.color = C;
		}
		else
		{
			// Reset the shader
			GetComponent<Renderer>().material.shader = m_OldShader;
			GetComponent<Renderer>().material.color = m_OldColor;
			// And remove this script
			Destroy(this);
		}*/
        if (m_Transparency < 1.0f)
        {
            Material[] rendererMat = GetComponent<Renderer>().materials;
            foreach (Material m in rendererMat)
            {
                if (m.shader.name == "Standard")
                {
                    if (m.GetFloat("_Mode") == 0.0f)
                    {
                        m.shader = Shader.Find("Transparent/Diffuse");
                    }
                }
                Color c = m.color;
                c.a = m_Transparency;
                m.color = c;
            }
        }
        else
        {
            Material[] rendererMat = GetComponent<Renderer>().materials;
            for (int i = 0; i < rendererMat.Length; i++)
            {
                rendererMat[i] = m_OldMaterials[i];
            }
            Destroy(this);
        }
        m_Transparency += ((1.0f-m_TargetTransparancy)*Time.deltaTime) / m_FallOff;
	}
}