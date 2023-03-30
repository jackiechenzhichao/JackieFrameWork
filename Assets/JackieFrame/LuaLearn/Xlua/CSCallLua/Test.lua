print("Test.Lua")
testNumber = 1
testBool = true
testFloat = 1.2
testString = "123"

--我们通过C# 没办直接获取本地局部变量
local testLocal = 10

--无参无返回
testFun = function()
	print("无参无返回")
end

--有参有返回
testFun2 = function(a)
	print("有参有返回")
	return a + 1
end

--多返回
testFun3 = function(a)
	print("多返回值")
	return 1, 2, false, "123", a
end

--变长参数
testFun4 = function(a, ...)
	print("变长参数")
	print(a)
	arg = {...}
	for k,v in pairs(arg) do
		print(k,v)
	end
end

--List
testList = {1,2,3,4,5,6}
testList2 = {"123", "123", true, 1, 1.2}

--Dictionary
testDic = {
	["1"] = 1,
	["2"] = 2,
	["3"] = 3,
	["4"] = 4
}

testDic2 = {
	["1"] = 1,
	[true] = 1,
	[false] = true,
	["123"] = false
}

--lua当中的一个自定义类
testClas = {
	testInt = 2,
	testBool = true,
	testFloat = 1.2,
	testString = "123",
	testFun = function()
		print("123123123")
	end
}