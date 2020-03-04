import requests
import urllib
import json
import re
from bs4 import BeautifulSoup
from lxml import etree


def get_douban_more(page_num):
    url = 'https://m.douban.com/rexxar/api/v2/gallery/topic/'+page_num+'/items?'
    headers = {
        'Referer':'https://www.douban.com/gallery/topic/'+page_num+'/',
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.116 Safari/537.36'
        }
    data = {'sort': 'hot','start': 20,'count': 40,'status_full_text': 1,'guest_only': 0,'ck': 0}
    url+=urllib.parse.urlencode(data)
    req = requests.get(url, headers = headers)
    data=json.loads(req.text)
    finally_str=""
    page=1
    for i in range(1,50):
        try:
            job_data=data['items'][i]
            finally_str+=str(job_data['abstract'])
            finally_str+="$"
            page+=1
            if(page==21):
                break
        except:
            continue
    return finally_str

def get_tieba_more(url_old):
    headers = {
        'user-agent': 'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/75.0.3770.100 YaBrowser/19.7.0.1635 Yowser/2.5 Safari/537.36'
        }
    comments=""
    for i_page in range(1,5):
        url=url_old+'?pn='+str(i_page)
        req=requests.get(url,headers=headers)
        #req.encoding='utf-8'
        soup = BeautifulSoup(req.text, 'lxml')
        r=str(soup)
        req=etree.HTML(r)
        for i in range(1,20):
            try:
                point_str=req.xpath('//*[@id="j_p_postlist"]/div['+str(i)+']/div[3]/div/cc/div[2]/text()')
                com_s=str(point_str[0])
            except:
                point_str=req.xpath('//*[@id="j_p_postlist"]/div['+str(i)+']/div[2]/div/cc/div[2]/text()')
                try:
                    com_s=str(point_str[0])
                except:
                    continue
            try:
                com=""
                for j in range(0,len(com_s)):
                    if(com_s[j]!=" " and com_s[j]!='\n'):
                        com+=com_s[j]
                comments+=com
                comments+='$'
            except:
                continue
    return comments

