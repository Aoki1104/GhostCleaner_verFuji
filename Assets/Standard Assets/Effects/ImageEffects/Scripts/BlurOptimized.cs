using System;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    [AddComponentMenu("Image Effects/Blur/Blur (Optimized)")]
    public class BlurOptimized : PostEffectsBase
    {

        [Range(0, 2)]
        public int downsample = 2;
       

        public enum BlurType
        {
            StandardGauss = 0,
            SgxGauss = 1,
        }

        [Range(0.0f, 10.0f)]
        public float blurSize = 10f;

        [Range(1, 4)]
        public int blurIterations = 4;

        public BlurType blurType = BlurType.StandardGauss;

        public Shader blurShader = null;
        private Material blurMaterial = null;


        private int next_step, step = 0;
        private bool state = false;
        private float deleay_time = 0;
        private float Processing_time = 0;

        void start()
        {
            PlayerPrefs.SetInt("Downsample", downsample);
            PlayerPrefs.SetFloat("BlurSize", blurSize);

        }
        public override bool CheckResources()
        {
            CheckSupport(false);

            blurMaterial = CheckShaderAndCreateMaterial(blurShader, blurMaterial);

            if (!isSupported)
                ReportAutoDisable();
            return isSupported;
        }

        public void OnDisable()
        {
            if (blurMaterial)
                DestroyImmediate(blurMaterial);
        }

        public void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (CheckResources() == false)
            {
                Graphics.Blit(source, destination);
                return;
            }

            float widthMod = 1.0f / (1.0f * (1 << downsample));

            blurMaterial.SetVector("_Parameter", new Vector4(blurSize * widthMod, -blurSize * widthMod, 0.0f, 0.0f));
            source.filterMode = FilterMode.Bilinear;

            int rtW = source.width >> downsample;
            int rtH = source.height >> downsample;

            // downsample
            RenderTexture rt = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);

            rt.filterMode = FilterMode.Bilinear;
            Graphics.Blit(source, rt, blurMaterial, 0);

            var passOffs = blurType == BlurType.StandardGauss ? 0 : 2;

            for (int i = 0; i < blurIterations; i++)
            {
                float iterationOffs = (i * 1.0f);
                blurMaterial.SetVector("_Parameter", new Vector4(blurSize * widthMod + iterationOffs, -blurSize * widthMod - iterationOffs, 0.0f, 0.0f));

                // vertical blur
                RenderTexture rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
                rt2.filterMode = FilterMode.Bilinear;
                Graphics.Blit(rt, rt2, blurMaterial, 1 + passOffs);
                RenderTexture.ReleaseTemporary(rt);
                rt = rt2;

                // horizontal blur
                rt2 = RenderTexture.GetTemporary(rtW, rtH, 0, source.format);
                rt2.filterMode = FilterMode.Bilinear;
                Graphics.Blit(rt, rt2, blurMaterial, 2 + passOffs);
                RenderTexture.ReleaseTemporary(rt);
                rt = rt2;
            }

            Graphics.Blit(rt, destination);

            RenderTexture.ReleaseTemporary(rt);
        }

        void Update()
        {

            next_step = PlayerPrefs.GetInt("Step");
   

            if (step != next_step)
            {
                Processing_time = 0;
                state = false;
                step = next_step;
            }
            /*
            if(step == 2)
            {
                Processing_time += Time.deltaTime;
                if (Processing_time > 0.5)
                {
                    deleay_time += Time.deltaTime;
                    if (state == false)
                    {
                        downsample = 2;
                        blurSize = 10;
                        blurIterations = 4;
                        state = true;
                        deleay_time = 0;
                    }


                    if (deleay_time > 0.2)
                    {
                        if (blurSize != 0)
                        {
                            blurSize -= 1;
                        }
                        if (blurIterations != 0)
                        {
                            blurIterations -= 1;
                        }
                        if (downsample != 0)
                        {
                            downsample -= 1;
                        }
                        deleay_time = 0;
                    }
                }
            }
            */

                if (step == 1)
                {
          
                    deleay_time += Time.deltaTime;
                    if (state == false)
                    {
                        downsample = 2;
                        blurSize = 10;
                        blurIterations = 4;
                    state = true;
                        deleay_time = 0;
      
                    }


                    if (deleay_time > 0.1)
                    {
                        if (blurSize != 0)
                        {
                            blurSize -= 1;
                        }
                        if (blurIterations != 0)
                        {
                            blurIterations -= 1;
                        }
                        if (downsample != 0)
                        {
                            downsample -= 1;
                        }
                        deleay_time = 0;
                    }
                
            }
        }
    }
}
