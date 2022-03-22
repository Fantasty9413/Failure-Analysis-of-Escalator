function [ time, amplitude ] = LoadDataFromTab( datapath )
%UNTITLED2 此处显示有关此函数的摘要
%   此处显示详细说明
%% 初始化变量。
% filename = 'E:\Code_master\Risk Assessment of Escalator\FaultAssessment\Data\DE3_base_m.tab';
filepath = 'E:\Code_master\Risk Assessment of Escalator\FaultAssessment2\';
filename = strcat(filepath, datapath);
delimiter = '\t';
startRow = 6;

%% 每个文本行的格式:
%   列1: 双精度值 (%f)
%	列2: 双精度值 (%f)
% 有关详细信息，请参阅 TEXTSCAN 文档。
formatSpec = '%f%f%[^\n\r]';

%% 打开文本文件。
fileID = fopen(filename,'r');

%% 根据格式读取数据列。
% 该调用基于生成此代码所用的文件的结构。如果其他文件出现错误，请尝试通过导入工具重新生成代码。
textscan(fileID, '%[^\n\r]', startRow-1, 'WhiteSpace', '', 'ReturnOnError', false, 'EndOfLine', '\r\n');
dataArray = textscan(fileID, formatSpec, 'Delimiter', delimiter, 'EmptyValue' ,NaN,'ReturnOnError', false);

%% 关闭文本文件。
fclose(fileID);

%% 对无法导入的数据进行的后处理。
% 在导入过程中未应用无法导入的数据的规则，因此不包括后处理代码。要生成适用于无法导入的数据的代码，请在文件中选择无法导入的元胞，然后重新生成脚本。

%% 将导入的数组分配给列变量名称
time = dataArray{:, 1};
amplitude = dataArray{:, 2};

%% 清除临时变量
clearvars filename delimiter startRow formatSpec fileID dataArray ans;

end

