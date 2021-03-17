# Example of container usage for dev

## Applications in containers
This example is comprised of minimal application in two directories (python and php). Each one of them have a Dockerfile in the directory, describing how to build the image to run the application. You can look into python/Dockerfile to see how to install the dependencies on the base image. The ENTRYPOINT describes the command the container will run once up.
Here what is important to note is that you have 2 Dockerfile, so docker will build 2 separate images only.

## Orchestration
In our example here, we'll use docker-compose to "orchestrate" the containers. In this example, we'll use the docker-compose abstraction to to also build the images but you could build those images directly with docker (docker build ./php/).
The orchestration layer should be considered as a way to describe which services should run and how they can interact with eachother. You have to think of the containers as running in a completely isolated environement and the orchestration layer gives you tools to create "doors" to this isolated environemnet through port mapping.

### Docker-compose

You can look at the orchestration in the docker compose file. In our example we have 5 services which will run; note here: it's important to distinguish an image (static) and a container which is an instance of the image running.

In the docker-compose file we are going to use 3 different images: one is generic and will downloaded from docker.io (mysql) and the 2 other images are coming from the locally built Dockerfile.

### MYSQL
The first service, mysql, will represent the database that all of our services are going to use. We are mapping the mysql port of the container to the localhost:3308 port and we also map the /var/lib/mysql path to ./data/. That means that on first run mysql will create all of the files in that directory and you can maintain persistence of the data between reload of the container. If you wanted to clean the db for example, you could just rm data and restart the container.
You can also see that we create the root password and root database through the env variables of the container.

### Application services: python

The 2 services, python and python2 are using the same base image but have slightly different parameters; first they are redirected to different port on our localhost -- altough you can note that the source port from the container is 8000 for both services. The second part is that in the first service we are using the files of the application that were copied during the build and in the python2 services we are explicitely mapping our local directory inside the container. This will unable us to edit directly the files on our local machine and have those changes beeing represented inside the container (for python2). It's not the case for the service name "python" where the application will not reflect any changes made on your local machine.
So if you point your local browser to http://localhost:8000 you have the static version of the app and http://localhost:8001 you'll have the "dynamic" version, that will take into account the changes you're making in your python/ directory. Note here that we are using the same image for both, we control all of this through the orchestration layer.


### Application services: php
Same as with the python version, we use the php image to run 2 services: one mapped to the local directory (http://localhost:8003) and the other one serving the app directly from the image (http://localhost:8002)

## Building and Launching everything

### Building
First you'll have to ask docker to build the images necessary for your orchestration:

	$ docker-compose build

that will pull all of the base images from the internet (the first line of both php|python/Dockerfile shows the base image to use) and run the list of commands to get to the state of the image necessary to run our apps.

### Running
Now, we need to run mysql first and give it some time to bootstrap itself and be ready to receive connection:
	
	$ docker-compose up -d mysql

So here it will run only the part of the docker-compose.yml file where the service is called "mysql". That will download the image: part from internet, map the mysql file to ./data/, launch mysqld and map the container port 3306 to localhost:3308. 
Once it's ready you can connect to the db 2 ways. The first one is from your local machine. Assuming you already have the mysql client installed on your local machine, you can run:

	$ mysql -P3308 -uroot -proot -h 127.0.0.1

The second way to do it, and it's useful if you don't have mysql client installed on your local machine, is to connect to the mysql container and use the mysql client from inside.

	$ docker-compose exec mysql /bin/sh

That will execute /bin/sh in the container with the service named "mysql" in our docker-compose.yaml file. Once you're in the container, with a shell open you can connect to the database like this:

	$ mysql -P3306 -uroot -proot





