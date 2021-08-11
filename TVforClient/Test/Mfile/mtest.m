function [ avg ] = mtest( t, amplitude )
%MTEST mtest:get avg of amplitude

L = length(t);
avg = sum(amplitude)/L;

end

