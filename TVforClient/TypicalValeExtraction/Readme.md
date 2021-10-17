# TypicalValeExtraction文档

## 0.环境配置

​	[生产环境配置](.\Environment Configuration.md)

## 1.框架

* DLL库
  * MWArray.dll：用于matlab数值计算，辅助核心算法。
  * ComputationMethod.dll：特征值提取的计算方法。通过matlab将核心算法打包成.NET组件直接导入。
  * ToolBox.dll：测试工具（导入测试数据）。
  * BasicFrequencyAnaysis.dll：用于扶梯的各类基频值分析，用于倍频值的特征值分析。
  * SpectrumAnalysis.dll：用于扶梯部件采集信号的频谱数据分析。
* class类
  * Signal.cs：信号对象基类。
  * SignalData.cs：测试对象。用于生成模拟数据，测试算法与接口。
  * VibrationAnalysis.cs：用于振动信号的分析，提取特征值。
  * Escalator_BFA.cs：`BasicFrequencyAnaysis.dll`的接口，用于为倍频值的分析。
  * Spectrum.cs：`SpectrumAnalysis.dll`的接口，用于频谱数据的分析。
* 其他
  * Program.cs：main函数，用于测试算法与框架。
  * ComputationMethod.ctf：移植产生的文件，具体作用 不清楚，应该和库调用有关，放在ComputationMethod.dll的目录下即可。
  * ToolBox.ctf：同上。
  * BasicFrequencyAnaysis.ctf：同上。
  * SpectrumAnalysis.ctf：同上。

## 2.接口

### 2.1特征值分析

​	核心算法实现在matlab中，打包转换成了分析方法类`FTM`（Frequency and Time domain analysis Method）。要实现特征值提取只需调用`FTM`中的方法即可。

​	为了方便使用，通过`VibrationAnalysis`类完成了数据类型转换，去除多余输入输出等工作，为`FTM`统一了接口。

* 输入接口
  * Length：采样信号的长度。默认参数8092。
  * Fs：采样频率。默认参数10^4。
  * time：采样信号的时间序列。
  * amplitude：采样信号的幅值序列。
  * bf：基频值，只用于倍频分析，留有set接口，更新了基频值的分析方法，可与set接口配合使用。
* 输出接口

|        方法         |               功能描述               |    输出    |                             备注                             |
| :-----------------: | :----------------------------------: | :--------: | :----------------------------------------------------------: |
|  `Analysis_tfv()`   |              获取通频值              |   double   |                                                              |
| `Analysis_pvifds()` | 获取低中高频段的信号峰值和对应的频率 |  2×Array   | Array0:长度1×3，分别为低中高频段的峰值；Array1:长度1×3，分别为Array中幅值对应的频率； |
|   `Analysis_kv()`   |     获取时域波形峰度（即陡峭度）     |   double   |                                                              |
|   `Analysis_pv()`   |    获取时域信号的峰值及其对应时间    |  4×double  |                                                              |
|  `Analysis_ppv()`   |         获取时域信号的峰峰值         |   double   |                                                              |
|   `Analysis_Nbf`    |    获取1至3倍基频时的倍频信号幅值    | Array：1×3 |  Array[0]:1倍频幅值；Array[1]:2倍频幅值；Array[2]:3倍频幅值  |

### 2.2频谱分析

​	对于采集的数据信号进行频谱分析，并得到相应类型的谱数据（x轴数据与y轴数据）。

​	通过`SpecAnalysis`类对谱分析进行了封装，通过`SetData()`方法进行数据输入后，调用`GetSpec(string type, int begin, int end)`方法即可得到相应的谱数据。

* 输入接口

  * Type：所需数据谱的类型。

  |   Type   |  谱类型  |                备注                |
  | :------: | :------: | :--------------------------------: |
  | `"time"` | 时域谱图 |       横坐标时间，单位$s$；        |
  | `"fre"`  | 频域谱图 |  横坐标频率，单位$Hz$；纵坐标幅值  |
  | `"pow"`  | 功率谱图 |  横坐标频率，单位$Hz$；纵坐标幅值  |
  | `"enve"` |  包络谱  |  横坐标频率，单位$Hz$；纵坐标幅值  |
  | `"ceps"` |   倒谱   | 横坐标伪频率，单位$Hz$；纵坐标幅值 |

  * begin：数据开始位置的下标，建议取1，跳过0，0为直流分量。
  * end：数据结束位置的下标，建议最大取250。

* 输出接口

  * result.Item1：普结果y轴数据
  * result.Item2：普结果x轴数据

## 3.使用方法

### 3.1特征值分析	

* step1. 创建`VibrationAnalysis`类的对象
* step2. 输入`time`、`amplitude`等数据参数
* step3. 调用接口获取相应特征值的数据

### 3.2频谱分析

* step1. 创建`SpecAnalysis`类的对象
* step2. 输入`time`、`amplitude`等数据参数
* step3. 调用接口获取相应谱数据

## 4.使用测试

### 4.1特征值分析

​	分别利用此demo和matlab进行特征值提取，对比二者结果，验证了此demo的可行性。

​	测试数据导入接口：`[amplitude, time] = ImportData(DataLength, FileName)`

​	测试数据路径：`\bin\Debug\netcoreapp3.1\data`

​	测试结果如下：

* test1:

<img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_matlab.jpg" alt="TestResult_matlab" style="zoom:120%;" /><img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_client.jpg" alt="TestResult_client" style="zoom:88%;" />

* test2:

<img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_matlab_8192.jpg" alt="TestResult_matlab" style="zoom:120%;" /><img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_client_8192.jpg" alt="TestResult_client" style="zoom:88%;" />

### 4.2频谱分析

​	利用`SpectrumAnalysis`进行谱分析，得出数据，每种谱取三点数据，与结果对比，符合结果。

![client](E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\SpectrumAnalysis\client.jpg)

## 5.移植测试

​	参考`环境配置`在空白环境（即未安装VS、Matlab等软件）中配置生产环境，对TypicalValeExtraction Demo进行运行测试。

​	移植后的Demo在生成环境中运行成功，且分析结果完全符合使用测试。

![image-20210926192626971](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20210926192626971.png)

## Remark

### 基频分析

​	基频值分析方法：通过附体的结构参数，分析得到各种关键类型的基频值，用于特征值的倍频值分析计算。

​	`Escalator_BFA.cs`为基频值分析提供了封装，便于统一接口。

* 输入接口：

  * 名称：`SP`——Structure Parameters，附体的结构参数。

  * 形式：double array[14]

  * 内容：

    | Array Index |             参数物理意义             |   符号   | 单位  | 备注 |
    | :---------: | :----------------------------------: | :------: | :---: | ---- |
    |    $[0]$    |             梯级运行速度             |  $v_1$   | $m/s$ |      |
    |    $[1]$    |           主机次轮箱减速比           |   $i$    |       |      |
    |    $[2]$    |             主机链轮齿数             |  $n_m$   |       |      |
    |    $[3]$    |           主驱动链链轮齿数           |  $n_d$   |       |      |
    |    $[4]$    |          主驱动梯级链轮齿数          |  $n_s$   |       |      |
    |    $[5]$    |             主驱动链节距             |  $d_m$   | $mm$  |      |
    |    $[6]$    |              梯级链节距              |  $d_s$   | $mm$  |      |
    |    $[7]$    |           扶手带驱动链节距           |  $d_h$   | $mm$  |      |
    |    $[8]$    |   扶手带驱动链链轮（主驱动轴）齿数   | $n_{md}$ |       |      |
    |    $[9]$    | 扶手带驱动链链轮（扶手带驱动轴）齿数 | $n_{hd}$ |       |      |
    |   $[10]$    |             梯级链轮直径             | $d_{r1}$ | $mm$  |      |
    |   $[11]$    |            梯级直径轮直径            | $d_{r2}$ | $mm$  |      |
    |   $[12]$    |             左驱动轮直径             | $d_{ml}$ | $mm$  |      |
    |   $[13]$    |             右驱动轮直径             | $d_{mr}$ | $mm$  |      |

* 输出接口：

  1. 键入所需基频值的类型，返回得到对应的基频值，单位为$Hz$。

  2. 获取基频值的方法：`double GetBf(string type)`。

  3. 方法参数于返回值对应表：

  | 基频值类型（type） |             方法返回值意义             |
  | :----------------: | :------------------------------------: |
  |    `"f_motor"`     |            主机马达旋转频率            |
  |      `"f_m"`       |            主机链轮旋转频率            |
  |      `"f_d"`       |             主驱动旋转频率             |
  |      `"f_md"`      |   扶手带驱动链轮（主驱动轴）旋转频率   |
  |      `"f_hd"`      | 扶手带驱动链轮（扶手带驱动轴）旋转频率 |
  |      `"f_r"`       |        梯级链轮／梯级轮旋转频率        |
  |      `"f_dp"`      |         主驱动链多边形效应频率         |
  |      `"f_sp"`      |          梯级链多边形效应频率          |
  |      `"f_hp"`      |       扶手带驱动链多边形效应频率       |

  

