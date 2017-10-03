# CppTestCaseViewer

这个小工具可以用来查看C++作业的测例。

## 前提
1. 下载好了题目
2. 装有.NET Framework 4.5，如果你有VS2013，那么应该没什么问题。

## 使用
1. 下载CppTestCaseViewer.exe，并保存到某个空目录，比如说D:\cpptest\下。
2. 从目录`C:\Users\{你的用户名}\AppData\Roaming\CppPlugin\download\{作业编号}\`下复制所有文件到目录D:\cpptest\testcases\下；
3. 运行CppTestCaseViewer.exe，并在最后提示`Test cases for all {problem num} problem...`后按下任意键关闭程序。
5. 进入`.\export`文件夹，里面会有以题号命名的不同文件夹，点开任意一个文件夹，里面会有命名类似于`test_{编号}.in`和`test_{编号}.out`的文件，它们就是测试用例。

## 注意
1. 运行插件后会产生`.\export`,`.\decompressed`以及`.\zips`文件夹，请保证在运行插件以前，**把它们全部删掉**！目录只留一个exe文件和`.\testcases`文件夹，否则会报错！
