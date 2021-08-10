# Readme

## C#工程调用matlab算法

* 编写好m文件函数（尽量用单独参数）
* matlab 命令行启动deploytool工具箱
* 打包生成`.dll`文件等。[参考1](https://blog.csdn.net/yu_ncepu/article/details/87797953)

* 将生成的`.dll`文件和`.ctf`（缺失ctf文件会报错）文件一并复制到`obj\debug`目录下。
* 将matlab工具箱中带的`MWArray.dll`文件复制到上述目录下。[参考2]([Matlab混合编程之NET组件（C#篇） - 程序员大本营 (pianshen.com)](https://www.pianshen.com/article/7641675654/))
* 将上述两条中的`.dll`文件天添加引用。右键“依赖项” -> 添加现有项目 -> .... [参考1](https://blog.csdn.net/yu_ncepu/article/details/87797953)
* 将`ctf`文件添加为嵌入资源（当前未添加，依旧可以运行，后续考虑添加增加速度）
* 编写代码（记得添加相应的空间命名）[参考3](https://blog.csdn.net/m0_37283423/article/details/74421305)

```c#
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using mtest;		// 自定义的
```

* 定义m文件格式的参数（函数的输入输出都需要定义成m文件的格式）

```c#
MWNumericArray time = new MWNumericArray(1, 8000, s1.time);
MWNumericArray amplitude = new MWNumericArray(1, 8000, s1.time);
MWArray result;
```

* 定义自定义的类，利用这个类来调用m文件自定义的函数

```c#
Class1 c = new Class1();
c.mfunction();
```

* 再将结果转换成c#相应的数据格式进行输出或处理 [参考3](https://blog.csdn.net/m0_37283423/article/details/74421305)



## MW类型数据

* 对于多输出函数，去看函数的重载定义，一般要增加函数参数个数，指定函数输出的个数。对于输出，则定义MWArray类型的数组来接收值，然后从数组中取出来得到分别的值。

```C#
function [ amplitude, f ] = fft_ss( y, Fs )		// matlab function
public MWArray[] fft_ss(int numArgsOut, MWArray y, MWArray Fs);		// 转换后的一种重载函数

MWArray[] result2;
result2 = c2.fft_ss(2, amplitude, Fs);
FD_amp = result2[0];
FD_fre = result2[1];
```



* `MWArray`类型类似于父类，是Matlab中使用的数据类型；`MWNumericArray`类似子类，是Matlab和C#的中间类型。后者到前者可以直接转换，前者到后者需要强制转换（数组的强制转换有点问题）。

  `MWArray`和`MWNumericArray`类型都可以条用转换函数，但是前者能用的转换函数很少，最好还是用后者来转换。

```c#
Array a1;
a1 = MWNumericArray)(FD_amp)).ToVector(MWArrayComponent.Real);
```

