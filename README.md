# migrate tool  migrate project between gitlab server

## Prerequisites:

 1. Windows operating system.
 2. Need to install Git client.
 3. Need .NET 6.0.
 4. Account and password for both gitSservers, prepare Access Tokens for both servers.

## Instructions for Use
 Step 1: Enter the source server address and access token, then click the 'checksrcserver' button. The program will read all groups and all projects under the groups from the source server and display them in the left list.

 Step 2: Enter the destination server address and access token, then click the 'checkdestserver' button. The program will read all groups from the destination server and display them in the right list. Note: Items starting with 'G' in the list indicate that it is a group, and items starting with 'P' indicate that it is a project.

 Step 3: Select a group or a project under the group from the left side, and then select a group or subgroup on the right side. Note: If a group is selected from the left, all projects under that group will be migrated during the migration process. If a project is selected, only the selected project will be migrated.

 Step 4: Click the 'migrate!' button to start the migration. Note: A console prompt will appear during the migration because the program calls the Git client to download and upload code. Pay attention to the prompts in the console; if a username and password are required, you will need to enter the username and password for either the source server or the destination server.


## The Git commands used:
```
git clone --bare <source_server_url>/<project_path>.git

git push --mirror    <destination_server_url>/<project_path>.git


Sometimes, if you encounter an error message, you can try the following methods:

Enter the program directory and find the project folder, manually execute the above command to view the specific error message, and then solve the corresponding problem.