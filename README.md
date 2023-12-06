# tms-api
Transport Management System

Requirement System:
1. VS 2022
2. Runtime .Netframework 5.0
3. MySql 8.0
4. Git command

How to build Api:
- Get source code from GitHub.
  + Go to folder what contains source code in Command Prompt
  + Type command 'Git clone https://github.com/[username]/tms-api.git'
- Run tms-api project in VS
- Check Transport database created or not yet.
- If https://localhost:5001/swagger/index.html is opend, api built successfully.
- Check Auth by clicking Authorize button and login by gmail, and then check api https://localhost:5001/api/User/CurrentUser.