

import socket

TCP_IP = '127.0.0.1'
TCP_PORT = 30001
BUFFER_SIZE = 1024
MESSAGE = "Hello, World!"

TCP_IP_CENTRALE='10.102.26.11'
TCP_PORT_CENTRALE=5555
c = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
c.connect((TCP_IP_CENTRALE,TCP_PORT_CENTRALE))
#c.send('hello <EOF>'.encode())

while 1:
    import time
    c.send('8D75804B580FF2CF7E9BA6F701D0'.encode())
    time.sleep(2)
    c.send('8D75804B580FF6B283EB7A157117'.encode())
    time.sleep(2)

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.connect((TCP_IP, TCP_PORT))

while 1:
    import time
    data = s.recv(BUFFER_SIZE)
    print(data)
    c.send(data+'<EOF>')

s.close()

