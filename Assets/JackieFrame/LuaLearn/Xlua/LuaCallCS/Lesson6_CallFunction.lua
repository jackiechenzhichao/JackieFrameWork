print("*********Lua调用C# 重载函数相关知识点***********")

local obj = CS.Lesson6()

--虽然Lua自己不支持写重载函数
--但是Lua支持调用C#中的重载函数  
print(obj:Calc())
print(obj:Calc(15, 1))

--Lua虽然支持调用C#重载函数
--但是因为Lua中的数值类型 只有Number
--对C#中多精度的重载函数支持不好 傻傻分不清
--在使用时 可能出现意想不到的问题
print(obj:Calc(10))
print(obj:Calc(10.2))


--解决重载函数含糊的问题
--xlua提供了解决方案 反射机制 
--这种方法只做了解 尽量别用
--Type是反射的关键类
--得到指定函数的相关信息
local m1 = typeof(CS.Lesson6):GetMethod("Calc", {typeof(CS.System.Int32)})
local m2 = typeof(CS.Lesson6):GetMethod("Calc", {typeof(CS.System.Single)})

--通过xlua提供的一个方法 把它转成lua函数来使用
--一般我们转一次 然后重复使用
local f1 = xlua.tofunction(m1)
local f2 = xlua.tofunction(m2)
--成员方法 第一个参数传对象
--静态方法 不用传对象
print(f1(obj, 10))
print(f2(obj, 10.2))