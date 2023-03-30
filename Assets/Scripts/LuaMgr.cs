using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

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
        /// ִ��lua����
        /// </summary>
        /// <param name="luaName"></param>
        public void DoLua(string luaName) 
        {
            luaEnv.DoString(luaName);
        }

        /// <summary>
        /// lua����·���ض���
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        private byte[] LuaLoader1(ref string _path) 
        {

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
