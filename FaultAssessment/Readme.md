# 故障分析文档

## 测试

### 特征提取测试

对测试数据进行特征值分析测试

* original data

<img src="E:\Code_master\Risk Assessment of Escalator\Figure\TVforClient\TestResult_matlab.jpg" alt="TestResult_matlab" style="zoom:120%;" />

* DE1_base_m

![image-20211220143934555](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20211220143934555.png)

* DE2_base_m

![image-20211220144442960](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20211220144442960.png)

* DE3_base_m

![image-20211220145538894](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20211220145538894.png)

### 训练测试（01.05）

#### 测试简介

针对Standard、Fault1-Fault8的数据（Standard50组，其余各类25组，具体内容见下*数据*章节），对原始数据实现特征值提取，然后打上对应标签，存入数据集，再利用Matlab Classficaition Leaner工具箱进行预测模型训练，部分实验结果见下。

#### 测试结果

##### 1.整体结果

所有方法测试的准确率见下图。选取部分高准确率的实验进行详细的结果分析。

<img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220106162925536.png" alt="image-20220106162925536" style="zoom:50%;" /><img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220106162940623.png" alt="image-20220106162940623" style="zoom:50%;" />

##### 2.部分详细结果

选取部分准确率较高的训练实验进行详细分析，并将相应的模型存储至路径`.\Model\01_05\`。

1. 实验1

   * 方式：Tree-Complex Tree

   * 准确率：74%

   * 详细预测结果：

     ![image-20220106153834176](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220106153834176.png)

     2. 实验2

     * 方式：Tree-Medium Tree

     * 准确率：74.8%

     * 详细预测结果：

       ![image-20220106154231025](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220106154231025.png)

3. 实验3

   * 方式：Enssemble-Boosted Trees

   * 准确率：78.0%

   * 详细预测结果：

     ![image-20220106154703810](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220106154703810.png)

4. 实验4

   * 方式：Enssemble-Bagged Trees

   * 准确率：**82.4%**

   * 详细预测结果：

     ![image-20220106154828369](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220106154828369.png)

##### 3.结果分析

对于标签号为5和6的数据，预测区分结果不太理想，且占据了错误预测数据的大部分。标签5的数据和标签6的数据的唯一区别在于前者故障类型是**齿轮断齿**，后者故障类型是**齿轮脱落**，二者对应的采集数据的特征值区分不明显。针对这一问题，解决方案考虑将二者**归为同一类型故障**。

### 训练测试（01.11）

#### 测试简介

相比于*训练测试（01.05）*，数据量翻倍，其他条件相同。模型名为`ClassifierModel`，保存至`.\Model\01_11\`路径下，文件名为对应的分类方式，如`Enssemble_Bagged_Trees`。

#### 测试结果

##### 1.整体结果

整体结果与*训练测试（01.05）*的结果大体相同，正确率最高的模型依旧为*Enssemble-Bagged Trees*方式，正确率由上次的**82.4%**提升到了**84.6%**。*Tree-Complex Tree*获取的模型也依旧有较好的表现，且准确率由上次的**74%**提升到了**82.6%**。

##### 2.详细结果分析

1. 实验1

   * 方式：Tree-Complex Tree

   * 准确率：82.6%

   * 详细预测结果：

     <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220111143149109.png" alt="image-20220111143149109" style="zoom:50%;" /><img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220111143216762.png" alt="image-20220111143216762" style="zoom:50%;" />

   * 分析：

     * Fault2的预测正确率最低，且错误的预测结果分布在Standard、Falule1、Fault3和Fault4等多种情况中。

     * Fault5和Fault6的预测正确率为倒数第二和倒数第三。二者的错误预测结果大多分布在对方，说明其对应的两种故障的特征相似度较高。通过具体分析其故障原因。Fault5对应断齿，发生在齿轮上；Fault6对应脱落，发生在蜗杆接触部分。虽然二者故障有差异，但本质上都是发生在同一个运动附上的故障，考虑将二者合并为同一类型故障。（下图左为断齿，右为脱落）

       <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220111144753850.png" alt="image-20220111144753850" style="zoom:50%;" /><img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220111144759989.png" alt="image-20220111144759989" style="zoom:50%;" />

     * 与*训练测试01.05*相比，随着数据量的增加，针对Fault5和Fault6的预测准确率大幅上升，考虑是否再次增加相应数据量进行训练测试。

2. 实验2

   * 方式：Enssemble-Bagged Trees

   * 准确率：84.6%

   * 详细预测结果：

     <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220111145308633.png" alt="image-20220111145308633" style="zoom:50%;" /><img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220111145321164.png" alt="image-20220111145321164" style="zoom:50%;" />

   * 分析：

     * 错误率最高的分别对应Fault6、Fault2和Fault5。
     * Fault6和Fault5之间依旧存在区分度不够的问题，见实验1中的分析。
     * Fault2的预测结果也通实验1中的分析，但正确率略有提升。
     * 考虑基于Enssemble-Bagged Trees的方法，将Fault5和Fault6进行合并。
     * 与*训练测试01.05*相比，随着数据量的增加，针对Fault5的预测准确率大幅上升，针对Fault6的预测准确率略有上升，考虑是否再次增加相应数据量进行训练测试。

### 训练测试（01.14）

#### 测试简介

相比于*训练测试（01.11）*，将`Fault5`、`Fault6`两种故障合并，其他条件相同。

#### 测试结果

##### 1.整体结果

整体结果与*训练测试（01.11）*的结果有一定差别，正确率最高的模型为*Enssemble-RUSBoosted Trees*，其正确率达到了87.8%；*Enssemble-Bagged Trees*方式的正确率为87.6%，*Tree-Complex Tree*获取的模型的准确率为86.4%。

##### 2.详细结果分析

1. 实验1

   * 方式：Tree-Complex Tree

   * 准确率：86.4%

   * 详细结果：

     <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220114150749104.png" alt="image-20220114150749104" style="zoom:50%;" />

2. 实验2

   * 方式：Enssemble-Bagged Trees

   * 准确率：87.6%

   * 详细结果：

     <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220114150851530.png" alt="image-20220114150851530" style="zoom:50%;" />

3. 实验3

   * 方式：Enssemble-RUSBoosted Trees

   * 准确率：87.8%

   * 详细结果：

     <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220114150931145.png" alt="image-20220114150931145" style="zoom:50%;" />

### 训练测试（01.17）

#### 测试简介

相比于*训练测试（01.11）*只考虑`base`这一位置的采集数据，还加入了`bolt`这一位置的采集数；并且特征值中**去掉了Nbf相关的特征**。信号的具体采集位置见`数据`中的信号采集位置图。

#### 测试结果

##### 1.整体结果

整体结果与*训练测试（01.11）*的结果有相似，正确率最高的模型依旧为*Enssemble-Bagged Trees*，其正确率达到了89.8%；*Enssemble-Boosted Trees*方式的正确率为86%，其余结果的正确率较低。关于`Fault5`和`Fault6`两种故障的识别，可以不用合并，单独时候也具有较好的分辨率。模型储存在`.\Model\01_17\`文件下，文件名为分类方式，**变量名统一为`Model`**。

##### 2.详细结果分析

1. 实验1

   * 方式：Ensemble-Bagged Tree

   * 准确率：89.8%

   * 详细结果：

     <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220117100321845.png" alt="image-20220117100321845" style="zoom:50%;" />

2. 实验2

   * 方式：Ensemble-Boosted Tree

   * 准确率：86.0%

   * 详细结果：

     <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220117100439068.png" alt="image-20220117100439068" style="zoom:50%;" />

## 数据

* 数据导入方法

  1. 将原始数据按*文件名-数据量-类型-标签*的形式整理（见下原始数据清单）。

  2. 在`.\Data\`路径下以*原始数据类型*为名，建立文件夹，并将此类型原始数据放入相应文件夹中。
  3. 打开`.\ForDataSet_v2.m`文件，将所有*原始数据标签、原始数据类型*分别放入`Labels`、`OriginalDataFileNames`中，然后运行程序。

* 原始数据清单

  * dataset_0105.sxls

    ​	 ***类型**列为故障类型。**标签**列的数字是相应类型故障的标号，对应数据集的label，用于后续训练。*
    
    | 文件名  | 数据量 |   类型   | 标签 | 备注 |
    | :-----: | :----: | :------: | :--: | :--: |
    | COM1111 |   50   | Standard |  0   |      |
    | COM1112 |   25   |  Fault1  |  1   |      |
    | COM1113 |   25   |  Fault2  |  2   |      |
    | COM1121 |   25   |  Fault3  |  3   |      |
    | COM1131 |   25   |  Fault4  |  4   |      |
    | COM1211 |   25   |  Fault5  |  5   |      |
    | COM1311 |   25   |  Fault6  |  6   |      |
    | COM2111 |   25   |  Fault7  |  7   |      |
    | COM3111 |   25   |  Fault8  |  8   |      |
    
  * dataset_0111.sxls

    | 文件名  | 数据量 |   类型   | 标签 | 备注 |
    | :-----: | :----: | :------: | :--: | :--: |
    | COM1111 |  100   | Standard |  0   |      |
    | COM1112 |   50   |  Fault1  |  1   |      |
    | COM1113 |   50   |  Fault2  |  2   |      |
    | COM1121 |   50   |  Fault3  |  3   |      |
    | COM1131 |   50   |  Fault4  |  4   |      |
    | COM1211 |   50   |  Fault5  |  5   |      |
    | COM1311 |   50   |  Fault6  |  6   |      |
    | COM2111 |   50   |  Fault7  |  7   |      |
    | COM3111 |   50   |  Fault8  |  8   |      |

* 数据采集位置图

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220117095941194.png" alt="image-20220117095941194" style="zoom: 200%;" />

* 故障原因参考表

| 水平/因素 | A-预紧力值 | B-结构缺陷 |         C-输入转速         |             D-负载大小             |
| :-------: | :--------: | :--------: | :------------------------: | :--------------------------------: |
|     1     | 标准$650N$ |    标准    |        标准$960$转         |          标准$2065.5N*m$           |
|     2     | 松动$400N$ |    断齿    |        低速$200$转         |          过载$2478.6N*m$           |
|     3     | 过松$150N$ |    脱落    | 波动$940 \thicksim 1060$转 | 波动$2272.05 \thicksim 1858.95N*m$ |

## 故障预测

* 测试数据：初次获得的正常数据，数据路径('.\Data\OrigdataTable.xlsx')，每8192个点为1组数据，取10组数据作为原始数据。

* 数据处理：利用`TypValExt.m`文件对原始数据进行预处理，获取得到特征值。

* 预测：利用`01_05`，`01_11`，`01_17`中的模型分别做预测。

  * 基于`01_05`，`01_11`的模型都只用了一个位置采集的数据（通道2——主机减速箱——base）。特征选取：特征值全用。
  * 基于`01_17`的模型都只用了**两个位置**采集的数据（通道2——主机减速箱——base；通道5——地板螺栓——bolt）。特征选取：除开Nbf的特征值。

  ![image-20220118213202917](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220118213202917.png)

* 预测结果：

  | 日期  |         模型名称          |       预测结果        | 备注 |
  | :---: | :-----------------------: | :-------------------: | :--: |
  | 01_05 |     Tree_Complex_Tree     | [0;0;4;4;0;0;0;4;4;0] |      |
  |       |  Enssemble_Bagged_Trees   | [0;4;4;4;4;4;4;4;4;4] |      |
  | 01_11 |     Tree_Complex_Tree     | [0;0;5;5;0;0;0;5;5;0] |      |
  |       |   Ensemble_Bagged_Trees   | [1;0;4;4;6;1;6;4;4;4] |      |
  | 01_17 | Ensemble_RUSBoosted_Trees | [4;4;5;5;4;4;4;5;5;4] |      |
  |       |   Ensemble_Bagged_Trees   | [6;0;6;6;0;6;0;6;6;6] |      |

## 理论知识

### 集成分类器(Ensemble Classifier)

* 原理：将相互之间具有独立决策能力的分类器联合起来的方式就叫作集成分类器。事实证明通常情况下集成分类器的预测能力要比单个分类器的预测能力好得多。

  ![image-20220124143139716](C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220124143139716.png)

  对于预测数据，集成分类器内部有多个单独的分类器，每个分类器先单独进行预测，每个分类器利用各自的预测结果来**投票**，最后的投票结果就是集成分类器的预测结果。

* 优势：

  * 数据量问题：学习的过程可以看做是在一个假设空间H中寻找一个最优的假设，如果训练集的数据量特别小的时候，由于训练数据不充分，可以学习到很多不同的假设，而这些假设在训练集上的正确率确是相同的，此时就很难抉择哪个假设在测试集上会得到好的结果了。通过集成这些假设就可以减少选错分类器的风险。
  * 计算问题：很多学习算法都会遇到局部最优的这种状况，例如，神经网络是通过梯度下降来最小化错误率的，决策树算法是通过贪婪分裂的规则去扩展决策树的，假如训练集是足够充足的，学习算法也很难得到全局最优解的。通过把从不同起始点得到的分类器集成的方法可以更好的靠近全局最优解。
  * 假设描述问题：大部分的应用中，机器学习算法很难用假设空间H中的假设去表达真实函数f，通过对假设空间H中的假设进行加权进而扩展假设空间H的规模或许能够表示真实函数f。

* 方法：

  1. Bagging方法
     * 基本思路：
       1. 利用boostrap方法抽取n个训练样本，样本可能被重复抽到。然后再进行k轮抽取，得到k个训练集，他们之间相互独立。
       2. k个训练集共训练了k个模型，具体使用什么算法，视具体的场景而定。
       3. 分类问题，k个模型得到的结果，采用投票的方式；回归问题：计算平均值。

  2. Boost方法
     * 基本思路：采用重赋权法迭代训练分类器。对每一轮样本权值分布依赖上一次的训练结果，产生误差越大的样本，所赋的权重越高。分类器之间采用序列式的线性加权方式进行组合。
  3. Bagging方法与Boost方法的区别
     * 样本选择：bagging在原始集上有放回选取，样本之间独立；boosting由于每个样本权重要改变因此每一轮训练集不变，以便赋值不同权重。
     * 样本权重：bagging中均匀取样，样本权重相等；boosting错误率越大权重越大。
     * 预测函数：bagging所有预测函数权重相等；boosting每个弱分类有相应权重，分类误差小的分类器有更大的权重。
     * 并行计算：bagging各个预测函数并行生成；boosting中预测函数顺序生成，因为结果有依赖关系。

* 参考：[参考1]([(1条消息) 为什么要集成分类器_lidoublewen的专栏-CSDN博客_集成分类器](https://blog.csdn.net/lidoublewen/article/details/6601137)) [参考2]([(1条消息) 机器学习(4)分类之集成方法_hcyxy-CSDN博客_分类器集成](https://blog.csdn.net/u011730199/article/details/78207535?spm=1001.2101.3001.6650.1&utm_medium=distribute.pc_relevant.none-task-blog-2~default~CTRLIST~Rate-1.pc_relevant_paycolumn_v3&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2~default~CTRLIST~Rate-1.pc_relevant_paycolumn_v3&utm_relevant_index=2)) [参考3]([Ensemble Classifier | Data Mining - GeeksforGeeks](https://www.geeksforgeeks.org/ensemble-classifier-data-mining/))

