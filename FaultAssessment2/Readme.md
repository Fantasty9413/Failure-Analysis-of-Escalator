# 故障分析文档

## 数据

* 原始数据信息：新的仿真数据针对**电机、减速箱轴承、齿轮**三个部件。

  * 各部件工况对应情况：

    |             电机              |         减速箱轴承         |           齿轮           |           地脚螺栓            |
    | :---------------------------: | :------------------------: | :----------------------: | :---------------------------: |
    |     正常：1DEnorMotorSim      |    正常：4DEnorGearSim     |   正常：4DEnorGearSim    | 正常：Bolt1DENorm（0327新增） |
    | 偏心（1.5mm）：2DEoffMotorsim | 滚动体缺失：7DElackBearSim | 齿轮磨损：5DEwearGearSim |  松动：Bolt2DEA（0327新增）   |
    | 电机轴承磨损：3DEbearMotorSim | 滚动体磨损：8DEwearBearSim | 齿隙扩大：6DEwideGearSim |  过松：Bolt3DEB（0327新增）   |

  * 各部件数据测量位置：

    | 序号 |    部件    | 数据采集位置 | 对应实际传感器通道 | 实际传感器位置 |
    | :--: | :--------: | :----------: | :----------------: | :------------: |
    |  1   |    电机    |   电机外壳   |      channel1      |    主机电机    |
    |  2   | 减速箱轴承 |  减速箱外壳  |      channel2      |   主机减速箱   |
    |  3   | 减速箱齿轮 |  减速箱外壳  |      channel2      |   主机减速箱   |
    |  4   |  地脚螺栓  |   地板螺栓   |      channel5      |    地板螺栓    |
  
  * 原始数据信息：
  
    |     工况编号      |       实际工况       |       数据存放路径       | 数据量 | 标签号 |                备注                 |
    | :---------------: | :------------------: | :----------------------: | :----: | :----: | :---------------------------------: |
    | Work Condition 1  |       电机正常       | `.\Data\WorkCondition1`  |   50   |   1    |      数据名统一为`signal`前缀       |
    | Work Condition 2  |       电机偏心       | `.\Data\WorkCondition2`  |   50   |   2    |        ~~数据全相同待修正~~         |
    | Work Condition 3  |     电机轴承磨损     | `.\Data\WorkCondition3`  |   50   |   3    |        ~~数据全相同待修正~~         |
    | Work Condition 4  |      减速箱正常      | `.\Data\WorkCondition4`  |   50   |   4    |                                     |
    | Work Condition 5  |    减速箱齿轮磨损    | `.\Data\WorkCondition5`  |   50   |   5    |                                     |
    | Work Condition 6  |    减速箱齿隙扩大    | `.\Data\WorkCondition6`  |   50   |   6    |                                     |
    | Work Condition 7  | 减速箱轴承滚动体缺失 | `.\Data\WorkCondition7`  |   50   |   7    |                                     |
    | Work Condition 8  | 减速箱轴承滚动体磨损 | `.\Data\WorkCondition8`  |   50   |   8    |                                     |
    | Work Condition 9  |      螺栓正常力      | `.\Data\WorkCondition9`  |   50   |   9    | 0327新增，有100组数据，目前只取50组 |
    | Work Condition 10 |       螺栓松动       | `.\Data\WorkCondition10` |   50   |   10   |                                     |
    | Work Condition11  |       螺栓过松       | `.\Data\WorkCondition11` |   50   |   11   |                                     |

## 代码

## 训练

按照电机、减速箱、地脚螺栓三大部件分别训练模型进行故障检测。

### 测试

* 0319测试

  * 针对减速箱的故障分类器的训练

    * 数据：Work Condition 4~8

    * 结果：效果较为理想，大部分分类器都可以做到90%以上的正确率，其中又以Ensemble Bagged Trees-99.6%和SVM Linear SVM--99.6%的正确率最高。

    * 详细结果：

      * Ensemble Bagged Trees:

        <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220319202703752.png" alt="image-20220319202703752" style="zoom:50%;" />

* 0327测试

  * 针对电机的故障分类器的训练

    * 数据：Work Condition 1~3

    * 结果：效果较为理想，大部分分类器都可以做到90%以上的正确率，其中又以Quadratic SVM--100%和Fine KNN--100%的正确率最高。

    * 详细结果：

      * Quadratic SVM:

        <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327120208870.png" alt="image-20220327120208870" style="zoom:50%;" />

      * Fine KNN：

        <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327120255495.png" alt="image-20220327120255495" style="zoom:50%;" />

  * 针对减速箱的故障分类器训练

    * 同0319的结果。

  * 针对地板螺栓的故障分类器训练

    * 数据：Work Condition 9~11

    * 结果：效果不太理想，大部分分类器都30%--50%的正确率，其中又以Simple Tree--61.3%正确率最高。

    * 详细结果：

      * Simple Trees:

        <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327115737027.png" alt="image-20220327115737027" style="zoom:50%;" />

    * 改进：

      * 改进方案1：增加数据量，重新训练。

        * 实验：效果不理想，假发数据量后训练模型的正确率最高为Fine Gaussian SVM -- 68.7%，并无较大提升。分析结果，这一现象的原因是特征值差异较小，无法区分。

          <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327143559297.png" alt="image-20220327143559297" style="zoom:50%;" />

      * 改进方案2：加入之前的模型，结合两个模型的结果来给出Bolt的工况判定结果。

## 结果

### 1.背景说明
**采用多模型策略**来减少耦合，降低差错率。即采用多个模型来实现不同部件的工况预测。电机工况预测对应一个模型、减速箱工况预测对应一个模型、地脚螺栓工况对应一个模型。每个模型的训练数据自单独来自其对应部件传感器采集到的数据。

### 2.结果获取

参照上述测试案例的过程和结果，获取**模型训练器**（trainclassifer）和**训练模型**（trainedclassifer）作为结果进行保存，存放路径为`./Train/`。

### 3.结果信息

#### 3.1motor：

* 模型：SVM Quadratic SVM

* 正确率：100%

* Confusion Matrix：

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327162558674.png" alt="image-20220327162558674" style="zoom:50%;" />

* Parallel Coordinates Plot：

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327162621029.png" alt="image-20220327162621029" style="zoom:33%;" />

#### 3.2reducer

* 模型：SVM Linear SVM

* 正确率：99.6%

* Confusion Matrix：

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327164655532.png" alt="image-20220327164655532" style="zoom:50%;" />

* Parallel Coordinates Plot：

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327164736750.png" alt="image-20220327164736750" style="zoom: 33%;" />

#### 3.3bolt

* 模型：Tree Simple Tree

* 正确率：63.3%

* Confusion Matrix：

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327170559335.png" alt="image-20220327170559335" style="zoom:50%;" />

* Parallel Coordinates Plot：

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327170639779.png" alt="image-20220327170639779" style="zoom: 33%;" />

## 附录

### 1.训练模型信息（全）

* motor

<img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327165218887.png" alt="image-20220327165218887" style="zoom:25%;" />

* reducer

<img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327165120401.png" alt="image-20220327165120401" style="zoom:25%;" />

* bolt

<img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327165441393.png" alt="image-20220327165441393" style="zoom:25%;" />

* complicated

<img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327205939438.png" alt="image-20220327205939438" style="zoom: 25%;" />

### 2.替补修正方案

由于多模型策略对应bolt处的工况判断效果较差，**因此加入FaultAssessment_v1中的正确率最高的模型用于替补修正**。

替补修正模型利用FaultAssessment_v1得到的数据进行训练的（数据存放地址`./Data/DataSet_ComplicatedModel.xlsx`）。训练后选区的分类器方式为Ensemble-Bagged-Trees，其正确率为85.6%，导出相应结果。并将模型命名为`trainedClassifier_complicated`，模型训练器命名为`trainClassifier_complicated`。

**此模型需要同时使用reducer处和bolt处测得数据。**

* 模型：Ensemble Bagged Trees

* 正确率：85.6%

* Confusion Matrix：

  <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327210539197.png" alt="image-20220327210539197" style="zoom:50%;" />

* Parallel Coordinates Plot：

<img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220327210622247.png" alt="image-20220327210622247" style="zoom:25%;" />
