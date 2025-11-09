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

                bat 'dotnet publish API/API.csproj -c Release -o "C:\\JenkinsPublish\\WebAPI"'
            }
        }

        stage('Deploy to IIS') {
            steps {
                // Stop IIS website
                bat 'powershell -Command "Import-Module WebAdministration; Stop-Website -Name \'MyWebAPI\'"'

                // Copy published files to IIS directory
                bat 'xcopy "C:\\JenkinsPublish\\WebAPI\\*" "C:\\inetpub\\wwwroot\\MyWebAPI\\" /E /Y /I'

                // Start IIS website
                bat 'powershell -Command "Import-Module WebAdministration; Start-Website -Name \'MyWebAPI\'"'
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
