docker build -t microservice -f docker-microservice.Dockerfile .
docker build -t bff -f docker-bff.Dockerfile .
docker build -t rabbitmq -f docker-rabbitMQ.Dockerfile .
docker build -t front -f docker-front.Dockerfile .