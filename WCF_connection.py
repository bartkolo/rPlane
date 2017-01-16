from suds.client import Client
import time


print("Connecting to Service...")
wsdl = "http://localhost:14736/rPlaneService.svc?WSDL"
client = Client(wsdl)

while 1:
    result = client.service.SendMessage('8D4840D6202CC371C32CE0576098')
    print(result)

