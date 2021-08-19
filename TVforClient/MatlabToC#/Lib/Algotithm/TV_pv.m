function [ pv_max, t_max, pv_min, t_min ] = TV_pv( TD_amp, TD_t )
%   TV_pv: get the peak value(pv) of typical value(TV)
%   Inputs:
%       TD_amp: amplitude of data struct of time domain(TD) signal
%       TD_t:   time of data struct of time domain(TD) signal
%   Outputs:
%       pv_max: max value of peak value
%       t_max:  time of pv_max
%       pv_min: min value of peak value
%       t_min:  time of pv_min

[pv_max, positon_max] = max(TD_amp);
t_max = TD_t(positon_max);
[pv_min, positon_min] = min(TD_amp);
t_min = TD_t(positon_min);

end

