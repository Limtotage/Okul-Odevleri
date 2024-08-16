from PIL import Image as im
from PIL import ImageTk
import numpy as np
from numpy import asarray
import matplotlib.pyplot as plt
from tkinter import filedialog
from tkinter import *
from tkinter import ttk
def GTB(resim,ed):
    w,h=resim.size
    temp=resim.copy()
    for x in range(w):
        for y in range(h):
            renk=temp.getpixel((x,y))
            if renk <int(ed):
                temp.putpixel((x,y),0)
            else:
                temp.putpixel((x,y),255)
    return temp
def RTG(resim):
    w,h=resim.size
    temp=im.new(mode="L",size=(w,h),color="black")
    for x in range(w):
        for y in range(h):
            r,g,b=resim.getpixel((x,y))
            temp.putpixel((x,y),int(0.299*r + 0.587*g + 0.114*b ) )
    return temp
def ZOU(resim):
    w,h=resim.size
    temp=im.new(mode=resim.mode,size=(int(w/2),int(h/2)),color="black")
    for x in range(w):
        for y in range(h):
            if x%2==0 and y%2==0:
                if resim.mode=="RGB":
                    r,g,b=resim.getpixel((x,y))
                    temp.putpixel((int(x/2),int(y/2)),(r,g,b) )
                elif resim.mode=="L":
                    gr=resim.getpixel((x,y))
                    temp.putpixel((int(x/2),int(y/2)),gr )             
    return temp
def ZIN(resim):
    w,h=resim.size
    matris = np.array([[0,1,0],
                       [1,1,1],
                       [0,1,0]], dtype=int)
    temp=im.new(mode=resim.mode,size=(2*w,2*h),color="black")
    for x in range(w):
        for y in range(h):
            if resim.mode=="RGB":
                r,g,b=resim.getpixel((x,y))
                for i in range(2):
                    for j in range(2):
                        temp.putpixel((2*x+i,2*y+j),(r,g,b)  )
            elif resim.mode=="L":
                gr=resim.getpixel((x,y))
                for i in range(2):
                    for j in range(2):
                        temp.putpixel((2*x+i,2*y+j),gr  )
    return temp
def histogram(resim):
    gresim=RTG(resim)
    resimarray=asarray(gresim)
    plt.hist(resimarray) 
    plt.xlabel("Ton")
    plt.ylabel("Adet")
    plt.title("gri seviye")
    plt.show()
def Kenar(resim):
    gresim=RTG(resim)
    resimarray=asarray(gresim)
    yeni=np.empty_like(resimarray,dtype=np.int8)
    yeni=resimarray.copy()
    h,w=resimarray.shape
    matrisx=[-1,0,1,-2,0,2,-1,0,1]
    matrisy=[1,2,1,0,0,0,-1,-2,-1]
    for x in range(1,w-1):
        for y in range(1,h-1):
            topgrix=0
            topgriy=0
            k=0
            for i in range(-1,2):
                for j in range(-1,2):
                    renk=resimarray[y+j,x+i]
                    topgrix+=renk*matrisx[k]
                    topgriy+=renk*matrisy[k]
                    k+=1
            renkxy=abs(topgrix)+abs(topgriy)
            if renkxy>255:
                renkxy=255
            if renkxy<0:
                renkxy=0
            yeni[y,x]=renkxy
    son=im.fromarray(yeni)
    son.show()
            
def histogramequal(resim):
    gresim=RTG(resim)
    resimarray=asarray(gresim)
    y,x=resimarray.shape
    a = np.zeros((256,),dtype=np.float16)
    for i in range(x):
        for j in range(y):
            g=resimarray[j,i]
            a[g]+=1
    tmp = 1.0/(x*y)
    b = np.zeros((256,),dtype=np.float16)
    yeni=np.empty_like(resimarray,dtype=np.int8)
    yeni=resimarray.copy()
    for i in range(256):
        for j in range(i+1):
            b[i] += a[j] * tmp
        b[i] = round(b[i] * 255)
    for i in range(x):
        for j in range(y):
            g = resimarray[j,i]
            yeni[j,i]= b[g]
    plt.hist(yeni) 
    plt.xlabel("Ton")
    plt.ylabel("Adet")
    plt.title("denklestirme sonucu")
    plt.show()
    son=im.fromarray(yeni)
    son.show()
def Median(resim):
    if resim.mode=="L":
        resimarray=asarray(resim)
        yeni=np.empty_like(resimarray,dtype=np.int8)
        yeni=resimarray.copy()
        h,w=resimarray.shape
        dizi=[]
        for x in range(1,w-1):
            for y in range(1,h-1):
                for i in range(-1,2):
                    for j in range(-1,2):
                        renkGr=resimarray[y+j,x+i]
                        dizi.append(renkGr)
                for i in range(0,len(dizi)):
                    for j in range(0,len(dizi)-1):
                        if dizi[j]>dizi[j+1]:
                            dizi[j],dizi[j+1]=dizi[j+1],dizi[j]

                yeni[y,x]=dizi[4]
                dizi.clear()
        son=im.fromarray(yeni)
        son.show()
    else:    
        resimarray=asarray(resim)
        yeni=np.empty_like(resimarray,dtype=np.int8)
        yeni=resimarray.copy()
        h,w,z=resimarray.shape
        renkR=[]
        renkG=[]
        renkB=[]
        renkgri=[]
        for x in range(1,w-1):
            for y in range(1,h-1):
                k=0
                for i in range(-1,2):
                    for j in range(-1,2):
                        renkR.append(resimarray[y+j,x+i,0])
                        renkG.append(resimarray[y+j,x+i,1])
                        renkB.append(resimarray[y+j,x+i,2])
                        renkgri.append(int(renkR[k] * 0.299 + renkG[k] * 0.587 + renkB[k] * 0.114))
                        k+=1
                for i in range(0,len(renkgri)):
                    for j in range(0,len(renkgri)-1):
                        if renkgri[j]>renkgri[j+1]:
                            renkR[j],renkR[j+1]=renkR[j+1],renkR[j]
                            renkG[j],renkG[j+1]=renkG[j+1],renkG[j]
                            renkB[j],renkB[j+1]=renkB[j+1],renkB[j]
                yeni[y,x,0]=renkR[4]
                yeni[y,x,1]=renkG[4]
                yeni[y,x,2]=renkB[4]
                renkB.clear()
                renkR.clear()
                renkG.clear()
                renkgri.clear()
        son=im.fromarray(yeni)
        son.show()
def Mean(resim):
    if resim.mode=="L":
        resimarray=asarray(resim)
        yeni=np.empty_like(resimarray,dtype=np.int8)
        yeni=resimarray.copy()
        h,w=resimarray.shape
        for x in range(1,w-1):
            for y in range(1,h-1):
                topGr=0
                for i in range(-1,2):
                    for j in range(-1,2):
                        renkGr=resimarray[y+j,x+i]
                        topGr+=renkGr
                Gray=int(topGr/9)
                yeni[y,x]=Gray
        son=im.fromarray(yeni)
        son.show()
    else:    
        resimarray=asarray(resim)
        yeni=np.empty_like(resimarray,dtype=np.int8)
        yeni=resimarray.copy()
        h,w,z=resimarray.shape
        for x in range(1,w-1):
            for y in range(1,h-1):
                topR=0
                topG=0
                topB=0
                k=0
                for i in range(-1,2):
                    for j in range(-1,2):
                        renkR=resimarray[y+j,x+i,0]
                        renkG=resimarray[y+j,x+i,1]
                        renkB=resimarray[y+j,x+i,2]
                        topR+=renkR
                        topG+=renkG
                        topB+=renkB
                Red=int(topR/9)
                Green=int(topG/9)
                Blue=int(topB/9)
                yeni[y,x,0]=Red
                yeni[y,x,1]=Green
                yeni[y,x,2]=Blue
        son=im.fromarray(yeni)
        son.show()
def keskinlik(resim):
    gresim=RTG(resim)
    resimarray=asarray(gresim)
    yeni=np.empty_like(resimarray,dtype=np.int8)
    yeni=resimarray.copy()
    h,w=resimarray.shape
    matris=[1,1,1,1,-8,1,1,1,1]
    for x in range(1,w-1):
        for y in range(1,h-1):
            topgri=0
            k=0
            for i in range(-1,2):
                for j in range(-1,2):
                    renk=resimarray[y+j,x+i]
                    topgri+=renk*matris[k]
                    k+=1
            renkx=abs(topgri)
            if renkx>255:
                renkx=255
            if renkx<0:
                renkx=0
            yeni[y,x]=renkx
    son=im.fromarray(yeni)
    son.show()
while True:
    k=input("resmi secmek icin r tuşunana basınız\n"
            "diger sayfaya geçmek için n tuşuna basınız")
    if k =="r":
        filename = filedialog.askopenfilename()
        if filename:
            im1 = im.open(filename)
            im1.show()
        else:
            print("No file selected.")
    if k == "n":
        break
korunan=im1.copy()
while True:
    k=input("yapmak istediğiniz işlem \n"
            "renkliden griye çevirmek için r\n"
            "griden siyah-beyaz resme geçmek için g\n"
            "zoom in -zoom out için i ve o tuşlarına basınız")
    raise


"""
deneme=[]
def set():
    filename = filedialog.askopenfilename()
    if filename:
        im1 = im.open(filename)
        im1 = ImageTk.PhotoImage(im1)
        Lab = Label(root,image=im1)
        deneme.append(im1)
        Lab.place(x=240,y=50)
    else:
        print("No file selected.")



root = Tk()

root.title("Sayisal Vize Odev")
root.geometry("1080x720")
button = Button(root, text = "Resim seciniz", command=set,
                fg = "red", font = "Verdana 14 underline",
                bd = 2, bg = "light blue", relief = "groove")
button.place(x=50,y=50)
root.mainloop()
"""