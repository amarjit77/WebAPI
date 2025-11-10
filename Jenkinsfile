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

                bat 'dotnet publish API/API.csproj -c Release -o "C:\\JenkinsPublish\\MyWebAPI"'
            }
        }

        stage('Deploy to IIS') {
            steps {
            powershell '''
                Import-Module WebAdministration

                $siteName = "MyWebAPI"
                $appPool = (Get-Website $siteName).ApplicationPool
                $publishPath = "C:\\JenkinsPublish\\MyWebAPI\\"
                $deployPath = "C:\\inetpub\\wwwroot\\MyWebAPI\\"

                Write-Host "Stopping IIS site and app pool..."
                Stop-Website -Name $siteName -ErrorAction SilentlyContinue
                Stop-WebAppPool -Name $appPool -ErrorAction SilentlyContinue

                # Wait for IIS to fully release locks
                Write-Host "Waiting for IIS to release file locks..."
                Start-Sleep -Seconds 5

                # Kill any remaining w3wp processes
                Write-Host "Killing leftover IIS worker processes..."
                Get-Process w3wp -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue

                # Now copy the new build files
                Write-Host "Copying new build files..."
                Remove-Item "$deployPath*" -Recurse -Force -ErrorAction SilentlyContinue
                Copy-Item "$publishPath*" "$deployPath" -Recurse -Force

                # Restart the app pool and website
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
