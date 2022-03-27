clear all;

%% 0.import data
run ('LoadData_OriginData.m');      % Load original data

% signal value and parameter in time domain
N = 10;                     % quantity of data
Fs = 10^4;        % sample frequency
L = 8192;         % data length

OD_amplitude_motor = dataAi1;
OD_time_motor = (1:1:length(OD_amplitude_motor)) / Fs;

OD_amplitude_reducer = dataAi2;
OD_time_reducer = (1:1:length(OD_amplitude_reducer)) / Fs;

OD_amplitude_bolt = dataAi5;
OD_time_bolt = (1:1:length(OD_amplitude_bolt)) / Fs;

OD_time_testdata = OD_time_bolt;
OD_amplitude_testdata = OD_amplitude_bolt;

time = [];
amp = [];

for i = 1:1:N
   time(i, :) = OD_time_testdata(1+(i-1)*L : i*L);
   amp(i, :) = OD_amplitude_testdata(1+(i-1)*L : i*L);
end

%% 1.preprocessing
n = 2;
tv = TypValExt(time(n, :), amp(n, :));

TypVal = {'pv_max', 't_max', 'pv_min', 't_min', 'kv', 'ppv', 'tfv', 'Nbf_1',...     % typical value
          'Nbf_2', 'Nbf_3', 'pvifds_amp_1', 'pvifds_amp_2', 'pvifds_amp_3',...
          'pvifds_f_1', 'pvifds_f_2', 'pvifds_f_3'};
      
for i = 1:1:length(TypVal)
    eval([cell2mat(TypVal(i)), '=', 'tv(i)', ';']);
end

T = table(pv_max, t_max, pv_min, t_min, kv, ppv, tfv, Nbf_1,...     % typical value
          Nbf_2, Nbf_3, pvifds_amp_1, pvifds_amp_2, pvifds_amp_3,...
          pvifds_f_1, pvifds_f_2, pvifds_f_3);

% %% 2.load classifer model
% PartName = 'bolt';
% CM_Path = '../Train/trainedClassifier/';
% CM_Name = strcat('trainedClassifier_', PartName);
% CM_Format = '.mat';
% Path = strcat(CM_Path, CM_Name, CM_Format);
% load(Path);
% eval(['ClassiferModel = ', CM_Name, ';']);
% 
% %% 3.prediction
% result = ClassiferModel.predictFcn(T);

%%
tv = table2array(T);
% tv = [tv, tv];
modelname = 'bolt';
[flag, label] = WorkConditionClassifier(tv, modelname);