function [ pvifds_amp, pvifds_f ] = TV_pvifds( FD_amp, FD_f )
%   TV_pvifds: pv in fd segment, get pv of acceleration in low, mid or high
%   frequency segment
%   Inputs:
%       FD_amp: amplitude of FD data
%       FD_f:   frequency of FD data
%   Outputs:
%       pvifds_amp:  pv value of acceleration in low, mid and high segment
%       pvifds_f:    frequency corresponding of pv

L = length(FD_amp);        % length of FD.data
L_segment = floor(L/3);     % length of segment(low frequency, mid frequency and high frequency)

pvifds_amp = zeros(1,3);
pvifds_f = zeros(1,3);

for index = 1:1:3
    s_start = (index-1)*L_segment + 1;  % the start index of this segment
    s_end = index*L_segment;            % the end index of this segment

    pvifds_amp(index) = max(FD_amp(s_start:s_end));
    pvifds_f(index) = FD_f(find(FD_amp(s_start:s_end) == pvifds_amp(index)) + s_start - 1);
end

end

