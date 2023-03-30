print("*********事件加减替换***********")


xlua.hotfix(CS.HotfixMain, {
	--add_事件名 代表着时间加操作
	--remove_事件名 减操作
	add_myEvent = function(self, del)
		print(del)
		print("添加事件函数")
		--会去尝试使用lua使用C#事件的方法去添加
		--在事件加减的重定向lua函数中
		--千万不要把传入的委托往事件里存
		--否则会死循环
		--会把传入的 函数 存在lua中！！！！！
		--self:myEvent("+", del)
	end,
	remove_myEvent = function(self, del )
		print(del)
		print("移除事件函数")
	end
})