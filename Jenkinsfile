pipeline {
    agent any

    environment {
        PUBLISH_DIR = "C:\\JenkinsPublish\\MyWebAPI"
        IIS_PATH = "C:\\inetpub\\wwwroot\\MyWebAPI"
        SITE_NAME = "MyWebAPI"
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
                bat 'dotnet restore API/API.csproj'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build API/API.csproj --configuration Release'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test API/API.csproj --no-build'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish API/API.csproj -c Release -o publish'
            }
        }

        stage('Archive Artifacts') {
            steps {
                archiveArtifacts artifacts: 'publish/**/*', fingerprint: true

                bat 'dotnet publish API/API.csproj -c Release -o "C:\\JenkinsPublish\\MyWebAPI"'
            }
        }

        stage('Deploy to IIS') {
            steps {
                bat '''
                powershell -NoProfile -ExecutionPolicy Bypass -Command "Import-Module WebAdministration; if (Test-Path 'IIS:\\Sites\\MyWebAPI') { Stop-Website -Name 'MyWebAPI'; }; Copy-Item 'C:\\JenkinsPublish\\WebAPI\\*' 'C:\\inetpub\\wwwroot\\MyWebAPI\\' -Recurse -Force; Start-Website -Name 'MyWebAPI'"
                '''
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
