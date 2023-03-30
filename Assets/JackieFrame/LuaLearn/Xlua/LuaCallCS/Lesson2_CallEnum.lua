print("*********Lua调用C#枚举相关知识点***********")

--枚举调用 
--调用Unity当中的枚举
--枚举的调用规则 和 类的调用规则是一样的
--CS.命名空间.枚举名.枚举成员
--也支持取别名 
PrimitiveType = CS.UnityEngine.PrimitiveType
GameObject = CS.UnityEngine.GameObject

local obj = GameObject.CreatePrimitive(PrimitiveType.Cube)

--自定义枚举 使用方法一样 只是注意命名空间即可
E_MyEnum =  CS.E_MyEnum

local c = E_MyEnum.Idle
print(c)
--枚举转换相关
--数值转枚举
local a = E_MyEnum.__CastFrom(1)
print(a)
--字符串转枚举
local b = E_MyEnum.__CastFrom("Atk")
print(b)
