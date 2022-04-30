function [ Flag, Grade ] = WorkConditionRating( Label, ModelName )
%
%   WorkConditionRating: work condition rating method
%   
%   Input:
%       Label: the prediction result from the corresponding classifier model
%       ModelName: the corresponding classifier model suffix name
%
%   Output:
%       Flag: file work flag
%           Flag = 1: success to obtain grade result
%           Flag = -1: model name error
%           Flag = -2: label can't match classifier model
%       Grade: work condition grade
%

%% 0.checking input
ModelList = {'motor', 'reducer', 'bolt', 'complicated'};
Index = find(ismember(ModelList, ModelName));      % find model index
if(isempty(Index))
   % model name error, return flag = -1;
   Flag = -1;
   Grade = 0;
   return;
end

%% 1.load work condition rating table
TableSavePath = './WorkConditionRatingTable/';
TableName = strcat('WorkConditionRatingTable', '_', ModelName);
FileName = strcat(TableSavePath, TableName, '.mat');
load(FileName);

%% 2.get grade
index = find(Label == Labels);

if(index < 0)
   % label can't match classifier model, return flag = -2;
   Flag = -2;
   Grade = 0;
   return;
end

Flag = 1;
Grade = Grades(index);

end

