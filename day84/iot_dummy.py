# 현재일부터 10000일전을 시작으로 하루에 데이터 100개씩
# pip install mysql-connector-python

import mysql.connector
import random
from datetime import datetime, timedelta


#MySQL 연결 설정
conn = mysql.connector.connect(
    host = 'localhost',
    database = 'smarthome',
    user = 'root',
    password = '12345' 
)

cursor = conn.cursor()

# 입력값 파라미터
start_date = datetime(1998,1,20)   #10000일전 기준일
records_per_day = 100
total_days = 10000

# 입력쿼리
insert_query = '''
INSERT INTO IOT_DATA (sensing_dt ,loc_id,temp ,humid ) VALUES (%s, %s, %s, %s)
'''

# 한 번에 1000개씩 데이터를 묶어서 한꺼번에 DB에 삽입합니다.
# 배치 리스트 batch에 데이터를 모읍니다.
batch_size=1000  
batch = []





#배치리스트 생성
for day in range(total_days) :
    date = start_date + timedelta(days=day)
    for i in range(records_per_day) :
        timestamp = date + timedelta(minutes=15*i)
        temp = round(random.uniform(19.0,28.0),2)
        humid = round(random.uniform(30.0,60.0),2)
        batch.append((timestamp, 'DINNING', temp, humid))

        if len(batch) >= batch_size :
            cursor.executemany(insert_query, batch)
            conn.commit()
            batch.clear()
    print(f'100번 완료 : {day}')
#남은 배치리스트가 있으면
if batch:
    cursor.executemany(insert_query, batch)
    conn.commit()
    batch.clear()

cursor.close()
conn.close()
print('더미데이터 삽입 완료')