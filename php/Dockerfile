FROM php:7.1-apache

RUN echo 'SetEnv MYSQL_USER ${MYSQL_DATABASE}' >> /etc/apache2/conf-enabled/environment.conf
RUN echo 'SetEnv MYSQL_USER ${MYSQL_USER}' >> /etc/apache2/conf-enabled/environment.conf
RUN echo 'SetEnv MYSQL_USER ${MYSQL_PASSWORD}' >> /etc/apache2/conf-enabled/environment.conf
RUN docker-php-ext-install mysqli pdo pdo_mysql && docker-php-ext-enable pdo_mysql mysqli
COPY . /var/www/html
