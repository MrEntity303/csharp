pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        echo 'Jenkins esegue automaticamente il pull del codice dal repository'
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
      }
    }

    stage('Send Test Errors to ClickUp') {
      steps {
        bat 'powershell -File parse_tests_clickup.ps1'
        bat '$CLICKUP_API_TOKEN = "pk_48924049_FWRYWTQ5HNT43DJHY6L7KTFCS4M0921L" $CLICKUP_LIST_ID = "901507049402" $testResults = [xml](Get-Content "test_results.trx")  foreach ($unitTest in $testResults.TestRun.Results.UnitTestResult) {     if ($unitTest.outcome -eq "Failed") {         $testName = $unitTest.testName         $errorMessage = $unitTest.output.errorInfo.message          $jsonBody = @{             name        = "Test Fallito: $testName"             description = "Errore: $errorMessage"             status      = "to do"         } | ConvertTo-Json -Depth 10          Invoke-RestMethod -Uri "https://api.clickup.com/api/v2/list/$CLICKUP_LIST_ID/task" `                           -Method Post `                           -Headers @{ "Authorization" = $CLICKUP_API_TOKEN; "Content-Type" = "application/json" } `                           -Body $jsonBody     } }'
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