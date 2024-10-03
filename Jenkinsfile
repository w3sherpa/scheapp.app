pipeline {
  environment {
    DOCKER_ACCOUNT_NAME = "w3sherpa"
    DOCKER_ACCOUNT_EMAIL = "lonheeti@gmail.com"
    DOCKER_NETWORK_NAME = "scheapp"
    JENKINS_CREDENTIAL_ID = "dockerhub"
    JENKINS_CREDENTIAL_VARIABLE = "dockerhubpwd"
    SERVICE_NAME = "scheapp"
    SERVICE_NAME_DOCKER_RUN = "scheapp_app"
    SEVICE_PORT = 8090
    HOST_IP = "192.168.1.19"
    registryCredential = 'dockerhub'
    dockerImage = ''
  }
  agent any
  stages {
    stage('Checkout code') {
        steps {
            checkout scm //make sure to set up scm in jenkins
        }
    }
    stage('Build Image') {
      steps{
        script {
          sh "docker build -t ${SERVICE_NAME}:latest ."
        }
      }
    }
    stage('Deploy our image') {
      steps{
        script {
          withCredentials([string(credentialsId: 'dockerhub', variable: 'dockerhubpwd')]) {
              sh 'docker login -u ${DOCKER_ACCOUNT_EMAIL} -p ${dockerhubpwd}'
              sh "docker tag ${SERVICE_NAME} ${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest"
              sh "docker push ${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest"
              sh 'docker logout'
          }
        }
      }
    }
    stage('Deploy App to Webserver') {
      steps {
       script{
          sshagent(['sshwebserver']) {
              def containerId = sh(script: 'ssh -o StrictHostKeyChecking=no -l websherpa ${HOST_IP} sudo docker ps -q --filter ancestor=${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest', returnStdout: true).trim()
              if(containerId != ""){
                  def command = 'ssh -o StrictHostKeyChecking=no -l websherpa ${HOST_IP} sudo docker stop '+ containerId
                  sh(script: command, returnStdout: true)
              }
              withCredentials([string(credentialsId: 'dockerhub', variable: 'dockerhubpwd')]) {
                sh 'docker login -u ${DOCKER_ACCOUNT_EMAIL} -p ${dockerhubpwd}'
                sh 'ssh -o StrictHostKeyChecking=no -l websherpa ${HOST_IP} sudo docker image pull ${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest'
                sh 'ssh -o StrictHostKeyChecking=no -l websherpa ${HOST_IP} sudo docker run --rm -d --name=${SERVICE_NAME_DOCKER_RUN} --net=${DOCKER_NETWORK_NAME} -p ${SEVICE_PORT}:8080 ${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest'
              }
              
          }
        }
      }
    }
    stage('Cleaning up') {
      steps{
        script {
          sh "echo end"
        }
      }
    }
  }
}