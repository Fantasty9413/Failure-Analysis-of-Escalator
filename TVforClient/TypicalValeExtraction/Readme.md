# TypicalValeExtraction文档

## 1.框架

* DLL库
  * MWArray.dll：用于matlab数值计算，辅助核心算法。
  * ComputationMethod.dll：特征值提取的计算方法。通过matlab将核心算法打包成.NET组件直接导入。
  * ToolBox.dll：测试工具（导入测试数据）。
* class类
  * Signal.cs：信号对象基类。
  * SignalData.cs：测试对象。用于生成模拟数据，测试算法与接口。
  * VibrationAnalysis.cs：用于振动信号的分析，提取特征值。
* 其他
  * Program.cs：main函数，用于测试算法与框架。
  * ComputationMethod.ctf：移植产生的文件，具体作用 不清楚，应该和库调用有关，放在ComputationMethod.dll的目录下即可。
  * ToolBox.ctf：同上。

## 2.接口

​	核心算法实现在matlab中，打包转换成了分析方法类`FTM`（Frequency and Domain analysis Method）。要实现特征值提取只需调用`FTM`中的方法即可。

​	为了方便使用，通过`VibrationAnalysis`类完成了数据类型转换，去除多余输入输出等工作，为`FTM`统一了接口。

* 输入接口
  * Length：采样信号的长度。默认参数8092。
  * Fs：采样频率。默认参数10^4。
  * time：采样信号的时间序列。
  * amplitude：采样信号的幅值序列。
  * bf：基频值，只用于倍频分析，留有set接口。
* 输出接口

|        方法         |               功能描述               |    输出    |                             备注                             |
| :-----------------: | :----------------------------------: | :--------: | :----------------------------------------------------------: |
|  `Analysis_tfv()`   |              获取通频值              |   double   |                                                              |
| `Analysis_pvifds()` | 获取低中高频段的信号峰值和对应的频率 |  2×Array   | Array0:长度1×3，分别为低中高频段的峰值；Array1:长度1×3，分别为Array中幅值对应的频率； |
|   `Analysis_kv()`   |     获取时域波形峰度（即陡峭度）     |   double   |                                                              |
|   `Analysis_pv()`   |    获取时域信号的峰值及其对应时间    |  4×double  |                                                              |
|  `Analysis_ppv()`   |         获取时域信号的峰峰值         |   double   |                                                              |
|   `Analysis_Nbf`    |    获取1至3倍基频时的倍频信号幅值    | Array：1×3 |  Array[0]:1倍频幅值；Array[1]:2倍频幅值；Array[2]:3倍频幅值  |

## 3.使用方法	

* step1. 创建`VibrationAnalysis`类的对象

* step2. 输入`time`、`amplitude`等数据参数

* step3. 调用接口获取相应特征值的数据

## 4.测试

​	分别利用此demo和matlab进行特征值提取，对比二者结果，验证了此demo的可行性。

​	测试数据路径：`\bin\Debug\netcoreapp3.1\data`

​	测试结果如下：

* test1:

<img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_matlab.jpg" alt="TestResult_matlab" style="zoom:120%;" /><img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_client.jpg" alt="TestResult_client" style="zoom:88%;" />

* test2:

<img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_matlab_8192.jpg" alt="TestResult_matlab" style="zoom:120%;" /><img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_client_8192.jpg" alt="TestResult_client" style="zoom:88%;" />

