from flask import Flask
import pymysql
import os

app = Flask(__name__)

connection = pymysql.Connection(user=os.getenv('MYSQL_USER'), database=os.getenv('MYSQL_DATABASE'), password=os.getenv('MYSQL_PASSWORD'), host='mysql')

@app.route('/')
def hello_world():
    cursor = connection.cursor()
    return 'Hello, World! ' + str(cursor.execute('show tables;'))
