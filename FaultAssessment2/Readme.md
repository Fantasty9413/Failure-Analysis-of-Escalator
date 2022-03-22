# 故障分析文档

## 数据

* 原始数据信息：新的仿真数据针对**电机、减速箱轴承、齿轮**三个部件。

  * 各部件工况对应情况：

    |             电机              |         减速箱轴承         |           齿轮           |
    | :---------------------------: | :------------------------: | :----------------------: |
    |     正常：1DEnorMotorSim      |    正常：4DEnorGearSim     |   正常：4DEnorGearSim    |
    | 偏心（1.5mm）：2DEoffMotorsim | 滚动体缺失：7DElackBearSim | 齿轮磨损：5DEwearGearSim |
    | 电机轴承磨损：3DEbearMotorSim | 滚动体磨损：8DEwearBearSim | 齿隙扩大：6DEwideGearSim |

  * 各部件数据测量位置：

    | 序号 |    部件    | 数据采集位置 | 对应实际传感器通道 | 实际传感器位置 |
    | :--: | :--------: | :----------: | :----------------: | :------------: |
    |  1   |    电机    |   电机外壳   |      channel1      |    主机电机    |
    |  2   | 减速箱轴承 |  减速箱外壳  |      channel2      |   主机减速箱   |
    |  3   | 减速箱齿轮 |  减速箱外壳  |      channel2      |   主机减速箱   |

  * 原始数据信息：

    |     工况编号     |       实际工况       |      数据存放路径       | 数据量 | 标签号 |           备注           |
    | :--------------: | :------------------: | :---------------------: | :----: | :----: | :----------------------: |
    | Work Condition 1 |       电机正常       | `.\Data\WorkCondition1` |   50   |   1    | 数据名统一为`signal`前缀 |
    | Work Condition 2 |       电机偏心       | `.\Data\WorkCondition2` |   50   |   2    |   ~~数据全相同待修正~~   |
    | Work Condition 3 |     电机轴承磨损     | `.\Data\WorkCondition3` |   50   |   3    |   ~~数据全相同待修正~~   |
    | Work Condition 4 |      减速箱正常      | `.\Data\WorkCondition4` |   50   |   4    |                          |
    | Work Condition 5 |    减速箱齿轮磨损    | `.\Data\WorkCondition5` |   50   |   5    |                          |
    | Work Condition 6 |    减速箱齿隙扩大    | `.\Data\WorkCondition6` |   50   |   6    |                          |
    | Work Condition 7 | 减速箱轴承滚动体缺失 | `.\Data\WorkCondition7` |   50   |   7    |                          |
    | Work Condition 8 | 减速箱轴承滚动体磨损 | `.\Data\WorkCondition8` |   50   |   8    |                          |

## 代码

## 训练

按照电机、减速箱两大部件分别训练模型进行故障检测。

### 测试

* 0319测试

  * 针对减速箱的故障分类器的训练

    * 数据：Work Condition 4~8

    * 结果：效果较为理想，大部分分类器都可以做到90%以上的正确率，其中又以Ensemble Bagged Trees-99.6%和SVM Linear SVM--99.6%的正确率最高。

    * 详细结果：

      * Ensemble Bagged Trees:

        <img src="C:\Users\96919\AppData\Roaming\Typora\typora-user-images\image-20220319202703752.png" alt="image-20220319202703752" style="zoom:50%;" />

