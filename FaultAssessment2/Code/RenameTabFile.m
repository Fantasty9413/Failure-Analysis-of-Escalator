%
%   rename the file in WorkCondition1
%   rename 'motor_*.tab' to 'signal_*.tab'
%
clear all;

cdc = pwd;

SenPos = 'motor';
Path_Tab = strcat(SenPos, '_m', '.tab');
Path_DEs = {};      % DE file path
for i = 1:1:50
    Path_DEs(1, i) = cellstr(strcat('de', num2str(i)));
end
Path_WCs = {'WorkCondition_1'};  

for index1 = 1:length(Path_WCs)    
   for index2 = 1:length(Path_DEs)
       % cd to work directory
       cd(cdc);
       tabfilepath = strcat('..\Data\', cell2mat(Path_WCs(index1)), '\', cell2mat(Path_DEs(index2)), '\');
       cd(tabfilepath)
       
       % rename file
       file = dir('*.tab');
       N = length(file);
       for index3 = 1:N
          oldname = file(index3).name;
          newname = strrep(oldname, 'motor', 'signal');     % creat new file name
          eval(['!rename' 32 oldname 32 newname]);          % rename
       end
       cd(cdc);
   end
end
