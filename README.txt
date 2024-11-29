docker build -t webshop .

docker run -d -p 8080:80 --name webshopcontainer webshop


docker-compose up --build
