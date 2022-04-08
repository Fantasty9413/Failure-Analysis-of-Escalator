function [ Flag, Label ] = WorkConditionClassifier( TypicalValue, ModelName )
%
%   WorkConditionClassufier: use model to classify work condition
%   
%   Input:
%       TypicalValue: typical value array(just row)
%       ModelName: classifier model suffix name
%
%   Output:
%       Flag: file work flag
%           Flag = 1: success to obtain classify result
%           Flag = -1: model name error
%           Flag = -2: model loading failure
%           Flag = -3: tv data error
%           Flag = -4: classifier error
%       Label: work condition label
%

tv = TypicalValue;

TabelLabels_1 = {'pv_max', 't_max', 'pv_min', 't_min', 'kv', 'ppv', 'tfv', 'Nbf_1',...     % typical value
                 'Nbf_2', 'Nbf_3', 'pvifds_amp_1', 'pvifds_amp_2', 'pvifds_amp_3',...
                 'pvifds_f_1', 'pvifds_f_2', 'pvifds_f_3'};
             
TabelLabels_2 = {'base_pv_max', 'base_t_max', 'base_pv_min', 'base_t_min', 'base_kv', 'base_ppv', 'base_tfv', 'base_Nbf_1',...     % typical value
                 'base_Nbf_2', 'base_Nbf_3', 'base_pvifds_amp_1', 'base_pvifds_amp_2', 'base_pvifds_amp_3',...
                 'base_pvifds_f_1', 'base_pvifds_f_2', 'base_pvifds_f_3', ...
                 'bolt_pv_max', 'bolt_t_max', 'bolt_pv_min', 'bolt_t_min', 'bolt_kv', 'bolt_ppv', 'bolt_tfv', 'bolt_Nbf_1',...     
                 'bolt_Nbf_2', 'bolt_Nbf_3', 'bolt_pvifds_amp_1', 'bolt_pvifds_amp_2', 'bolt_pvifds_amp_3',...
                 'bolt_pvifds_f_1', 'bolt_pvifds_f_2', 'bolt_pvifds_f_3'};

ModelList = {'motor', 'reducer', 'bolt', 'complicated'};

Index = find(ismember(ModelList, ModelName));      % find model index

%% 1.check ModelName
if(isempty(Index))
   % model name error, return flag = -1;
   Flag = -1;
   Label = -1;
   return;
end

%% 2.load classfier model
ModelSuffix = cell2mat(ModelList(Index));
% CM_Path = '../Train/trainedClassifier/';
CM_Path = './trainedClassifier/';
CM_Name = strcat('trainedClassifier_', ModelSuffix);
CM_Format = '.mat';
Path = strcat(CM_Path, CM_Name, CM_Format);
try
    %#function ClassificationECOC
    load(Path);
    eval(['ClassiferModel = ', CM_Name, ';']);
catch
    % model loading failure, return flag = -2;
    Flag = -2;
    Label = -1;
    return;
end

%% 3.creat data table
T = [];
switch Index
    case 1
        datalength = 16;
        if(length(tv) ~= datalength)
            % tv data error, return flag = -3;
            Flag = -3;
            Label = -1;
            return;
        else
            T = array2table(tv, 'VariableNames', TabelLabels_1);
        end
    case 2
        datalength = 16;
        if(length(tv) ~= datalength)
            % tv data error, return flag = -3;
            Flag = -3;
            Label = -1;  
            return;
        else
            T = array2table(tv, 'VariableNames', TabelLabels_1);
        end
    case 3
        datalength = 16;
        if(length(tv) ~= datalength)
            % tv data error, return flag = -3;
            Flag = -3;
            Label = -1;
            return;
        else
            T = array2table(tv, 'VariableNames', TabelLabels_1);
        end
    case 4
        datalength = 32;
        if(length(tv) ~= datalength)
            % tv data error, return flag = -3;
            Flag = -3;
            Label = -1;
            return;
        else
            T = array2table(tv, 'VariableNames', TabelLabels_2);
        end
end

%% 4.classify work condition
try
    result = ClassiferModel.predictFcn(T);
catch
    % classifier error, return flag = -4;
    Flag = -4;
    Label = -1;
    return;    
end

% label alignment
% switch Index
%     case 1
%         result  = result;
%     case 2
%         result  = result - 3;
%     case 3
%         result  = result - 8;
%     case 4
%         result  = result;
% end

Flag = 1;
Label = result;

end % function WorkConditionClassifier

