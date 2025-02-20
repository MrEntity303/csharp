pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        echo 'Jenkins esegue automaticamente il pull del codice dal repository'
        git(credentialsId: 'github-token', url: 'https://github.com/MrEntity303/csharp', branch: 'my-new-brach-pipeline')
      }
    }

    stage('Restore NuGet Packages') {
      steps {
        bat 'dotnet restore'
      }
    }

    stage('Build') {
      steps {
        bat 'dotnet build --configuration Release'
      }
    }

    stage('Test') {
      steps {
        bat 'dotnet test --logger "trx;LogFileName=test_results.trx" || echo "TEST FAILED"'
        bat 'powershell -ExecutionPolicy Bypass -File "C:\\DevTools\\parse_tests_clickup.ps1"'
      }
    }

    stage('Send Test Errors to ClickUp') {
      steps {
        bat 'powershell -ExecutionPolicy Bypass -File "C:\\DevTools\\parse_tests_clickup.ps1"'
      }
    }

    stage('Complete') {
      steps {
        bat 'echo Pipeline completata!'
        bat 'del test_results.trx'
      }
    }

  }
}