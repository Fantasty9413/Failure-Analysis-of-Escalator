function [ Flag, Label ] = WorkConditionClassifier_Complicated( TypicalValue )
%
%   WorkConditionClassufier_motot: use model to classify work condition
%   with complicated model
%   
%   Input:
%       TypicalValue: typical value array(just row)
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

ModelName = 'complicated';

[ Flag, Label ] = WorkConditionClassifier( TypicalValue, ModelName );

end

