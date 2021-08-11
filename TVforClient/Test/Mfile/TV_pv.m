function [ pv_max, t_max, pv_min, t_min ] = TV_pv( TD )
%   TV_pv: get the peak value(pv) of typical value(TV)
%   Inputs:
%       TD: data struct of time domain(TD) signal
%   Outputs:
%       pv_max: max value of peak value
%       t_max:  time of pv_max
%       pv_min: min value of peak value
%       t_min:  time of pv_min

[pv_max, positon_max] = max(TD.data);
t_max = TD.t(positon_max);
[pv_min, positon_min] = min(TD.data);
t_min = TD.t(positon_min);

end

