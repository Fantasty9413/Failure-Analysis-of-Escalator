function [ time, amplitude ] = ImportData( DataLength, FileName )
%   ImportData: import signal data to dataspace
%   
%   Input:
%       DataLength: length of sample data
%       FileName: name of data file in data directory
%   Output:
%       time: time series of signal
%       amplitude: amplitude series of signal

len = DataLength;

path = '.\data\';
filepath = strcat(path, FileName);

load(filepath);

time = t(1:len);
amplitude = data(1:len);
end

