

import socket

TCP_IP = '127.0.0.1'
TCP_PORT = 30001
BUFFER_SIZE = 1024
MESSAGE = "Hello, World!"

TCP_IP_CENTRALE='10.102.26.35'
TCP_PORT_CENTRALE=30001
c = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
c.connect((TCP_IP_CENTRALE,TCP_PORT_CENTRALE))
#c.send('hello <EOF>'.encode())

while 1:
    import time
    c.send('hello'.encode())
    time.sleep(2)

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((TCP_IP, TCP_PORT))

while 1:
    import time
    data = s.recv(BUFFER_SIZE)
    print(data)
    c.send(data+'<EOF>')

s.close()

