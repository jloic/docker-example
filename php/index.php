<?php 

$link = new PDO('mysql:host=mysql;dbname=' . $_ENV['MYSQL_DATABASE'], $_ENV['MYSQL_USER'], $_ENV['MYSQL_PASSWORD']);
echo 'Connected successfully\n';

var_dump($link->query('select * from users;'))
 ?>

