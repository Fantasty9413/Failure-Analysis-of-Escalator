%
%   1. creat work condition rating table
%   2. the save path of rating table is './WorkConditionRatingTable/' 
%   3. the rating critics follows the rule in readme
%

clear all;
TableSavePath = './WorkConditionRatingTable/';

% work condition rating table - motor
Labels = 1:1:3;
Grades = [100, 80, 60];
ModelName = 'motor';
TableName = strcat('WorkConditionRatingTable', '_', ModelName);
FileName = strcat(TableSavePath, TableName, '.mat');
save(FileName, 'Labels', 'Grades');

% work condition rating table - reducer
Labels = 4:1:8;
Grades = [100, 80, 80, 60, 60];
ModelName = 'reducer';
TableName = strcat('WorkConditionRatingTable', '_', ModelName);
FileName = strcat(TableSavePath, TableName, '.mat');
save(FileName, 'Labels', 'Grades');

% work condition rating table - bolt
Labels = 9:1:11;
Grades = [100, 80, 60];
ModelName = 'bolt';
TableName = strcat('WorkConditionRatingTable', '_', ModelName);
FileName = strcat(TableSavePath, TableName, '.mat');
save(FileName, 'Labels', 'Grades');

% work condition rating table - complicated
Labels = 0:1:8;
Grades = [100, 100, 100, 100, 100, 80, 60, 80, 60];
ModelName = 'complicated';
TableName = strcat('WorkConditionRatingTable', '_', ModelName);
FileName = strcat(TableSavePath, TableName, '.mat');
save(FileName, 'Labels', 'Grades');
