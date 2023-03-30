using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson6_CallListDic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LuaMgr.instance.Init();
        LuaMgr.instance.DoLuaFile("Main");

        //同一类型List
        List<int> list = LuaMgr.instance.Global.Get<List<int>>("testList");
        Debug.Log("*******************List************************");
        for ( int i = 0; i <list.Count; ++i )
        {
            Debug.Log(list[i]);
        }
        //值拷贝 浅拷贝 不会改变lua中的内容
        list[0] = 100;
        List<int> list2 = LuaMgr.instance.Global.Get<List<int>>("testList");
        Debug.Log(list2[0]);

        //不指定类型 object
        List<object> list3 = LuaMgr.instance.Global.Get<List<object>>("testList2");
        Debug.Log("*******************List object************************");
        for (int i = 0; i < list3.Count; ++i)
        {
            Debug.Log(list3[i]);
        }

        Debug.Log("*******************Dictionary************************");
        Dictionary<string, int> dic = LuaMgr.instance.Global.Get<Dictionary<string, int>>("testDic");
        foreach (string item in dic.Keys)
        {
            Debug.Log(item + "_" + dic[item]);
        }
        dic["1"] = 100000;
        //值拷贝 不会改变lua中的内容
        Dictionary<string, int> dic2 = LuaMgr.instance.Global.Get<Dictionary<string, int>>("testDic");
        Debug.Log(dic2["1"]);

        Debug.Log("*******************Dictionary object************************");
        Dictionary<object, object> dic3 = LuaMgr.instance.Global.Get<Dictionary<object, object>>("testDic2");
        foreach (object item in dic3.Keys)
        {
            Debug.Log(item + "_" + dic3[item]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
