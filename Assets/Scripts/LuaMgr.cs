using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

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

            

            DoLua("print('Helllo World !')");
        }

        /// <summary>
        /// 执行lua代码
        /// </summary>
        /// <param name="luaName"></param>
        public void DoLua(string luaName) 
        {
            luaEnv.DoString(luaName);
        }

        /// <summary>
        /// lua加载路径重定向
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        private byte[] LuaLoader1(ref string _path) 
        {

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
