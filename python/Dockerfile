FROM python:3.9.1-alpine

ENV PYTHONUNBUFFERED 1

RUN mkdir /usr/src/app
WORKDIR /usr/src/app

RUN apk add --no-cache \
        mariadb-dev \
        build-base \
        linux-headers \
        libffi-dev

COPY requirements.txt .
RUN pip install -r requirements.txt
COPY . .

ENTRYPOINT ["flask", "run", "-h", "0.0.0.0", "-p", "8000", "--reload", "--debugger"]
