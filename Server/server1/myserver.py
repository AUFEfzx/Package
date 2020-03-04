import server1
import mymail
import emo
import more
#api服务
import json
from urllib import parse
from wsgiref.simple_server import make_server


def application(environ, start_response):
    start_response('200 OK', [('Content-Type', 'text/html')])
    # environ是当前请求的所有数据，包括Header和URL，body，这里只涉及到get
    # 获取当前get请求的所有数据，返回是string类型
    params = parse.parse_qs(environ['QUERY_STRING'])
    # 获取get中key为name的值
    name = params.get('name', [''])[0]
    od = params.get('od', [''])[0]
    td = params.get('td', [''])[0]
    if(name=='get_163()'):
        return json.dumps(server1.get_163(od,td),ensure_ascii=False)
    elif(name=='get_douban()'):
        return json.dumps(server1.get_douban(od,td),ensure_ascii=False)
    elif(name=='get_sougou()'):
        return json.dumps(server1.get_sougou(od,td),ensure_ascii=False)
    elif(name=='get_tieba()'):
        return json.dumps(server1.get_tieba(od,td),ensure_ascii=False)
    elif(name=='png_post()'):
        return json.dumps(server1.png_post(od,td),ensure_ascii=False)
    elif(name=='send_email()'):
        mymail.send_mail(od,"舆论系统提醒！",td)
        return json.dumps("",ensure_ascii=False)
    elif(name=='get_more()'):
        if(od=='1'):
            return json.dumps(more.get_douban_more(td),ensure_ascii=False)
        elif(od=='2'):
            return json.dumps(more.get_tieba_more(td),ensure_ascii=False)
    elif(name=='get_side'):
            return json.dumps(emo.sentiment_score(emo.sentiment_score_list(str(od))),ensure_ascii=False)

def make_myserver():
    port = 5088
    httpd = make_server("0.0.0.0", port, application)
    print('port{0} is servering fo you..........'.format(str(port)))
    httpd.serve_forever()