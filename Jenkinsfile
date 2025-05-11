pipeline {
  environment {
    DOCKER_ACCOUNT_NAME = "w3sherpa"
    DOCKER_ACCOUNT_EMAIL = "lonheeti@gmail.com"
    JENKINS_CREDENTIAL_ID = "dockerhub"
    JENKINS_CREDENTIAL_VARIABLE = "dockerhubpwd"
    JENKINS_CREDENTIAL_DEPLOYMENT_SERVER_USER = 'websherpa'
    SERVICE_NAME = "scheapp-app"
    SERVICE_NAME_DOCKER_RUN = "scheapp_app"
    SEVICE_PORT = 8002
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

        withCredentials([string(credentialsId: 'AuthenticationScheme__Google__ClientId', variable: 'GoogleClientId')
                        , string(credentialsId: 'AuthenticationScheme__Google__ClientSecret', variable: 'GoogleClientSecret')]) {
             script {
                  sh "docker build -t ${SERVICE_NAME}:latest --build-arg GoogleClientId=${GoogleClientId} --build-arg GoogleClientSecret=${GoogleClientSecret} ."          
                }
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
              def containerId = sh(script: 'ssh -o StrictHostKeyChecking=no -l ${JENKINS_CREDENTIAL_DEPLOYMENT_SERVER_USER} ${HOST_IP} sudo docker ps -q --filter ancestor=${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest', returnStdout: true).trim()
              echo 'container id is :' + containerId
              if(containerId != ""){
                  def command = 'ssh -o StrictHostKeyChecking=no -l ${JENKINS_CREDENTIAL_DEPLOYMENT_SERVER_USER} ${HOST_IP} sudo docker stop '+ containerId
                  sh(script: command, returnStdout: true)
              }
              withCredentials([string(credentialsId: 'dockerhub', variable: 'dockerhubpwd')]) {
                sh 'docker login -u ${DOCKER_ACCOUNT_EMAIL} -p ${dockerhubpwd}'
                sh 'ssh -o StrictHostKeyChecking=no -l ${JENKINS_CREDENTIAL_DEPLOYMENT_SERVER_USER} ${HOST_IP} sudo docker image pull ${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest'
                sh 'ssh -o StrictHostKeyChecking=no -l ${JENKINS_CREDENTIAL_DEPLOYMENT_SERVER_USER} ${HOST_IP} sudo docker run --rm -d --name=${SERVICE_NAME_DOCKER_RUN} -p ${SEVICE_PORT}:8080 ${DOCKER_ACCOUNT_NAME}/${SERVICE_NAME}:latest'
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