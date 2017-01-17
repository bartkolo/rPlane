from suds.client import Client
import time


print("Connecting to Service...")
wsdl = "http://localhost:14736/rPlaneService.svc?WSDL"
client = Client(wsdl)


a=0
with open("testfile1.txt", "r") as ins:
    for line in ins:
        client.service.SendMessage(line)
        a += 1
        print(a)
#while 0:
    #client.service.SendMessage('8D40621D58C382D690C8AC2863A7')
    #client.service.SendMessage('8D40621D58C386435CC412692AD6')



