function [  ] = TypValSave(  )
%UNTITLED 此处显示有关此函数的摘要
%   此处显示详细说明
%% 0.File Infor
% excel file path
FilePath = '..\Data\';
ExcelFileName = 'DataSet.xlsx';
ExcelFilePath = strcat(FilePath, ExcelFileName);

% creat excel label
TypValType = {'pv_max', 't_max', 'pv_min', 't_min', 'kv', 'ppv', 'tfv', 'Nbf_1',...     % typical value
              'Nbf_2', 'Nbf_3', 'pvifds_amp_1', 'pvifds_amp_2', 'pvifds_amp_3',...
              'pvifds_f_1', 'pvifds_f_2', 'pvifds_f_3'};
ExcelLabel = TypValType;
ExcelLabel{end+1} = 'datalabel';

% initial excel file
filepath = ExcelFilePath;        % excel file path
WriteRange = 'A1';
WriteData = ExcelLabel;
WriteSheet = 1;
xlswrite(filepath, WriteData, WriteSheet, WriteRange);
WriteSheet = 2;
xlswrite(filepath, WriteData, WriteSheet, WriteRange);

%% 1.Motor part
% motor part, save data to excel sheet1
sheetnum = 1;

% 
SenPos = 'motor';
Path_Tab = strcat(SenPos, '_m', '.tab');
Path_DEs = {};      % DE file path
for i = 1:1:50
    Path_DEs(1, i) = cellstr(strcat('de', num2str(i)));
end
Path_WCs = {'WorkCondition_1', 'WorkCondition_2', 'WorkCondition_3'};       % WorkCondition file path
DataLabels = [1, 2, 3]; % label for data

for index1 = 1:length(Path_WCs)    
   for index2 = 1:length(Path_DEs)
       % read data from tab file
       tabfilepath = strcat('Data\', cell2mat(Path_WCs(index1)), '\', cell2mat(Path_DEs(index2)), '\',  Path_Tab);
       [time, amp] = LoadDataFromTab(tabfilepath);      % load original data
       
       % extract typical value
       tv = TypValExt(time, amp);
       
       % construct data set
       datalabel = DataLabels(index1);
       data = [tv, datalabel];
       
       % data save
       filepath = ExcelFilePath;                  % excel file path
       [~,~,raw] = xlsread(filepath, sheetnum);   % read excel
       [row, ~] = size(raw);                      % find latest row to write data
       WriteSheet = sheetnum;
       WriteRow = row + 1;
       WriteRange = strcat('A', num2str(WriteRow));
       WriteData = data;
       xlswrite(filepath, WriteData, WriteSheet, WriteRange);       
   end
end

%% 2.Signal part
% reducer part, save data to excel sheet2
sheetnum = 2;

% 
SenPos = 'signal';
Path_Tab = strcat(SenPos, '_m', '.tab');
Path_DEs = {};      % DE file path
for i = 1:1:50
    Path_DEs(1, i) = cellstr(strcat('de', num2str(i)));
end
Path_WCs = {'WorkCondition_4', 'WorkCondition_5', 'WorkCondition_6', ...       % WorkCondition file path
            'WorkCondition_7', 'WorkCondition_8'};
DataLabels = [4, 5, 6, 7, 8]; % label for data

for index1 = 1:length(Path_WCs)    
   for index2 = 1:length(Path_DEs)
       % read data from tab file
       tabfilepath = strcat('Data\', cell2mat(Path_WCs(index1)), '\', cell2mat(Path_DEs(index2)), '\',  Path_Tab);
       [time, amp] = LoadDataFromTab(tabfilepath);      % load original data
       
       % extract typical value
       tv = TypValExt(time, amp);
       
       % construct data set
       datalabel = DataLabels(index1);
       data = [tv, datalabel];
       
       % data save
       filepath = ExcelFilePath;                  % excel file path
       [~,~,raw] = xlsread(filepath, sheetnum);   % read excel
       [row, ~] = size(raw);                      % find latest row to write data
       WriteSheet = sheetnum;
       WriteRow = row + 1;
       WriteRange = strcat('A', num2str(WriteRow));
       WriteData = data;
       xlswrite(filepath, WriteData, WriteSheet, WriteRange);       
   end
end

end

