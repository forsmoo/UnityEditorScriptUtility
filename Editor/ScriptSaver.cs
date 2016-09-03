using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Text;

namespace ScriptGenerator
{
    public class ScriptSaver
    {
        public static bool GenerateScript(string folderPath,string filename, IScriptGenerator generator)
        {
            try
            {
                string path = folderPath + Path.AltDirectorySeparatorChar + filename;
                if (!File.Exists(path))
                {
                    using (var streamW = new StreamWriter(path))
                    {
                        generator.Generate(streamW);
                    }

                    AssetDatabase.Refresh();
                    return true;
                }
                else
                {
                    Debug.LogError("Script with the same name already exists at path " + path);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return false;
        }
    }

}