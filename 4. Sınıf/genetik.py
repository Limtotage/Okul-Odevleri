import random
def minimizasyon(birey):
    elemanlar =[]
    elemanlar[:]=[]
    elemanlar.append(birey[0:15])
    elemanlar.append(birey[15:30])
    elemanlar.append(birey[30:45])
    elemanlar.append(birey[45:60])
    elemanlar.append(birey[60:75])
    toplam=0
    for x in elemanlar:
        negatif=x[0]
        esas=x[1:]
        k=int(esas,2)
        if(negatif=="1"):
            k=-1*k   
        toplam+=int(k)**2
    if toplam==0:
        toplam=1/1000
    return toplam
def karardeg():
    sayi=""
    for x in range(15):
        d = random.random()
        if d>=0.5:
            x=1
            sayi+=str(x)
        else:
            x=0
            sayi+=str(x)  
    return sayi   
def bireyol():
    sayi=""
    karar=0
    while karar<5:
        t=karardeg()
        negatif=t[0]
        esas=t[1:]
        k=int(esas,2)
        if(k <=100):
            karar+=1
            sayi+=t
    return sayi   
def capraz(ebeveyn1,ebeveyn2):
    k=random.random()
    yenibirey1=ebeveyn1
    yenibirey2=ebeveyn2
    if(k<=0.7):
        kopuk1=random.randrange(0,75)
        kopuk2=random.randrange(0,75)
        while kopuk1==kopuk2:
            kopuk2=random.randrange(0,75)
        if(kopuk1>kopuk2):
            yenibirey1=ebeveyn1[0:kopuk2]+ebeveyn2[kopuk2:kopuk1]+ebeveyn1[kopuk1:]
            yenibirey2=ebeveyn2[0:kopuk2]+ebeveyn1[kopuk2:kopuk1]+ebeveyn2[kopuk1:]
        else:
            yenibirey1=ebeveyn1[0:kopuk1]+ebeveyn2[kopuk1:kopuk2]+ebeveyn1[kopuk2:]
            yenibirey2=ebeveyn2[0:kopuk1]+ebeveyn1[kopuk1:kopuk2]+ebeveyn2[kopuk2:]
    ebeveyn1=yenibirey1
    ebeveyn2=yenibirey2
    return ebeveyn1,ebeveyn2
def mutasyon(populasyon):
    Mutrate= 0.001
    br=0
    for Birey in populasyon:
        by = 0
        Genler=list(Birey)
        for Gen in Birey:
            mutation=random.random()
            if(mutation<=Mutrate):
                if(Gen=="0"):
                    Genler[by]="1"
                else:
                    Genler[by]="0"
            by+=1
        mutasyonlubirey="".join(Genler)
        populasyon[br]=mutasyonlubirey
        br+=1
def populasyon():
    Bireyler=[]
    for x in range(20):
        Bireyler.append(bireyol())
    return Bireyler
def evli(olasilik):
    secims=[]
    secims[:]=[]
    oltab=[]
    oltab[:]=[]
    kumuol=0
    i=0
    for x in olasilik:
        kumuol+=(x*100)
        oltab.append(kumuol)
    ekleme=0
    y=0
    while y < 20:
        p=random.random()*100
        i=0
        while i < 19:
            if 0<=p and p<=oltab[0]:
                secims.append(0)
                ekleme+=1
                y+=1
                break
            elif oltab[19]<=p:
                secims.append(19)
                ekleme+=1
                y+=1
                break
            elif oltab[i] < p and p<=oltab[i+1]:
                secims.append(i+1)
                ekleme+=1
                y+=1
                break
            i+=1
    return secims
def eniyi(ol,populasyon):#sıkıntı burada
    eniyi=ol[0]
    i=0
    for y in ol:
        if(y>=eniyi):
            eniyi=y
            indis=i
            i+=1
            continue
        i+=1
    birey=populasyon[indis]
    karardegis=[]
    negatif=[birey[0]]
    esas=birey[1:15]
    k=int(esas,2)
    if negatif=="1":
        k=-1*k
    karardegis.append(k)
    negatif=[birey[15]]
    esas=birey[16:30]
    k=int(esas,2)
    if negatif=="1":
        k=-1*k
    karardegis.append(k) 
    negatif=[birey[30]]
    esas=birey[31:45]
    k=int(esas,2)
    if negatif=="1":
        k=-1*k
    karardegis.append(k)
    negatif=[birey[45]]
    esas=birey[46:60]
    k=int(esas,2)
    if negatif=="1":
        k=-1*k
    karardegis.append(k)
    negatif=[birey[60]]
    esas=birey[61:75]
    k=int(esas,2)
    if negatif=="1":
        k=-1*k
    karardegis.append(k)     
    essonuc=minimizasyon(birey)
    return karardegis,essonuc,indis
Serap = []
sonuc=[]
sonuc2=[]
ol=[]
top =0
top2 =0
deneme=0
i=0
Serap = populasyon()
for canli in Serap:
    x=minimizasyon(canli)
    sonuc.append(x)
    top+=x
for z in sonuc:
    top2+=top/z
    sonuc2.append(top/z)
for y in sonuc:
    z=sonuc2[i]
    ol.append(z/top2)
    i+=1
Karardegiskenleri,enso,insid=eniyi(ol,Serap)
print("ilk Populasyonda En iyi Birey="+str(insid+1)+". Bireydir =>"+Serap[insid])
print("ilk Populasyonda En iyi bireyin karar degiskenleri => X0="+str(Karardegiskenleri[0])+" X1="+str(Karardegiskenleri[1])+" X2="+str(Karardegiskenleri[2])+" X3="+str(Karardegiskenleri[3])+" X4="+str(Karardegiskenleri[4]))
enso=int(enso)
print("ilk Populasyonda En iyi Bireyin Fonk Sonucu ="+str(enso))

den=evli(ol)
ol.clear()
yenipop=[]
for x in range(20):
    if x%2==1:
        a,b=capraz(Serap[den[x]],Serap[den[x-1]])
        yenipop.append(a)
        yenipop.append(b)
Serap=yenipop.copy()
den[:]=[]

for j in range(9999):
    sonuc.clear()
    sonuc2.clear()
    for canli in Serap:
        x=minimizasyon(canli)
        sonuc.append(x)
        top+=x
    top2=0
    for z in sonuc:#top/0 ?
        top2+=top/z
        sonuc2.append(top/z)
    i=0
    for y in sonuc:
        z=sonuc2[i]
        ol.append(z/top2)
        i+=1
    Karardegiskenleri,enso,insid=eniyi(ol,Serap)
    print(str(j+2)+". Populasyonda En iyi Birey="+str(insid+1)+". Bireydir =>"+Serap[insid])
    print(str(j+2)+". Populasyonda En iyi bireyin karar degiskenleri => X0="+str(Karardegiskenleri[0])+" X1="+str(Karardegiskenleri[1])+" X2="+str(Karardegiskenleri[2])+" X3="+str(Karardegiskenleri[3])+" X4="+str(Karardegiskenleri[4]))
    enso=int(enso)
    print(str(j+2)+". Populasyonda En iyi Bireyin Fonk Sonucu ="+str(enso))
    den=evli(ol)#eşleştirildi caprazlama yapılıp yeni nesil üretilecek ve mutasyon döngü

    ol.clear()
    yenipop=[]
    for x in range(20):
        if x%2==1:
            a,b=capraz(Serap[den[x]],Serap[den[x-1]])
            yenipop.append(a)
            yenipop.append(b)
    den[:]=[]
    Serap=yenipop.copy() 
    
    mutasyon(Serap)
    input("selam")
    """print("6.birey")
print(Serap[5])
print(ol[5])
print("14.birey")
print(Serap[13])
print(ol[13])
print("19.birey")
print(Serap[18])
print(ol[18])
"""
