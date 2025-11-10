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
                powershell '''
                    Import-Module WebAdministration

                    $siteName = "MyWebAPI"
                    $appPool = (Get-Website $siteName).ApplicationPool

                    Write-Host "Stopping IIS site and app pool..."
                    Stop-Website -Name $siteName -ErrorAction SilentlyContinue
                    Stop-WebAppPool -Name $appPool -ErrorAction SilentlyContinue

                    Write-Host "Copying new build files..."
                    Copy-Item "C:\\JenkinsPublish\\MyWebAPI\\*" "C:\\inetpub\\wwwroot\\MyWebAPI\\" -Recurse -Force

                    Write-Host "Starting app pool and website..."
                    Start-WebAppPool -Name $appPool
                    Start-Website -Name $siteName

                    Write-Host "Deployment completed successfully."
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
