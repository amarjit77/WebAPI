pipeline {
    agent any

    environment {
        PATH = "C:\\Program Files\\dotnet;${env.PATH}"
    }

    stages {
        stage('Checkout') {
            steps {
                PUBLISH_DIR = "C:\\JenkinsPublish\\WebAPI"
                IIS_PATH = "C:\\inetpub\\wwwroot\\MyWebAPI"
                SITE_NAME = "MyWebAPI"
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
                script {
                    // Stop IIS website if exists
                    bat """
                    powershell -Command "Import-Module WebAdministration;
                    if (Test-Path IIS:\\Sites\\%SITE_NAME%) {
                        Stop-Website -Name '%SITE_NAME%'
                    } else {
                        Write-Host 'Site not found: %SITE_NAME%'
                    }"
                    """

                    // Copy files to IIS directory
                    bat 'xcopy "%PUBLISH_DIR%\\*" "%IIS_PATH%\\" /E /Y /I'

                    // Start IIS website (if it exists)
                    bat """
                    powershell -Command "Import-Module WebAdministration;
                    if (Test-Path IIS:\\Sites\\%SITE_NAME%) {
                        Start-Website -Name '%SITE_NAME%'
                    } else {
                        Write-Host 'Skipping start — site not found: %SITE_NAME%'
                    }"
                    """
                }
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
