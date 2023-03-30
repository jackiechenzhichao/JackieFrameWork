using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

namespace JackieFrame
{
    /// <summary>
    /// lua���ع�����
    /// </summary>
    public class LuaMgr:Singleton<LuaMgr>
    {
        /// <summary>
        /// lua������
        /// </summary>
        private static LuaEnv luaEnv;

        private string luaPath 
        {
            get 
            {
                string path = "";
#if UNITY_EDITOR
                path = $"{Application.dataPath}/Lua/"; 
# endif
                return path;
            }
        }

        public void Init() 
        {
            if (luaEnv == null) 
            {
                luaEnv = new LuaEnv();
                luaEnv.AddLoader(LuaLoader1);
                luaEnv.AddLoader(LuaLoader2);
            }
        }

        /// <summary>
        /// ִ��lua����
        /// </summary>
        /// <param name="luaName"></param>
        public void DoLuaFile(string luaName) 
        {
            string str = string.Format("require('{0}')", luaName);
            if (luaEnv ==null) 
            {
                Debug.LogError("LuaEnvδ��ʼ��");
                return;
            }
            luaEnv.DoString(str);
        }

        /// <summary>
        /// lua����·���ض���
        /// </summary>
        /// <param name="luaName"></param>
        /// <returns></returns>
        private byte[] LuaLoader1(ref string luaName) 
        {
            string luaFile = luaPath + luaName + ".lua";
            if (File.Exists(luaFile)) 
            {
                return File.ReadAllBytes(luaFile);
            }
            else 
            {
                Debug.Log("�ض���ʧ�ܣ��ļ���Ϊ��" + luaName);
            }
            return null;
        }

        /// <summary>
        /// lua����·���ض���
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        private byte[] LuaLoader2(ref string _path)
        {
            return null;
        }
    }
}
