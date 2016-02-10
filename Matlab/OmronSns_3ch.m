clear
clc

% Find a serial port object. If it exists - close it. 
obj1 = instrfind('Type', 'serial', 'Port', 'COM10', 'Tag', '');
if ~isempty(obj1)
    fclose(obj1);
end;
obj1 = serial('COM10', 'Baudrate', 115200);
% Connect to port
fopen(obj1);

% Time, temperature, Beam[8]
formatSpec = '%d %d %d %d %d %d %d %d %d %d';

time=200;
tic
tt=toc;

while tt < time
    data = fscanf(obj1, formatSpec);
    data(1) = []; % Remove first element: this is time
    data(1) = []; % Remove second element: this is self temperature
    MinValue = min(data);
    data = data - MinValue;
    % Draw input diagram    
    subplot(2, 1, 1);
    bar(data);
    axis([0 9 0 200]);
    % ==== Calculate PWM values for three channels ====
    k = 2.5;
    pwm = [k*mean(data(1:3)), k*mean(data(4:5)), k*mean(data(6:8))];
    %pwm = [k*std(data(1:3)), k*std(data(3:6)), k*std(data(6:8))];
    % Draw output diagram
    subplot(2, 1, 2);
    bar(pwm);
    axis([0 4 0 255]);
        
    pause(0.1);
end;
    