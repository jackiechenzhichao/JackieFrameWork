using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

namespace JackieFrame
{

    /// <summary>
    /// ab包加载管理器
    /// </summary>
    public class ABMgr : MonoSingleton<ABMgr>
    {
        /// <summary>
        /// 已经加载的ab包
        /// </summary>
        private Dictionary<string, AssetBundle> abDics = new Dictionary<string, AssetBundle>();

        //主包
        private AssetBundle mainAB;

        private bool isloadMainAB;

        //主包信息
        private AssetBundleManifest mainfest;

        //主包名称
        public string MainABName
        {
            get
            {
                return "PC";
            }
        }

        /// <summary>
        /// AB包路径
        /// </summary>
        public string Path
        {
            get
            {
                string _path = $"{Application.streamingAssetsPath}/ABRes/{MainABName}/";
                return _path;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public T LoadRes<T>(string abName, string resName) where T : UnityEngine.Object
        {
            //加载主包
            if (mainAB == null)
            {
                mainAB = AssetBundle.LoadFromFile(Path + MainABName);
                mainfest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
            //加载依赖包
            string[] deps = mainfest.GetAllDependencies(abName);
            AssetBundle ab = null;
            for (int i = 0; i < deps.Length; i++)
            {
                if (!abDics.ContainsKey(deps[i]))
                {
                    ab = AssetBundle.LoadFromFile(Path + deps[i]);
                    abDics.Add(deps[i], ab);
                }
            }
            //加载本包
            if (!abDics.ContainsKey(abName))
            {
                ab = AssetBundle.LoadFromFile(Path + abName);
                abDics.Add(abName, ab);
            }
            //加载具体资源
            T result = abDics[abName].LoadAsset<T>(resName);
            //如果是Gameobject,返回实例化后的对象，简化在外面还需要实例化的操作
            if (typeof(T).Name == "GameObject")
            {
                return Instantiate(result);
            }
            return result;
        }

        /// <summary>
        /// 加载单个AB包
        /// </summary>
        /// <returns></returns>
        private IEnumerator LoadABAnsy(string abName, Action<AssetBundle> callback)
        {
            //如果当前包没有被加载
            if (!abDics.ContainsKey(abName))
            {
                //首先加入空值占用
                abDics.Add(abName, null);
                UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(Path + abName);
                yield return www.SendWebRequest();

                //if (www.result != UnityWebRequest.Result.Success)
                //{
                //    Debug.Log(www.error);
                //    //加载失败移除
                //    abDics.Remove(abName);
                //}
                //else
                //{
                //    //获取ab包内容
                //    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(www);
                //    //储存加载的包
                //    abDics[abName] = ab;
                //    callback?.Invoke(abDics[abName]);
                //}
            }
            else
            {
                //判断是否加载成功
                yield return new WaitUntil(() => { return !abDics.ContainsKey(abName) || abDics[abName] != null; });
                //如果加载失败，直接返回
                if (!abDics.ContainsKey(abName))
                    yield break;
                callback?.Invoke(abDics[abName]);
            }

        }



        /// <summary>
        /// 加载AB包
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        private IEnumerator LoadRes<T>(string abName, string resName, Action<T> callback) where T : UnityEngine.Object
        {
            //加载依赖包
            string[] deps = mainfest.GetAllDependencies(abName);

            for (int i = 0; i < deps.Length; i++)
            {
                yield return StartCoroutine(LoadABAnsy(deps[i], null));
            }
            //加载本包
            yield return StartCoroutine(LoadABAnsy(abName, (ab) =>
             {

                 T obj = abDics[abName].LoadAsset<T>(resName);
            //如果是gameobject对象，直接返回实例化后的对象
            if (typeof(T).Name == "GameObject")
                 {
                     T t = Instantiate(obj);
                     callback?.Invoke(t);
                     return;
                 }
                 callback?.Invoke(obj);
             }));


        }

        /// <summary>
        /// 网络加载AB包
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="abName"></param>
        /// <param name="resName"></param>
        /// <param name="callback"></param>
        public void LoadResByWebRequest<T>(string abName, string resName, Action<T> callback) where T : UnityEngine.Object
        {

            StartCoroutine(LoadABAnsy(MainABName, (ab) =>
             {
                 if (mainfest == null)
                     mainfest = mainfest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                 StartCoroutine(LoadRes<T>(abName, resName, callback));
             }));
        }

        /// <summary>
        /// 释放单个AB包
        /// </summary>
        /// <param name="abName"></param>
        public void ReleaseAB(string abName)
        {
            if (abDics.ContainsKey(abName) && abDics[abName] != null)
            {
                abDics[abName].Unload(false);
            }
        }


        /// <summary>
        /// 释放所有AB包
        /// </summary>
        public void ReleaseAllAB()
        {
            AssetBundle.UnloadAllAssetBundles(false);
            abDics.Clear();
        }
    }
}