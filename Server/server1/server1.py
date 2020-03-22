from lxml import etree
import requests
from bs4 import BeautifulSoup
import re
import random
import mymail
import myserver
import json
#词云图包
import jieba
import jieba.analyse
from PIL import Image,ImageSequence
import numpy as np
import codecs
import matplotlib.pyplot as plt
from wordcloud import WordCloud,ImageColorGenerator
#情感倾向分析
import emo
import more
import LSTM

headers = {
    'user-agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 YaBrowser/19.7.0.1635 Yowser/2.5 Safari/537.36'
}

all_string=""

def get_163(od,td):
    global all_string
    url="https://news.163.com"
    req=requests.get(url)
    #req.encoding='utf-8'
    soup = BeautifulSoup(req.text, 'lxml')
    r=str(soup)
    req=etree.HTML(r)

    point_str=req.xpath('//*[@id="js_top_news"]/h2[1]/a/text()')
    arr1=str(point_str[0])
    point_str=req.xpath('//*[@id="js_top_news"]/h2[1]/a/@href')
    arr1_url=str(point_str[0])
    arr1_num=random.randint(90,100)
    #这里有问题，索引越界
    try:
        point_str=req.xpath('//*[@id="js_top_news"]/h2[2]/a/text()')
    except:
        point_str=req.xpath('//*[@id="js_top_news"]/h2[2]/a//text()')
    arr2=str(point_str)

    point_str=req.xpath('//*[@id="js_top_news"]/h2[2]/a/@href')
    arr2_url=str(point_str[0])
    arr2_num=random.randint(80,100)
    point_str=req.xpath('//*[@id="js_top_news"]/ul[1]/li[1]/a//text()')
    arr3=str(point_str[0])
    point_str=req.xpath('//*[@id="js_top_news"]/ul[1]/li[1]/a/@href')
    arr3_url=str(point_str[0])
    arr3_num=random.randint(60,80)
    point_str=req.xpath('//*[@id="js_top_news"]/ul[1]/li[2]//text()')
    arr4=str(point_str[0])
    point_str=req.xpath('//*[@id="js_top_news"]/ul[1]/li[2]/a[1]/@href')
    arr4_url=str(point_str[0])
    arr4_num=random.randint(60,80)
    point_str=req.xpath('//*[@id="js_top_news"]/ul[2]/li[1]/a[1]/text()')
    arr5=str(point_str[0])
    point_str=req.xpath('//*[@id="js_top_news"]/ul[2]/li[1]/a[1]/@href')
    arr5_url=str(point_str[0])
    arr5_num=random.randint(50,70)
    all_string+=arr1+arr2+arr3+arr4+arr5
    create_bar("wangyi_news",create_arr(arr1),arr1_num,create_arr(arr2),arr2_num,create_arr(arr3),arr3_num,create_arr(arr4),arr4_num,create_arr(arr5),arr5_num)
    if(od=='1'):
        if(td=='1'):return arr1;
        elif(td=='2'):return arr2;
        elif(td=='3'):return arr3;
        elif(td=='4'):return arr4;
        elif(td=='5'):return arr5;
    elif(od=='2'):
        if(td=='1'):return arr1_num;
        elif(td=='2'):return arr2_num;
        elif(td=='3'):return arr3_num;
        elif(td=='4'):return arr4_num;
        elif(td=='5'):return arr5_num;
    elif(od=='3'):
        if(td=='1'):return arr1_url;
        elif(td=='2'):return arr2_url;
        elif(td=='3'):return arr3_url;
        elif(td=='4'):return arr4_url;
        elif(td=='5'):return arr5_url;
    elif(od=='4'):
        return "网易"
    elif(od=='5'):
        if(td=='1'):return emo.sentiment_score(emo.sentiment_score_list(arr1));
        elif(td=='2'):return emo.sentiment_score(emo.sentiment_score_list(arr2));
        elif(td=='3'):return emo.sentiment_score(emo.sentiment_score_list(arr3));
        elif(td=='4'):return emo.sentiment_score(emo.sentiment_score_list(arr4));
        elif(td=='5'):return emo.sentiment_score(emo.sentiment_score_list(arr5));

        
def get_douban(od,td):
    global all_string
    url="https://www.douban.com/"
    req=requests.get(url,headers=headers)
    #req.encoding='utf-8'
    soup = BeautifulSoup(req.text, 'lxml')
    r=str(soup)
    req=etree.HTML(r)

    point_str=req.xpath('//li[@class="rec_topics"][1]/a/text()')
    arr1=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][1]/span/text()')
    arr1_num=get_num(str(point_str))
    point_str=req.xpath('//li[@class="rec_topics"][1]/a/@href')
    arr1_url=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][2]/a/text()')
    arr2=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][2]/span/text()')
    arr2_num=get_num(str(point_str))
    point_str=req.xpath('//li[@class="rec_topics"][2]/a/@href')
    arr2_url=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][3]/a/text()')
    arr3=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][3]/span/text()')
    arr3_num=get_num(str(point_str))
    point_str=req.xpath('//li[@class="rec_topics"][3]/a/@href')
    arr3_url=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][4]/a/text()')
    arr4=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][4]/span/text()')
    arr4_num=get_num(str(point_str))
    point_str=req.xpath('//li[@class="rec_topics"][4]/a/@href')
    arr4_url=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][5]/a/text()')
    arr5=str(point_str[0])
    point_str=req.xpath('//li[@class="rec_topics"][5]/span/text()')
    arr5_num=get_num(str(point_str))
    point_str=req.xpath('//li[@class="rec_topics"][5]/a/@href')
    arr5_url=str(point_str[0])
    max_num=max(arr1_num,arr2_num,arr3_num,arr4_num,arr5_num)
    arr1_num=int(100*arr1_num/max_num)
    arr2_num=int(100*arr2_num/max_num)
    arr3_num=int(100*arr3_num/max_num)
    arr4_num=int(100*arr4_num/max_num)
    arr5_num=int(100*arr5_num/max_num)
    create_bar("douban_news",create_arr(arr1),arr1_num,create_arr(arr2),arr2_num,create_arr(arr3),arr3_num,create_arr(arr4),arr4_num,create_arr(arr5),arr5_num)
    all_string+=arr1+arr2+arr3+arr4+arr5
    if(od=='1'):
        if(td=='1'):return arr1;
        elif(td=='2'):return arr2;
        elif(td=='3'):return arr3;
        elif(td=='4'):return arr4;
        elif(td=='5'):return arr5;
    elif(od=='2'):
        if(td=='1'):return arr1_num;
        elif(td=='2'):return arr2_num;
        elif(td=='3'):return arr3_num;
        elif(td=='4'):return arr4_num;
        elif(td=='5'):return arr5_num;
    elif(od=='3'):
        if(td=='1'):return arr1_url;
        elif(td=='2'):return arr2_url;
        elif(td=='3'):return arr3_url;
        elif(td=='4'):return arr4_url;
        elif(td=='5'):return arr5_url;
    elif(od=='4'):
        return "豆瓣"
    elif(od=='5'):
        if(td=='1'):return emo.sentiment_score(emo.sentiment_score_list(arr1));
        elif(td=='2'):return emo.sentiment_score(emo.sentiment_score_list(arr2));
        elif(td=='3'):return emo.sentiment_score(emo.sentiment_score_list(arr3));
        elif(td=='4'):return emo.sentiment_score(emo.sentiment_score_list(arr4));
        elif(td=='5'):return emo.sentiment_score(emo.sentiment_score_list(arr5));

def get_sougou(od,td):
    global all_string
    """
    url="https://news.sogou.com/"
    req=requests.get(url,headers=headers)
    #req.encoding='utf-8'
    soup = BeautifulSoup(req.text, 'lxml')
    r=str(soup)
    req=etree.HTML(r)
    """
    url = 'https://news.sogou.com/pc/json/index.json?t=1582611299971'
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.116 Safari/537.36'
        }
    req = requests.get(url, headers = headers)
    data=json.loads(req.text)
    arr1=data['top'][0][0]['title']
    arr1_url=data['top'][0][0]['url']
    arr1_num=random.randint(80,100)

    arr2=data['top'][1][0]['title']
    arr2_url=data['top'][1][0]['url']
    arr2_num=random.randint(80,100)

    arr3=data['top'][2][0]['title']
    arr3_url=data['top'][2][0]['url']
    arr3_num=random.randint(80,100)

    arr4=data['top'][3][1]['title']
    arr4_url=data['top'][3][1]['url']
    arr4_num=random.randint(80,100)

    arr5=data['top'][4][0]['title']
    arr5_url=data['top'][4][0]['url']
    arr5_num=random.randint(80,100)

    """
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="important first"]/a/@title')
    arr1=str(point_str[0])
    arr1_num=random.randint(80,100)
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="important first"]/a/@href')
    arr1_url=str(point_str[0])
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="important"]/a/@title')
    arr2=str(point_str[0])
    arr2_num=random.randint(60,80)
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="important"]/a/@href')
    arr2_url=str(point_str[0])
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="important title-padding"]/a/@title')
    arr3=str(point_str[0])
    arr3_num=random.randint(70,80)
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="important title-padding"]/a/@href')
    arr3_url=str(point_str[0])
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="icon-title"]/a/@title')
    arr4=str(point_str[0])
    arr4_num=random.randint(60,70)
    point_str=req.xpath('//ul[@class="hot-list"]/li[@class="icon-title"]/a/@href')
    arr4_url=str(point_str[0])
    point_str=req.xpath('//ul[@class="hot-list"]/li[7]/a/@title')
    arr5=str(point_str[0])
    arr5_num=random.randint(50,70)
    point_str=req.xpath('//ul[@class="hot-list"]/li[7]/a/@href')
    arr5_url=str(point_str[0])
    """
    all_string+=arr1+arr2+arr3+arr4+arr5
    create_bar("sougou_news",create_arr(arr1),arr1_num,create_arr(arr2),arr2_num,create_arr(arr3),arr3_num,create_arr(arr4),arr4_num,create_arr(arr5),arr5_num)
    if(od=='1'):
        if(td=='1'):return arr1;
        elif(td=='2'):return arr2;
        elif(td=='3'):return arr3;
        elif(td=='4'):return arr4;
        elif(td=='5'):return arr5;
    elif(od=='2'):
        if(td=='1'):return arr1_num;
        elif(td=='2'):return arr2_num;
        elif(td=='3'):return arr3_num;
        elif(td=='4'):return arr4_num;
        elif(td=='5'):return arr5_num;
    elif(od=='3'):
        if(td=='1'):return arr1_url;
        elif(td=='2'):return arr2_url;
        elif(td=='3'):return arr3_url;
        elif(td=='4'):return arr4_url;
        elif(td=='5'):return arr5_url;
    elif(od=='4'):
        return "搜狗"
    elif(od=='5'):
        if(td=='1'):return emo.sentiment_score(emo.sentiment_score_list(arr1));
        elif(td=='2'):return emo.sentiment_score(emo.sentiment_score_list(arr2));
        elif(td=='3'):return emo.sentiment_score(emo.sentiment_score_list(arr3));
        elif(td=='4'):return emo.sentiment_score(emo.sentiment_score_list(arr4));
        elif(td=='5'):return emo.sentiment_score(emo.sentiment_score_list(arr5));
    


def get_tieba(od,td):
    global all_string
    url="https://tieba.baidu.com/"
    req=requests.get(url,headers=headers)
    #req.encoding='utf-8'
    soup = BeautifulSoup(req.text, 'lxml')
    r=str(soup)
    req=etree.HTML(r)

    point_str=req.xpath('//*[@id="new_list"]/li[1]/div/div[1]/div[2]/a/text()')
    arr1=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[1]/div/div[1]/div[2]/a/@href')
    arr1_url="http://tieba.baidu.com"+str(point_str[0])
    #point_str=req.xpath('//*[@id="new_list"]/li[1]/div/div/span[@class="time"]/text()')
    #arr1_time=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[1]/div/div[1]/div[2]/span/em/text()')
    arr1_num=int(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[2]/div/div[1]/div[2]/a/text()')
    arr2=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[2]/div/div[1]/div[2]/a/@href')
    arr2_url="http://tieba.baidu.com"+str(point_str[0])
    #point_str=req.xpath('//*[@id="new_list"]/li[2]/div/div[3]/span/text()')
    #arr2_time=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[2]/div/div[1]/div[2]/span/em/text()')
    arr2_num=int(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[3]/div/div[1]/div[2]/a/text()')
    arr3=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[3]/div/div[1]/div[2]/a/@href')
    arr3_url="http://tieba.baidu.com"+str(point_str[0])
    #point_str=req.xpath('//*[@id="new_list"]/li[3]/div/div[3]/span/text()')
    #arr3_time=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[3]/div/div[1]/div[2]/span/em/text()')
    arr3_num=int(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[5]/div/div[1]/div[2]/a/text()')
    arr4=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[5]/div/div[1]/div[2]/a/@href')
    arr4_url="http://tieba.baidu.com"+str(point_str[0])
    #point_str=req.xpath('//*[@id="new_list"]/li[5]/div/div[3]/span/text()')
    #arr4_time=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[5]/div/div[1]/div[2]/span/em/text()')
    arr4_num=int(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[6]/div/div[1]/div[2]/a/text()')
    arr5=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[6]/div/div[1]/div[2]/a/@href')
    arr5_url="http://tieba.baidu.com"+str(point_str[0])
    #point_str=req.xpath('//*[@id="new_list"]/li[6]/div/div[3]/span/text()')
    #arr5_time=str(point_str[0])
    point_str=req.xpath('//*[@id="new_list"]/li[6]/div/div[1]/div[2]/span/em/text()')
    arr5_num=int(point_str[0])
    all_string+=arr1+arr2+arr3+arr4+arr5
    max_num=max(arr1_num,arr2_num,arr3_num,arr4_num,arr5_num)
    arr1_num=int(100*arr1_num/max_num)
    arr2_num=int(100*arr2_num/max_num)
    arr3_num=int(100*arr3_num/max_num)
    arr4_num=int(100*arr4_num/max_num)
    arr5_num=int(100*arr5_num/max_num)
    create_bar("tieba_news",create_arr(arr1),arr1_num,create_arr(arr2),arr2_num,create_arr(arr3),arr3_num,create_arr(arr4),arr4_num,create_arr(arr5),arr5_num)
    if(od=='1'):
        if(td=='1'):return arr1;
        elif(td=='2'):return arr2;
        elif(td=='3'):return arr3;
        elif(td=='4'):return arr4;
        elif(td=='5'):return arr5;
    elif(od=='2'):
        if(td=='1'):return arr1_num;
        elif(td=='2'):return arr2_num;
        elif(td=='3'):return arr3_num;
        elif(td=='4'):return arr4_num;
        elif(td=='5'):return arr5_num;
    elif(od=='3'):
        if(td=='1'):return arr1_url;
        elif(td=='2'):return arr2_url;
        elif(td=='3'):return arr3_url;
        elif(td=='4'):return arr4_url;
        elif(td=='5'):return arr5_url;
    elif(od=='4'):
        return "贴吧"
    elif(od=='5'):
        if(td=='1'):return emo.sentiment_score(emo.sentiment_score_list(arr1));
        elif(td=='2'):return emo.sentiment_score(emo.sentiment_score_list(arr2));
        elif(td=='3'):return emo.sentiment_score(emo.sentiment_score_list(arr3));
        elif(td=='4'):return emo.sentiment_score(emo.sentiment_score_list(arr4));
        elif(td=='5'):return emo.sentiment_score(emo.sentiment_score_list(arr5));

def creat_png(topic,od,td):
    #jieba.add_word("some_words",freq=None, tag=None)   #增加新词
    result=jieba.analyse.textrank(topic,topK=200,withWeight=True)
    keywords = dict()
    flag=1
    s1=s2=s3=s4=""
    for i in result:
        keywords[i[0]]=i[1]
        if(flag==1):
            s1=i[0]
            flag+=1
        elif(flag==2):
            s2=i[0]
            flag+=1;
        elif(flag==3):
            s3=i[0]
            flag+=1
        elif(flag==4):
            s4=i[0]
            flag+=1
    
    #
    if(td==2):
        image= Image.open('./pic2.jpg')
        try:
            save_str='d:/result2.png'
        except:
            save_str='d:/result2_2.png'
    elif(td==1):
        image= Image.open('./中国.jpg')#添加背景模板图片
        try:
            save_str='d:/result.png'
        except:
            save_str='d:/result_2.png'
    graph = np.array(image)
    wc = WordCloud(font_path='./fonts/simhei.ttf',background_color='White',max_words=100,mask=graph)
    wc.generate_from_frequencies(keywords)
    image_color = ImageColorGenerator(graph)
    plt.imshow(wc)
    plt.imshow(wc.recolor(color_func=image_color))
    plt.axis("off")
    #plt.show()
    if(od== '1'):
        wc.to_file(save_str)
        return s1
    elif(od=='2'):
        wc.to_file(save_str)
        return s2
    elif(od=='3'):
        wc.to_file(save_str)
        return s3
    elif(od=='4'):
        wc.to_file(save_str)
        return s4

def create_bar(name,s1,n1,s2,n2,s3,n3,s4,n4,s5,n5):
    plt.rcParams['font.sans-serif']=['SimHei']
    plt.rcParams['axes.unicode_minus'] = False
    num= (n1, n2, n3, n4, n5)
    ind = np.arange(len(num))  
    width = 0.3  
    fig, ax = plt.subplots()
    rects1 = ax.bar(ind - width/3, num, width, color='SkyBlue')
    fig.subplots_adjust(bottom = 0.23)
    ax.set_ylabel('Heat')
    ax.set_title('Hot Information Heat Histogram')
    plt.xticks(ind,(s1,s2,s3,s4,s5))
    try:
        plt.savefig('d:/'+name+'.png',dpi=300)
    except:
        plt.savefig('d:/'+name+'2.png',dpi=300)

    #plt.show()

def create_arr(arr):
    arr_f=""
    for i in range(0,int(len(arr)/5)+2):
        arr_f+=arr[i*5:(i+1)*5]+'\n'
    return arr_f

def get_num(string_n):
    arr=""
    for i in range(2,len(string_n)):
        if(string_n[i]=='.' or string_n[i]=='万'or string_n[i]=='次'):
            break;
        arr+=string_n[i]
    try:
        return int(arr)
    except:
        arr=""
        for i in range(7,len(string_n)):
            if(string_n[i]=='.' or string_n[i]=='万'or string_n[i]=='次'):
                break;
            arr+=string_n[i]
        try:
            return int(arr)
        except:
            arr=""
            for i in range(9,len(string_n)):
                if(string_n[i]=='.' or string_n[i]=='万'or string_n[i]=='次'):
                    break;
                arr+=string_n[i]
            try:
                return int(arr)
            except:
                 return 0
            #try:
            #    return int(arr)
            #except:
                #return -1

def png_post(od,td):
    get_163(1,1)
    get_sougou(1,1)
    get_tieba(1,1)
    get_douban(1,1)
    return creat_png(all_string,od,1)

if __name__=='__main__':
    #print(get_163('1','1'))
    png_post(1,1)
    myserver.make_myserver()
    
 
   