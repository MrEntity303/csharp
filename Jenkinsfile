pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        echo 'Jenkins esegue automaticamente il pull del codice dal repository'
        echo 'prova'
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
        bat 'dotnet test --logger "trx;LogFileName=test_results.trx" || echo "TEST FAILED" && exit 0'
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