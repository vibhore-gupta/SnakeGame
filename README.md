# SnakeGame
Console based classic snake game implemented with object oriented design patterns.

It is built with .net 5 and runs on console/terminal window.

Following are the steps to get the game running
1. Install dotnet SDK from https://dotnet.microsoft.com/download/dotnet/5.0 according to your platform.
2. Clone the repository and navigate to the project folder with .csproj file.
3. Execute command dotnet run -> This will build the project and then game will start on the terminal window.
4. Use Arrow keys to control the direction of the snake and try to eat the food. Level changes once you have eaten food 10 times.

Two ways to run the game in a docker container:
1. Pull image from dockerhub and run:
```docker run -it aprv/snakegame:latest```
2. Build docker locally using `Dockerfile`:
    * Navigate to Project folder and use this cmd to build the image
     ```docker  build -t snakegame -f Dockerfile .```
    * To run the docker :
    ```docker run -it snakegame  ```


If you like the game , don't forget to leave a star :)
