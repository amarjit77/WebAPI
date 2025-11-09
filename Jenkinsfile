pipeline {
    agent any

    environment {
        PATH = "C:\\Program Files\\dotnet;${env.PATH}"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/amarjit77/WebAPI.git'
            }
        }     
        
        stage('Check Dotnet') {
            steps {
                bat 'dotnet --version'
            }
        }

        stage('Restore') {
            steps {
                bat 'dotnet restore WebAPI/WebAPI.csproj'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build WebAPI/WebAPI.csproj --configuration Release'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test WebAPI/WebAPI.csproj --no-build'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish WebAPI/WebAPI.csproj -c Release -o publish'
            }
        }

        stage('Archive Artifacts') {
            steps {
                archiveArtifacts artifacts: 'publish/**/*', fingerprint: true
            }
        }
    }

    post {
        success {
            echo 'Build and Test successful!'
        }
        failure {
            echo 'Build failed!'
        }
    }
}
