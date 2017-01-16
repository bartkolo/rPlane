from suds.client import Client
import time


print("Connecting to Service...")
wsdl = "http://localhost:14736/rPlaneService.svc?WSDL"
client = Client(wsdl)

while 1:
    client.service.SendMessage('8D40621D58C382D690C8AC2863A7')
    client.service.SendMessage('8D40621D58C386435CC412692AD6')



