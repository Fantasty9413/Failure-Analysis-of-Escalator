function [ Flag, Score ] = HealthScoreRating( Grades, Weight )
%
%   HealthScoreRating: health score rating method
%   
%   Input:
%       Grades: the grades for three parts obtained from work condition
%           rating methods
%       Weight: the weights for each grade to each part
%
%   Output:
%       Flag: file work flag
%           Flag = 1: success to obtain score result
%           Flag = -1: input error
%       Score: final health score
%

%% 0.checking input
if(length(Grades)<3 || length(Weight)<3)
   % input error
   Flag = -1;
   Score = 0; 
   return;
end

%% 2.obtain score
Score = sum((Grades(1:3) .* Weight(1:3))) / sum(Weight(1:3));
Flag = 1;

end

