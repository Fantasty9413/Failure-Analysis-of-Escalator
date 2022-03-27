%% ������ӱ���е�����
% ���ڴ����µ��ӱ�������ݵĽű�:
%
%    ������: E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx
%    ������: OrigdataTable
%
% Ҫ��չ�����Թ�����ѡ�����ݻ��������ӱ��ʹ�ã������ɺ���������ű���

% �� MATLAB �Զ������� 2022/01/18 17:25:24

%% ��������
[~, ~, raw0_0] = xlsread('E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx','OrigdataTable','E2:E93697');
[~, ~, raw0_1] = xlsread('E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx','OrigdataTable','F2:F93697');
[~, ~, raw0_2] = xlsread('E:\Code_master\Risk Assessment of Escalator\Data\OrigdataTable.xlsx','OrigdataTable','I2:I93697');
raw = [raw0_0,raw0_1,raw0_2];
raw(cellfun(@(x) ~isempty(x) && isnumeric(x) && isnan(x),raw)) = {''};

%% ������ֵԪ���滻Ϊ NaN
R = cellfun(@(x) ~isnumeric(x) && ~islogical(x),raw); % ���ҷ���ֵԪ��
raw(R) = {NaN}; % �滻����ֵԪ��

%% �����������
data = reshape([raw{:}],size(raw));

%% ����������������б�������
dataAi1 = data(:,1);
dataAi2 = data(:,2);
dataAi5 = data(:,3);

%% �����ʱ����
clearvars data raw raw0_0 raw0_1 raw0_2 R;