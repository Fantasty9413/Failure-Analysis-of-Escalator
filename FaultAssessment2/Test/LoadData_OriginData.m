%% 导入电子表格中的数据
% 用于从以下电子表格导入数据的脚本:
%
%    工作簿: E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx
%    工作表: OrigdataTable
%
% 要扩展代码以供其他选定数据或其他电子表格使用，请生成函数来代替脚本。

% 由 MATLAB 自动生成于 2022/01/18 17:25:24

%% 导入数据
[~, ~, raw0_0] = xlsread('E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx','OrigdataTable','E2:E93697');
[~, ~, raw0_1] = xlsread('E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx','OrigdataTable','F2:F93697');
[~, ~, raw0_2] = xlsread('E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx','OrigdataTable','I2:I93697');
raw = [raw0_0,raw0_1,raw0_2];
raw(cellfun(@(x) ~isempty(x) && isnumeric(x) && isnan(x),raw)) = {''};

%% 将非数值元胞替换为 NaN
R = cellfun(@(x) ~isnumeric(x) && ~islogical(x),raw); % 查找非数值元胞
raw(R) = {NaN}; % 替换非数值元胞

%% 创建输出变量
data = reshape([raw{:}],size(raw));

%% 将导入的数组分配给列变量名称
dataAi1 = data(:,1);
dataAi2 = data(:,2);
dataAi5 = data(:,3);

%% 清除临时变量
clearvars data raw raw0_0 raw0_1 raw0_2 R;