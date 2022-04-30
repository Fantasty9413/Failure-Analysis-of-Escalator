function [ Flag, Grade ] = WorkConditionRating_Bolt( Label )
%
%   WorkConditionRating: work condition rating method
%   
%   Input:
%       Label: the prediction result from the corresponding classifier
%       model with bolt model
%
%   Output:
%       Flag: file work flag
%           Flag = 1: success to obtain grade result
%           Flag = -1: model name error
%           Flag = -2: label can't match classifier model
%       Grade: work condition grade
%

ModelName = 'bolt';

[ Flag, Grade ] = WorkConditionRating( Label, ModelName );

end

