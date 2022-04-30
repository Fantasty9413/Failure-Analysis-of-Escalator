%
%
%
%% 1.test WorkConditionRating
Label = 1;
ModelName = 'complicated';
[ Flag, Grade ] = WorkConditionRating( Label, ModelName )

Grades = [100, 80, 80];
Weight = [1, 1, 1];
[Flag, Score] = HealthScoreRating( Grades, Weight );