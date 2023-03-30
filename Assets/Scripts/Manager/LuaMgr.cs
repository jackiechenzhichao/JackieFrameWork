using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

namespace JackieFrame
{
    /// <summary>
    /// lua加载管理类
    /// </summary>
    public class LuaMgr:Singleton<LuaMgr>
    {
        /// <summary>
        /// lua虚拟器
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
        /// 执行lua代码
        /// </summary>
        /// <param name="luaName"></param>
        public void DoLuaFile(string luaName) 
        {
            string str = string.Format("require('{0}')", luaName);
            if (luaEnv ==null) 
            {
                Debug.LogError("LuaEnv未初始化");
                return;
            }
            luaEnv.DoString(str);
        }

        /// <summary>
        /// lua加载路径重定向
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
                Debug.Log("重定向失败，文件名为：" + luaName);
            }
            return null;
        }

        /// <summary>
        /// lua加载路径重定向
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        private byte[] LuaLoader2(ref string _path)
        {
            return null;
        }
    }
}
