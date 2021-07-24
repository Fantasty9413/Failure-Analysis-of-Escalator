function [ pv, f ] = TV_pvifds( FD, fds )
%   TV_pvifds: pv in fd segment, get pv of acceleration in low, mid or high
%   frequency segment
%   Inputs:
%       FD:  data struct of frequency domain(FD) signal
%       fds: option of chose low, mid or high segment
%   Outputs:
%       pv:  pv value of acceleration in specified segment
%       f:   frequency corresponding of pv

keyword = {'low', 'mid', 'high'};       % option of fds

index = find(strcmp(keyword,fds) == 1); % index of fds in keyword

L = length(FD.data);        % length of FD.data
L_segment = floor(L/3);     % length of segment(low frequency, mid frequency and high frequency)

s_start = (index-1)*L_segment + 1;  % the start index of this segment
s_end = index*L_segment;            % the end index of this segment

pv = max(FD.data(s_start:s_end));
f = FD.f(find(FD.data(s_start:s_end) == pv));

end

