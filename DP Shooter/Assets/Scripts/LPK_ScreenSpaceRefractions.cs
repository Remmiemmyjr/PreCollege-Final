/***************************************************
File:           LPK_ScreenSpaceRefractions.cs
Authors:        Christopher Onorati
Last Updated:   1/12/2020
Last Version:   2019.1.5

Description:
  This component is added to a camera to make it render a
  RT texture that is used for LPK_ScreenRefract effects.
  This script is a tad advanced, so if you have any
  questions on usage, please contact the author.

Copyright 2018-2020, DigiPen Institute of Technology
***************************************************/

using UnityEngine;

namespace LPK
{

/**
* CLASS NAME  : LPK_ScreenSpaceRefractions
* DESCRIPTION : Create an RT texture via camera for use in LPK_ScreenRefract.
**/
[AddComponentMenu("LPK/Effects/LPK_Screen Space Refractions")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class LPK_ScreenSpaceRefractions : MonoBehaviour
{
    /************************************************************************************/

    public Vector2Int m_vecTextureResolution = new Vector2Int( 450, 285 );

    /************************************************************************************/

    //Name of the texture to generate.
    string m_sRealTimeTextureName = "_RTScreenSpaceRefractions";

    /************************************************************************************/

    Camera m_cCamera;

    /**
    * FUNCTION NAME: OnEnable
    * DESCRIPTION  : Create the RT texture and set shader properties.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void OnEnable()
    {
        m_cCamera = GetComponent<Camera>();

        CreateRTTexture();
    }

    /**
    * FUNCTION NAME: CreateRTTexture
    * DESCRIPTION  : Create a real time texture for global use.
    * INPUTS       : None
    * OUTPUTS      : None
    **/
    void CreateRTTexture()
    {
        if(m_cCamera.targetTexture != null)
        {
            RenderTexture texture = m_cCamera.targetTexture;

            //Destroy existing texture to replace with our own.
            m_cCamera.targetTexture = null;
            DestroyImmediate(texture);
        }

        m_cCamera.targetTexture = new RenderTexture(m_vecTextureResolution.x, m_vecTextureResolution.y, 16);
        
        //Avoid chunky appearence of rendered objects.
        m_cCamera.targetTexture.filterMode = FilterMode.Bilinear;

        //Create global refernece to the texture for shaders to use.
        Shader.SetGlobalTexture(m_sRealTimeTextureName, m_cCamera.targetTexture);
    }
}

}   //LPK
