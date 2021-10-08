import org.jenkinsci.plugins.workflow.cps.CpsThread
import org.jenkinsci.plugins.workflow.actions.LabelAction
import com.cwctravel.hudson.plugins.extended_choice_parameter.ExtendedChoiceParameterDefinition

def buildNumber = env.BUILD_NUMBER
def octopusVersion = "1.0.${buildNumber}"
def octopusServer = 'http://asi-deploy-02/'
def octopusProject = ''
def appName = 'CompanyProfile.Web.Api'
def basePath = 'src\\CompanyProfile.Web.Api\\bin\\Release\\netcoreapp3.1'
def artifactsPath = "${basePath}\\octo"
def publishPath = "${basePath}\\publish"

def nuget = "nuget.exe"
def nugetServer = "\\\\asinetwork.local\\servers\\nuget\\packages"

def cronSchedule = '0 16 * * 1-5'

def projectProperties = [
    [$class: 'BuildDiscarderProperty', strategy: [$class: 'LogRotator', numToKeepStr: '10']]
];
if (env.BRANCH_NAME == 'master'){ 
    projectProperties.push(
        pipelineTriggers(
            [
                parameterizedCron(cronSchedule)
            ]
        )
    )
}
properties(projectProperties)

echo "buildNumber: ${buildNumber}"
echo "octopusVersion: ${octopusVersion}"
echo "octopusServer: ${octopusServer}"
echo "appName: ${appName}"
echo "basePath: ${basePath}"
echo "artifactsPath: ${artifactsPath}"
echo "PATH is $env.PATH"

node ('next'){
    stage('checkout') {
        checkout scm
    }

    stage('build') {
        bat 'dotnet build CompanyProfile.sln -c Release'
    }

    stage('test') {
        bat 'dotnet test --filter IntegrationTest!=true'	
        //jenkins is not stopping the server
        //bat 'dotnet test --filter IntegrationTest=true&Database!=SQL'	
    }
    
    stage('package') {
        bat "dotnet publish src\\${appName} -c Release --output=${publishPath}"
        
        archiveArtifacts artifacts: '**/CompanyProfile.Contracts.*.nupkg', onlyIfSuccessful: true
        //archiveArtifacts artifacts: '**/ASI.Services.CompanyProfile.*.nupkg', onlyIfSuccessful: true
        
        bat("${nuget} push **/CompanyProfile.Contracts.*.nupkg -s ${nugetServer}")
       // bat("${nuget} push **/ASI.Services.CompanyProfile.*.nupkg -s ${nugetServer}")
    }

    stage('deploy') {
        if(octopusProject != ''){
            if (env.BRANCH_NAME == 'master')
            { 
                withCredentials([string(credentialsId: 'octopus-api-key', variable: 'octopusApiKey')]) {

                    //half the time this takes forever because artifacts sometimes are created 
                    echo "octo pack starting"
                    bat "dotnet octo pack --id=${appName} --version=${octopusVersion} --basePath=\"${publishPath}\" --outFolder=\"${artifactsPath}\""
            
                    echo "octo push starting"
                    bat "dotnet octo push --package=${artifactsPath}\\${appName}.${octopusVersion}.nupkg --server=${octopusServer} --apiKey ${octopusApiKey}"
            
                    bat "dotnet octo create-release --server ${octopusServer} --apiKey ${octopusApiKey} --project \"${octopusProject}\" --packageversion \"${octopusVersion}\" --version \"${octopusVersion}\""
                    bat "octo deploy-release --server ${octopusServer} --apiKey ${octopusApiKey} --project \"${octopusProject}\" --releaseNumber \"${octopusVersion}\" --deployto DEV-ESPFamily"
                    bat "octo deploy-release --server ${octopusServer} --apiKey ${octopusApiKey} --project \"${octopusProject}\" --releaseNumber \"${octopusVersion}\" --deployto UAT-ESPFamily"
                }
            }
        }
    }
    
    stage('Cleanup') {
        step([$class: 'WsCleanup', notFailBuild: true])
    }
}