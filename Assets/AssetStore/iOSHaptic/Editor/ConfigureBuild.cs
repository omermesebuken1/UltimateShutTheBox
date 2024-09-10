﻿using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.IO;

public class ConfigureBuild : MonoBehaviour {

	internal static void CopyAndReplaceDirectory(string srcPath, string dstPath)
    {
        if (Directory.Exists(dstPath))
            Directory.Delete(dstPath);
        if (File.Exists(dstPath))
            File.Delete(dstPath);
 
        Directory.CreateDirectory(dstPath);
 
        foreach (var file in Directory.GetFiles(srcPath))
            File.Copy(file, Path.Combine(dstPath, Path.GetFileName(file)));
 
        foreach (var dir in Directory.GetDirectories(srcPath))
            CopyAndReplaceDirectory(dir, Path.Combine(dstPath, Path.GetFileName(dir)));
    }

	[PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            string projPath = PBXProject.GetPBXProjectPath(path);
            PBXProject proj = new PBXProject(); 
            proj.ReadFromString(File.ReadAllText(projPath));
            string target = "";
            target = proj.GetUnityMainTargetGuid();

#if UNITY_2019_3_OR_NEWER
            Debug.Log("Targetting UnityFramework for 2019.3+");
            target = proj.GetUnityFrameworkTargetGuid();
#endif

            proj.AddFrameworkToProject(target, "CoreHaptics.framework", false);
            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}
