using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEditor;
using System.IO;
 
#if UNITY_EDITOR && UNITY_WEBGL
namespace FooBar.Editor
{
    /// <summary>
    /// Pre processor to set the right web gl template.
    /// </summary>
    public class WebGLTemplatePreProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;
 
        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("Starting to pre-process webl template...");
 
            var destinationFolder = Path.GetFullPath("Assets/WebGLTemplates/MinimalVkCom");
 
            var sourceFolder = Path.GetFullPath("Packages/VK SDK/Runtime/WebGLTemplates/MinimalVkCom");
 
            Debug.Log($"Copying template from {sourceFolder}...");
 
            FileUtil.ReplaceDirectory(sourceFolder, destinationFolder);
 
            AssetDatabase.Refresh();
 
            Debug.Log($"Setting webgl template, old was = {PlayerSettings.WebGL.template}");
 
            PlayerSettings.WebGL.template = "PROJECT:MinimalVkCom";
 
            Debug.Log($"Set webgl template to {PlayerSettings.WebGL.template}");
 
            Debug.Log("Done pre-processing webl template...");
        }
    }
}
#endif