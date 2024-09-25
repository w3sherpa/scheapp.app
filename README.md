# sherpaticket
`docker build -t sherpaticket:latest .`
`docker tag sherpaticket w3sherpa/sherpaticket:latest`  
`docker push w3sherpa/sherpaticket:latest`  
`docker run --rm -d --name=sherpaticket_app -p 8090:8080 w3sherpa/sherpaticket:latest`