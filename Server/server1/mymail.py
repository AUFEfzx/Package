import smtplib 
from email.mime.text import MIMEText
from email.header import Header
import traceback

#三个变量分别为收件人邮箱，主题和正文
def send_mail(rec_mail,theme,text):
    mailhost='smtp.qq.com'
    qqmail = smtplib.SMTP()
    qqmail.connect(mailhost,25)

    account = '915373174@qq.com'
    password = 'gemntphnljrjbffj'
    qqmail.login(account,password)

    receiver=rec_mail

    content=text
    message = MIMEText(content, 'plain', 'utf-8')#实例化一个MIMEText邮件对象，该对象需要写进三个参数，分别是邮件正文，文本格式和编码.
    subject = theme
    message['Subject'] = Header(subject, 'utf-8')
    try:
        qqmail.sendmail(account, receiver, message.as_string())
        print ('邮件发送成功')
    except Exception as e:  
        print(traceback.format_exc())
        print ('邮件发送失败')
        
    qqmail.quit()