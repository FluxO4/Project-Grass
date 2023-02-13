#
#   Hello World server in Python
#   Binds REP socket to tcp://*:5555
#   Expects b"Hello" from client, replies with b"World"
#

from pickle import TRUE
import time
from turtle import width
import zmq
from PIL import Image

context = zmq.Context()
socket = context.socket(zmq.REP)
socket.bind("tcp://*:5555")

def get_image():
    socket.send(b"GIB")

    #  Wait for next request from client
    #width = socket.recv()
    #print(width)
    #height = socket.recv()
    #print(height)
    message = socket.recv()
    #print("Received request: %s" % message)
    
    #img = Image.frombytes("RGB", (200, 200), message)
    print(message)
    #img.show()

    #  Do some 'work'.
    #  Try reducing sleep time to 0.01 to see how blazingly fast it communicates
    #  In the real world usage, you just need to replace time.sleep() with
    #  whatever work you want python to do, maybe a machine learning task?
    time.sleep(1)

    #  Send reply back to client
    #  In the real world usage, after you finish your work, send your output here
    socket.send(b"Received")

while(True):
    a=str(input("give key"))
    if(a=="q"):
        break;
    if(a=="p"):
        get_image()